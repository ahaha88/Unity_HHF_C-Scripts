using System.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class JumpState : MoveState
{
    Player.InputDir initialDir;


    public JumpState()
    {
        this.myState = Player.State.Jump;
    }
    public override void EnterState()
    {
        initialDir = this.player.inputDir;

        

        if ((this.player.isLeft && initialDir == Player.InputDir.UR) || (!this.player.isLeft && initialDir == Player.InputDir.UL))
        {
            this.animator.SetInteger("JumpType", 1);
        }
        else if ((!this.player.isLeft && initialDir == Player.InputDir.UR) || (this.player.isLeft && initialDir == Player.InputDir.UL))
        {
            this.animator.SetInteger("JumpType", -1);
        }
        else
        {
            this.animator.SetInteger("JumpType", 0);
        }
            this.animator.SetBool("Jump", true);

        this.player.JumpAction();
    }
    public override void UpdateState()
    {
        // Jump中の移動処理
        // <注意> Jump中の攻撃はStateを変更する(一度でも攻撃すると、着地するまでスティックでの移動はできなくなる)
        if (initialDir == Player.InputDir.UR)
        {
            MovePosition(1, 4f);
        }
        else if (initialDir == Player.InputDir.UL)
        {
            MovePosition(-1, 4f);
        }

    }

    public override void ExitState()
    {
        this.animator.SetInteger("JumpType", 0);
        this.animator.SetBool("Jump", false);
    }

}