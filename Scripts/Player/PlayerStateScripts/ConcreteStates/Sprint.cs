using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public class SprintState : MoveState
{
    public SprintState()
    {
        this.myState = Player.State.Sprint;
    }
    public override void EnterState()
    {
        this.animator.SetTrigger("Sprint");
    }
    public override void UpdateState()
    {

    }

    public override void ExitState()
    {

    }
}
