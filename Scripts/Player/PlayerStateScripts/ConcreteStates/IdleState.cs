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
        this.animator.SetBool("Jump", false);
        this.animator.Play("Idle");

        ReverseBody();
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

            ReverseBody();
        }
    }

    // 反転の処理
    // アニメーションのフリップモーションはすべてのアニメーションステートに適用するためplayerクラスに記述した
    private void ReverseBody()
    {
        if (this.player.isGround == true && this.player.isReverseBody == true)
        {
            this.myObj.transform.Rotate(0f, 180f, 0f);
            this.player.isReverseBody = false;
        }
    }

    public override void ExitState()
    {
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
        this.player.currentAttackTag = "";

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
