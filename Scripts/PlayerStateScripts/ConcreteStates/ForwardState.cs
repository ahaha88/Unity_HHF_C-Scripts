using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public class ForwardState : MoveState
{
    public ForwardState()
    {
        this.myState = Player.State.Forward;
    }
    public override void EnterState()
    {
        this.player.nextAttackType = "";
        this.animator.SetInteger("NextAttackType", 0);
        this.player.attackPhase = 0;
        this.animator.SetInteger("AttackPhase", 0);

        this.animator.SetBool("Forward", true);
    }
    public override void UpdateState()
    {
        // プレイヤーの相対位置によって移動方向を決める
        int tmpDir = this.player.isLeft == true ? 1 : -1;
        MovePosition(tmpDir, 4f);
    }

    public override void ExitState()
    {
        this.animator.SetBool("Forward", false);
    }
}
