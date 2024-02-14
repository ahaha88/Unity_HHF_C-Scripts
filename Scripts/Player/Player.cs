using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UIElements;

/// <summary>
/// Stateパターンを利用したプレイヤーの操作クラス（StateMachine）
/// 抽象クラスPlayerStateを実装した各Stateクラスのうち、現在のStateに対応するクラスのインスタンスがcurrentStateObjとして機能する
/// このクラスは1P/2Pにアタッチする
/// 2Pの動きはローカル対戦用の入力処理とml-agentで制御する処理をする
/// 基本的にこのクラスはアニメーションの処理やエフェクトの処理、オブジェクトの移動処理などは書かずに各Stateに遷移する際の処理を書くようにする
/// </summary>

public class Player : MonoBehaviour
{
    // ジョイスティック入力を8方向に変換したもの
    public enum InputDir
    {
        // U:Upper, Lw:Lower, R:Right, L:Left
        // 回転角の大きさ順になっている必要があるためため順番は変えない（None以外）

        R,      // 右
        UR,     // 右上
        U,      // 上
        UL,     // 左上
        L,      // 左
        LwL,    // 左下
        Lw,     // 下
        LwR,    // 右下

        None    // デッドゾーン内の入力
    }

    // 各状態を列挙、現在の状態によって対応したクラスが機能する
    public enum State 
    {
        Idle,          // アニメーション依存（WIN、Lose以外の全てのアニメーションは終了するとIdleに移行する）
        Punch = 1,     // ボタン依存 コンボ技はアニメーション依存
        Kick = 2,      // ボタン依存 コンボ技はアニメーション依存
        Damaged,       // 当たり判定依存
        KnockDown,     // 当たり判定依存
        Forward,       // ボタン依存
        Backward,      // ボタン依存
        Jump,          // ボタン依存
        Throw,         // ボタン依存
        Sprint,        // ボタン依存
        Win,           // 勝敗判定依存
        Lose           // 勝敗判定依存
    }

    // 補助的なState、対応したクラスは持たないがフラグとして管理
    public bool isGround { get; set; } = true; // 接地判定、Start()の処理が終了する前に地面につくとfalseのままなのでtrueで初期化
    public bool isSitDown { get; set; } // しゃがみ判定 

    // Start()の初期化が終了したかどうかの判定（初期化する前に地面に設置するとエラーになるため実装）
    private bool isFinishedInit = false;

    public State currentState { get; set; } = State.Idle; // 現在のState
    private PlayerState currentStateObj; // 現在のStateに対応するクラスのインスタンス
    public Animator animator { get; private set; }
    public Rigidbody rb { get; private set; }

    [SerializeField]
    private PlayerState[] StateObjects;
    public IdleAnimBehaviour iab {  get; set; }
    public AttackAnimBehaviour[] aab { get; set; }
    private TextMeshProUGUI playerInfo;


    public float[] input { get; private set; } = new float[2]; // 移動の入力 0:x 1:y
    public InputDir inputDir { get; private set; } = InputDir.None; // 現在の入力
    public bool wasLeft { get; set; } = true; //切り替わる前の相対位置
    public bool isLeft { get; set; } = true; // 相対的位置 左に位置するか否か
    public bool reverseBody { get; set; } = false; // 反転待ちの状態かどうか
    public bool isInIdleAnim { get; set; } = false; // Idleアニメーションが再生中かどうか
    public float jumpDeltaTime { get; private set; } = 0;
    public float jumpHeight { get; private set; } = 6.5f;

    // animation parameters
    public int attackPhase { get; set; } = 0; // 攻撃のフェーズ（コンボ攻撃を行うかどうかの指標）
    public string nextAttackType { get; set; }  // コンボ攻撃の次の攻撃　P: Punch, K: Kick (enumの番号による)
    public string currentAttackTag { get; set; } = "none"; // 現在再生している攻撃アニメーションのタグ（文字列""で初期化するとエラーになるため"none"で初期化）

    // player status
    public int PLAYER_NUMBER { get; private set; } // 1P or 2P
    public Main.Character CHARA { get; set; }      // 着ぐるみの種類
    public string NAME { get; set; }               // ニックネーム
    public int attackPower { get; set; } = 500;    // 攻撃力（キャラクターの人気によって戦いの最中に変動する予定）
    public int deffencePower { get; set; }         // 防御力（ダメージ計算に使うか未定）
    public int hitPoint { get; set; } = 10000;     // HP
    public int popularity { get; set; }            // 人気度
    public int stomack { get; set; }               // 胃の容量 投げ技のアイテムをビール瓶とする際のパラメーター
    public int combo { get; set; }                 // コンボの数

    private void Start()
    {
        PLAYER_NUMBER = gameObject.tag == "1P" ? 1: 2; // tagに従う
        
        // 2PのPlayerStateScriptsを別のインスタンスとして取得する処理
        if (PLAYER_NUMBER == 2)
        {
            wasLeft = false;
            isLeft = false;
            Instantiate(GameObject.FindWithTag("PSScripts_1P")).tag = "PSScripts_2P";
        }

        StateObjects = new PlayerState[Enum.GetValues(typeof(State)).Length];
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        iab = animator.GetBehaviour<IdleAnimBehaviour>();
        aab = animator.GetBehaviours<AttackAnimBehaviour>();
        playerInfo = GameObject.Find("PlayerInfo").GetComponent<TextMeshProUGUI>();

        GetAllStateComponent();
        SetState(State.Idle);

        isFinishedInit = true;
    }

    private void Update()
    {
        currentStateObj.UpdateState();

        if (PLAYER_NUMBER == 1)
        {
            playerInfo.SetText("1P State:" + currentState.ToString() + "\nAttackName: " + currentAttackTag);
        }

        // 移動制限
        if (transform.position.x < -Main.FIELD_END)
        {
            transform.position = new Vector3(-Main.FIELD_END, transform.position.y, transform.position.z);
        }
        if (transform.position.x > Main.FIELD_END)
        {
            transform.position = new Vector3(Main.FIELD_END, transform.position.y, transform.position.z);
        }

        // Player同士の位置が入れ替わった時の処理
        if (wasLeft != isLeft)
        {
            reverseBody = reverseBody == true ? false : true; // この変数が変更されればidleアニメーション中に反転する
            wasLeft = isLeft;
        }

        if (isGround == false)
        {
            JumpAction();
        }

    }

    // Jumpの処理 Update()とJumpStateのEnterState()で呼ばれる
    // JumpState以外のStateでもこの上下移動をしたいためこのクラスに処理を書いた
    public void JumpAction()
    {
        jumpDeltaTime += Time.deltaTime;
        float sin = jumpHeight * Mathf.Sin(2 * Mathf.PI * jumpDeltaTime);

        float x = this.transform.position.x;
        float y = sin > 0 ? Main.START_POS[0].y + sin : Main.START_POS[0].y; // 0 < y < height にする処理、配列のindexは0でも1でもいい
        float z = this.transform.position.z;

        this.transform.position = new Vector3(x, y, z);
    }

    // 現在のStateをSetする関数
    public void SetState(State state)
    {
        int stateNum = (int)state;

        // 前のStateと新しいStateが同じなら何もしない（Damaged以外）
        // currentStateではなくcurrentStateObjで判定するのは一番最初に呼ばれたときcurrentStateObjがnullなるのを防ぐため。
        if (currentStateObj == StateObjects[stateNum] && currentStateObj != StateObjects[(int)State.Damaged])
        {
            return;
        }

        currentStateObj?.ExitState(); // 現在のStateから終了処理を実行、[?]は最初の処理で除外するため

        // 新しい状態へ遷移
        currentStateObj = StateObjects[stateNum];

        currentState = currentStateObj.SetEnable();
        if (currentState != State.Idle)
        {
            isInIdleAnim = false;
        }
        Debug.Log(PLAYER_NUMBER + "P currentState: " + currentState);
        currentStateObj.EnterState();
    }

    // 入力処理
    public void OnMove(InputAction.CallbackContext ctx)
    {
        float x = ctx.ReadValue<Vector2>().x;
        float y = ctx.ReadValue<Vector2>().y;

        // 入力がデッドゾーン内ならNoneになる（デッドゾーンが円状であることに注意）
        if (Mathf.Sqrt(x * x + y * y) < Main.DEAD_ZONE)
        {
            input[0] = 0;
            input[1] = 0;
            MoveAction();
            return;
        }

        input[0] = x;
        input[1] = y;
        MoveAction();
    }

    // 移動入力があった時の処理（Idleアニメーションが開始したときも呼ばれる）
    public void MoveAction()
    {
        float angle = Mathf.Atan2(input[1], input[0]) * Mathf.Rad2Deg; // 弧度法から度数法へ変換した角度
        angle = Mathf.RoundToInt(angle / 45f); // 16分割した角度
        angle = angle < 0 ? angle += 8 : angle; // -180 < thita < 180 から 0 < thita < 360 に変更 (thita : 度数法で表す回転角)

        inputDir = (InputDir)angle;

        inputDir = (input[0] == 0 && input[1] == 0) ? InputDir.None : inputDir; // 入力がデッドゾーン内ならあらかじめ0にされる

        // 攻撃中はinputの値を変化させるだけでstateは変えない
        if (ContainState(State.Punch, State.Kick, State.Throw, State.Damaged)) return;

        if (!(inputDir == InputDir.LwL || inputDir == InputDir.Lw || inputDir == InputDir.LwR)) 
            isSitDown = false;

        State left = isLeft == true ? State.Backward : State.Forward;
        State right = isLeft == true ? State.Forward : State.Backward;

        switch (inputDir)
        {
            // 右入力
            case InputDir.R: 
                if (isGround == true)
                    SetState(right);
                break;

            // 上入力
            case InputDir.UR:
            case InputDir.U:
            case InputDir.UL:
                if (isSitDown == true) return;

                if (isGround == true)
                    SetState(State.Jump);
                break;

            // 左入力
            case InputDir.L: 
                if (isGround == true)
                    SetState(left);
                break;

            // 下入力
            case InputDir.LwL: 
            case InputDir.Lw:
            case InputDir.LwR:
                if (currentState == State.Jump) return;

                isSitDown = true;
                SetState(State.Idle);
                break;
            
            // ニュートラル (None)
            default:
                if (ContainState(State.Backward, State.Forward) && isGround == true)
                    SetState(State.Idle);
                break;
        }
    }

    // PanchとKickの処理
    public void OnAction(InputAction.CallbackContext ctx, State state)
    {
        if (ctx.started) // アクションが呼び出されたとき
        {
            if (attackPhase == 0)
            {
                // 同時押しの排他処理
                if (currentState == State.Punch || currentState == State.Kick) return;

                SetState(state);
            }
            else
            {
                Debug.Log(currentAttackTag + Main.csvm.attackDataDic[currentAttackTag].phase);
                if (Main.csvm.attackDataDic[currentAttackTag].phase == attackPhase && isGround == true) // 再生中のAnimationのPhaseを確認
                {
                    // １つのアニメーションに対して再生中に一回しか呼ばれない
                    currentStateObj.OnStarted(state);
                }
            }
        }

        // アクションが実行された（押され続けられている）とき
        if (ctx.performed)
        {
            // 溜め技の処理追加
        }

        // アクションがキャンセルされたとき
        if (ctx.canceled)
        {

        }
    }

    // 実装予定（ものを投げる技）
    private void OnThrow(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            SetState(State.Throw);
        }
    }

    // 相手の攻撃が当たった時に呼ばれる
    public void OnDamaged()
    {
        if (isSitDown == true || currentState == State.Backward)
        {
            // 防御処理OnGuard()
        }

        SetState(State.Damaged);
    }

    // StateObjectsをすべて取得し初期化
    private void GetAllStateComponent()
    {
        string playerNum = PLAYER_NUMBER == 1 ? "1P" : "2P";
        PlayerStateScripts pss = GameObject.FindGameObjectWithTag("PSScripts_" + playerNum).GetComponent<PlayerStateScripts>();

        for (int i = 0; i < Enum.GetValues(typeof(State)).Length; i++)
        {
            // PlayerStateScriptsオブジェクトから各Scriptを取得
            StateObjects[i] = pss.Scripts[i];

            StateObjects[i]?.SetGameObject(gameObject);
            StateObjects[i].SetPlayer(this);
            StateObjects[i].SetAnimator(animator);
            StateObjects[i].SetRigidbody(rb);

            StateObjects[i].SetAnimationEvent(); // コールバック登録（あれば）

            if (StateObjects[i] == null)
                Debug.Log(StateObjects[i] + "がnull");
        }
    }

    // 現在のStateを複数の候補（引数として指定）から確認する関数
    public bool ContainState(params State[] states)
    {
        foreach (State state in states)
        {
            if (currentState == state)
                return true;
        }

        return false;
    }

    // 自分のTriggerが相手のコライダーに接触したとき相手のOnDamaged()を呼ぶ
    private void OnTriggerEnter(Collider collider)
    {
        // 敵のtagの文字列を設定
        string enemyTag = PLAYER_NUMBER == 1 ? "2P" : "1P";

        //  敵のボディにtriggerが接触したら敵のOnDamaged()を呼ぶ
        if (collider.gameObject.tag == enemyTag)
            collider.transform.root.gameObject.GetComponent<Player>().OnDamaged();
    }

    // 接地判定
    private void OnCollisionEnter(Collision collision)
    {
        // Start()が終了していない状態で呼ばれるとエラーになるため追加
        if (isFinishedInit == false)
        {
            return;
        }

        if (collision.gameObject.tag == "Floor")
        {
            isGround = true;
            jumpDeltaTime = 0;
            if (currentState == State.Jump)
            {
                SetState(State.Idle);
            }
        }
    }

    // 接地判定
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isGround = false;
        }
    }
}
