using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public class PunchState : AttackState
{
    public PunchState()
    {
        this.myState = Player.State.Punch;
    }
    public override void EnterState()
    {
        if (this.player.attackPhase == 0)
        {
            this.animator.SetTrigger("Punch");
            OnStarted(this.myState);
        }
    }
    public override void UpdateState()
    {

    }

    public override void ExitState()
    {

    }

    // SetAnimattionEvent()はAttackState()に記述し、このクラスには何も書かない
    // PlayerクラスのGetAllStateConponent()で登録されるときに複数回呼ばれるのを防ぐため派生クラスであるKickStateとThrowStateには何もしないメソッド SetAnimattionEvent(){}を記述する必要がある。
}
