using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// IdleState、ForwardState、BackwardState、JumpState、SprintStateの基底クラス
/// </summary>

public abstract class MoveState : PlayerState
{
    public void MovePosition(int dir, float speed) // dirは1なら右方向、-1なら左方向に移動する
    {
        if (dir < -1 || dir > 1 || dir == 0)
        {
            Debug.Log("MovePosition() の引数が誤っている");
            return;
        }

        float moveAmount = speed * Time.deltaTime;

        if (Main.distance >= Main.MAX_DIST || Mathf.Sqrt(player.transform.position.x) >= Main.FIELD_END)
        {
            if ((player.isLeft == true && dir == -1) || (player.isLeft == false && dir == 1))
            {
                moveAmount = 0;
            }
        }

        if (Main.distance <= Main.MIN_DIST && this.player.isGround == true)
        {
            if ((player.isLeft == true && dir == 1) || (player.isLeft == false && dir == -1))
            {
                moveAmount = 0;
            }
        }

        myObj.transform.position += new Vector3(dir * moveAmount, 0f, 0f);

    }

    public override void OnStarted(Player.State state)
    {

    }

    public override void SetAnimationEvent()
    {

    }
}
