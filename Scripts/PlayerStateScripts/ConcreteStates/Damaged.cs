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

    private void Start()
    {
        
    }

    public override void EnterState()
    {
        this.animator.SetTrigger("Damaged");
        Player enemy = this.player.PLAYER_NUMBER == 1 ? Main.players[1] : Main.players[0];

        this.player.hitPoint -= Mathf.FloorToInt(enemy.attackPower * Main.csvm.attackDataDic[enemy.currentAttackTag].multiplier);

        if (this.player.hitPoint <= 0)
        {
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
