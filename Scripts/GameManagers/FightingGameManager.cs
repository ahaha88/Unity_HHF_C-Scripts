using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static MainData;

/// <summary>
/// Environmentオブジェクトにアタッチ
/// 主にオブジェクトの設置やパラメーターの初期化、更新を行う
/// </summary>

public class FightingGameManager : MonoBehaviour
{

    public Player[] players { get; private set; } = new Player[2];
    private bool isStopTime = false;
    public UIManager uim {  get; private set; } 

    public Dictionary<string, AttackData> attackDataDic = new Dictionary<string, AttackData>();
    public Dictionary<string, CharacterData> characterDataDic = new Dictionary<string, CharacterData>();

    private void Awake()
    {
        QualitySettings.vSyncCount = 0; // スクリーンの性能によってフレームレートが可変にならないように設定（エディタの設定でも同様にしている）
        Application.targetFrameRate = 60; // エディタの画面では60を下回ることがあるため注意
    }

    private void Start()
    {
        players[0] = GameObject.FindWithTag("1P").GetComponent<Player>();
        players[1] = GameObject.FindWithTag("2P").GetComponent<Player>();

        uim = GetComponent<UIManager>();
        MainDataStart();
    }

    private void Update()
    {
        MainDataUpdate();
        
        if (isStopTime == false)
            TimeLimitUpdate();
    }

    // 負けたプレイヤー(stateがLoseになったとき)が渡される
    public void Judge(Player loser)
    {
        Time.timeScale = 1f;
        /*if (MainData.timeLimit <= 0f)
            return;*/

        if (loser.currentState != Player.State.Lose)
        {
            loser.SetState(Player.State.Lose);
        }

        Player winner = loser.status.index == 1 ? players[1] : players[0];
        MainData.winner = winner.status.index.ToString() + "P";
        winner.SetState(Player.State.Win);
        winner.status.point++;
        uim.SetJudgeText(winner.status.index + "P WIN");
        uim.SetPoint(winner.status.index, winner.status.point);
        isStopTime = true;

        if (winner.status.point < MainData.MAX_POINT)
        {
            Invoke(nameof(NewRound), 3f);
        }
        else
        {
            Invoke(nameof(LoadResult), 3);
        }
    }

    private void LoadResult()
    {
        MainData.currentGameState = MainData.GameState.Ready;
        SceneManager.LoadScene("Result");
    }

    private void TimeUp()
    {
        if (players[0].status.hitPoint < players[1].status.hitPoint)
        {
            players[1].SetState(Player.State.Win);

            Judge(players[0]);
        }
        else if (players[0].status.hitPoint > players[1].status.hitPoint)
        {
            players[1].SetState(Player.State.Lose);
            Judge(players[1]);
        }
        else
        {
            players[0].SetState(Player.State.Lose);
            players[1].SetState(Player.State.Lose);
            uim.SetJudgeText("DRAW");

            Invoke(nameof(NewRound), 3);
        }
    }

    public void TimeLimitUpdate()
    {
        if (MainData.timeLimit <= 0f)
        {
            TimeUp();
        }
        else
        {
            MainData.timeLimit -= Time.deltaTime;
        }
    }

    private void NewRound()
    {
        players[0].transform.position = MainData.START_POS[0];
        players[1].transform.position = MainData.START_POS[1];

        players[0].SetState(Player.State.Idle);
        players[1].SetState(Player.State.Idle);

        players[0].status.hitPoint = characterDataDic[MainData.CHARACTERS[0].ToString()].HP_Max;
        players[1].status.hitPoint = characterDataDic[MainData.CHARACTERS[1].ToString()].HP_Max;

        MainData.timeLimit = MainData.MAX_TIME;
        isStopTime = false;
        
        uim.SetJudgeText("");
        uim.SetTimerColor(Color.white);

        MainData.round++;

        players[0].playerInput.SwitchCurrentActionMap(players[0].playerInput.defaultActionMap);
        players[1].playerInput.SwitchCurrentActionMap(players[1].playerInput.defaultActionMap);

    }

    private void MainDataStart()
    {
        if (currentGameState == GameState.Ready) { return; }

        players[0].playerInput.SwitchCurrentActionMap(players[0].playerInput.defaultActionMap);
        players[1].playerInput.SwitchCurrentActionMap(players[1].playerInput.defaultActionMap);

        if (players[0] == null || players[1] == null)
        {
            Debug.Log("Main.player1 or 2がnull");
        }
        else
        {
            players[0].InitStatus();
            players[1].InitStatus();
            players[0].isLeft = players[0].transform.position.x <= players[1].transform.position.x ? true : false;
            players[1].isLeft = players[0].isLeft == true ? false : true;
            players[0].wasLeft = players[0].isLeft;
            players[1].wasLeft = players[1].isLeft;

            MainData.distance = Mathf.Abs(players[0].transform.position.x - players[1].transform.position.x);
            MainData.midPoint = new Vector3((players[0].transform.position.x + players[1].transform.position.x) / 2f, (players[0].transform.position.y + players[1].transform.position.y) / 2f, 0f);

            timeLimit = MAX_TIME;
        }
    }

    private void MainDataUpdate()
    {
        if (currentGameState == GameState.Ready) { return; }

        MainData.distance = Mathf.Abs(players[0].transform.position.x - players[1].transform.position.x);
        MainData.midPoint = new Vector3((players[0].transform.position.x + players[1].transform.position.x) / 2f, (players[0].transform.position.y + players[1].transform.position.y) / 2f, 0f);

        players[0].isLeft = players[0].transform.position.x <= players[1].transform.position.x ? true : false;
        players[1].isLeft = players[0].isLeft == true ? false : true;
    }
}
