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
            this.OnStarted(this.myState);
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
        foreach (var behaviour in this.player.aab)
        {
            behaviour.SetAnimEnterListener(SetCount);
            behaviour.SetAnimUpdateListener(OnAnimUpdate);
        }
    }

}
