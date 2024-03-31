using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

// 攻撃時にアニメーションの動きに合わせてTriggerをON、OFFするクラス
// アニメーションイベントに設定するメソッドを持つ
// プレイヤーのオブジェクトにアタッチしなければならない

public class ColliderManager : MonoBehaviour
{
    private Player player;
    private Collider[] colliders = new Collider[5]; // 各attackPointのコライダーコンポーネント

    public void Start()
    {
        player = GetComponent<Player>();

        foreach (Collider collider in GetComponentsInChildren<Collider>())
        {
            if (collider.transform.root.tag == player.transform.tag)
            {
                switch (collider.transform.tag)
                {
                    case "Hand_L":
                        colliders[0] = collider;
                        break;
                    case "Hand_R":
                        colliders[1] = collider;
                        break;
                    case "Foot_L":
                        colliders[2] = collider;
                        break;
                    case "Foot_R":
                        colliders[3] = collider;
                        break;
                    case "Head":
                        colliders[4] = collider;
                        break;
                    default: 
                        break;
                }
            }
        }
    }

    private void Update()
    {
        if (player.currentState == Player.State.Damaged)
        {
            ResetAllCollider();
        }
    }

    public void ResetAllCollider()
    {
        foreach (Collider collider in colliders)
        {
            if (collider != null)
            {
                collider.enabled = false;
            }
        }
    }

    // TriggerをONにする、アニメーションイベントの攻撃の瞬間に登録
    private void OnCollider(int index)
    {
        
        // フリップモーション対応
        if (this.player.isLeft == false)
        {
            if (index == 0 || index == 2)
            {
                index++;
            }
            else // 1, 3
            {
                index--;
            }
        }
            

        colliders[index].enabled = true;
    }

    // TriggerをOFFにする、アニメーションイベントの攻撃の瞬間の直後に登録
    private void OffCollider(int index)
    {
        // フリップモーション対応
        if (this.player.isLeft == false)
        {
            if (index == 0 || index == 2)
            {
                index++;
            }
            else // 1, 3
            {
                index--;
            }
        }

        colliders[index].enabled = false;
    }
}
