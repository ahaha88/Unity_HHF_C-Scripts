using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public class KickState : AttackState
{
    public KickState()
    {
        this.myState = Player.State.Kick;
    }
    public override void EnterState()
    {
        if (this.player.attackPhase == 0)
        {
            this.animator.SetTrigger("Kick");
            OnStarted(this.myState);
        }

        player.status.popularity += player.fgm.attackDataDic[player.currentAttackTag].pop_damage;
    }
    public override void UpdateState()
    {

    }

    public override void ExitState()
    {

    }

    // コールバックが複数回登録されるのを防ぐため何もしないメソッド SetAnimattionEvent(){}を記述する必要がある。
    public override void SetAnimationEvent()
    {

    }

}
