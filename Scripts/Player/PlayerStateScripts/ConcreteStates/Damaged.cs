using System;
using System.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class DamagedState : PlayerState
{

    public DamagedState()
    {
        this.myState = Player.State.Damaged;
    }

    public override void EnterState()
    {
        this.animator.SetTrigger("Damaged");

        if (MainData.timeLimit <= 0)
            return;

        Player enemy = this.player.status.index == 1 ? player.fgm.players[1] : player.fgm.players[0];

        // 相手が使用している攻撃の威力を計算してHPから引く
        this.player.status.hitPoint -= Mathf.FloorToInt(enemy.status.attackPower * player.fgm.attackDataDic[enemy.currentAttackTag].multiplier);
        if (this.player.status.hitPoint <= 0)
        {
            this.player.status.hitPoint = 0;
            Time.timeScale = 0.5f;

            if (this.player.isGround == true)
                this.player.SetState(Player.State.Lose);
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
