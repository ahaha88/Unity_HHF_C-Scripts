using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public class WinState : PlayerState
{
    public WinState()
    {
        this.myState = Player.State.Win;
    }
    public override void EnterState()
    {
        this.animator.SetTrigger("Win");

        player.playerInput.SwitchCurrentActionMap("None");
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
