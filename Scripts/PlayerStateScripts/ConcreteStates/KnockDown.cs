using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public class KnockDownState : PlayerState
{
    public KnockDownState()
    {
        this.myState = Player.State.KnockDown;
    }   
    public override void EnterState()
    {
        this.animator.SetTrigger("KnockDown");
    }

    public override void UpdateState()
    {

    }

    public override void ExitState()
    {

    }

    public override void OnStarted(Player.State state)
    {

    }

    public override void SetAnimationEvent()
    {

    }
}
