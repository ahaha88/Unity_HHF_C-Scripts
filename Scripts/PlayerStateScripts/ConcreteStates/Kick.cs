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
    }
    public override void UpdateState()
    {

    }

    public override void ExitState()
    {

    }

    public override void SetAnimationEvent()
    {

    }

}
