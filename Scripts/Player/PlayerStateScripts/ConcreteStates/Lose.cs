using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public class LoseState : PlayerState
{
    public LoseState()
    {
        this.myState = Player.State.Lose;
    }
    public override void EnterState()
    {
        this.animator.SetTrigger("Lose");

        player.playerInput.SwitchCurrentActionMap("None");
        if (MainData.timeLimit > 0f)
        {
            GameObject.FindWithTag("GameManager").GetComponent<FightingGameManager>().Judge(this.player);
        }
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
