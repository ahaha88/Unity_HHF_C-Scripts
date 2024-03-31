using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// PunchStateとKickStateの基底クラス
/// </summary>

public abstract class AttackState : PlayerState
{
    private int count = 0;

    // AttackStateの派生クラスでPunchボタンかKickボタンが押されたときの処理
    public override void OnStarted(Player.State state)
    {
        if (this.player.isGround == false)
        {
            this.animator.SetBool("Jump", true);
        }

        if (this.player.isSitDown == true)
        {
            this.animator.SetBool("SitDown", true);
        }


        this.player.attackPhase++;
        this.animator.SetInteger("AttackPhase", this.player.attackPhase);

        if (this.player.attackPhase == 1)
        {
            this.player.currentAttackTag = state.ToString().Substring(0, 1);
        }
        else
        {
            this.player.nextAttackType = state.ToString().Substring(0, 1);
            this.animator.SetInteger("NextAttackType", (int)state);
        }
    }

    public override void SetAnimationEvent()
    {
        foreach (var behaviour in this.player.aab)
        {
            behaviour.SetAnimEnterListener(EnterAttackAnim);

            behaviour.SetAnimUpdateListener(() => count++);


            // アニメーションの経過フレーム数に応じてコライダーの発生、消去（硬直）の処理を加える
            // ＜注意＞ビルドしたときは60フレームだが、エディタの画面ではPCの性能により60フレーム以下になってしまうため調整する際などは気を付ける。

        }
    }

    private void EnterAttackAnim()
    {
        count= 0;

        string nextAttackTag = this.player.currentAttackTag + this.player.nextAttackType;
        
        if (this.animator.GetCurrentAnimatorStateInfo(0).IsTag(nextAttackTag))
        {
            this.player.currentAttackTag = nextAttackTag;
        }
    }
}
