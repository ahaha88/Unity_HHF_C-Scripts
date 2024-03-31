using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

/// <summary>
/// 各Stateに実装する抽象クラス。
/// </summary>

public abstract class PlayerState : MonoBehaviour
{
    public GameObject myObj {  get; protected set; }
    public Player player { get; protected set; }
    public Animator animator { get; protected set; }
    public Rigidbody rb { get; protected set; }
    protected InputAction inputAction;
    public Player.State myState { get; protected set; } // 自分のStateをコンストラクタで代入


    public abstract void EnterState(); // 初期化処理(アニメーションの開始など)

    public abstract void UpdateState(); // 更新処理(必要であれば)

    public abstract void ExitState(); // 終了処理（アニメーションの終了など）

    public abstract void OnStarted(Player.State state); // 入力処理があった時の処理

    public abstract void SetAnimationEvent();

    public Player.State SetEnable()
    {
        return this.myState;
    }

    // Animatorのすべてのparameterをリセット
    public void ResetAllAnimatorParam()
    {
        AnimatorControllerParameter[] parameters = animator.parameters;

        foreach (var parameter in parameters)
        {
            switch (parameter.type)
            {
                case AnimatorControllerParameterType.Trigger:
                    animator.ResetTrigger(parameter.name);
                    break;
                case AnimatorControllerParameterType.Bool:
                    animator.SetBool(parameter.name, false);
                    break;
                case AnimatorControllerParameterType.Int:
                    animator.SetInteger(parameter.name, 0);
                    break;
                case AnimatorControllerParameterType.Float:
                    animator.SetFloat(parameter.name, 0);
                    break;
                default:
                    break;
            }
        }
    }

    // 以下コンポーネントとオブジェクトのSetter

    public void SetGameObject(GameObject myObj)
    {
        if (myObj == null)
        {
            Debug.Log("GameObjectがnull");
            return;
        }
        this.myObj = myObj;
    }

    public void SetPlayer(Player player)
    {
        if (player == null)
        {
            Debug.Log("Player型の引数がnull");
            return;
        }
        this.player = player;
    }

    public void SetAnimator(Animator animator)
    {
        if (animator == null)
        {
            Debug.Log("Animator型の引数がnull");
            return;
        }
        this.animator = animator;
    }

    public void SetRigidbody(Rigidbody rb)
    {
        if (rb == null)
        {
            Debug.Log("Rigidbody型の引数がnull");
            return;
        }
        this.rb = rb;
    }
}
