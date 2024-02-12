using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class IdleState : MoveState
{

    public IdleState()
    {
        this.myState = (int)Player.State.Idle;
    }

    public override void EnterState()
    {
        // EnterIdleAnim()に処理を書く（アニメーション依存の遷移をするため）
        this.animator.Play("Idle");
    }

    public override void UpdateState()
    {
        if (this.player.isSitDown == true)
        {
            this.animator.SetBool("SitDown", true);
        }
        else
        {
            this.animator.SetBool("SitDown", false);

            // 反転の処理
            if (this.player.isGround == true && this.player.reverseBody == true)
            {
                this.myObj.transform.Rotate(0f, 180f, 0f);
                this.player.reverseBody = false;
            }
        }
    }

    public override void ExitState()
    {
        this.player.isSitDown = false;
        this.animator.SetBool("SitDown", false);
    }

    public override void SetAnimationEvent()
    {
        this.player.iab.SetAnimEnterListener(EnterIdleAnim);
    }

    public void EnterIdleAnim()
    {
        this.player.nextAttackType = "";
        this.animator.SetInteger("NextAttackType", 0);
        this.player.attackPhase = 0;
        this.animator.SetInteger("AttackPhase", 0);

        if (this.player.currentState != Player.State.Jump)
        {
            if (this.player.isGround == true)
            {
                this.player.SetState(this.myState);
            }
            else
            {
                this.player.SetState(Player.State.Jump);
            }
        }
        this.player.MoveAction();
    }

    

}
