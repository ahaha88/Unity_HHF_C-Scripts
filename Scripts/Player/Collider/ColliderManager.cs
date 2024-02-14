using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 攻撃時にアニメーションの動きに合わせてTriggerをON、OFFするクラス
// アニメーションイベントに設定するメソッドを持つ
// プレイヤーのオブジェクトにアタッチしなければならない

public class ColliderManager : MonoBehaviour
{
    private Player player;
    private GameObject[] attackPoints = new GameObject[4]; // Triggerのあるオブジェクト
    private Collider[] colliders = new Collider[4]; // 各attackPointのコライダーコンポーネント

    public void Start()
    {
        player = GetComponent<Player>();
        attackPoints[0] = GameObject.FindGameObjectWithTag("Hand_L"); // アーマチュアのHand.L
        attackPoints[1] = GameObject.FindGameObjectWithTag("Hand_R"); // アーマチュアのHand.R
        attackPoints[2] = GameObject.FindGameObjectWithTag("Foot_L"); // アーマチュアのToes.L
        attackPoints[3] = GameObject.FindGameObjectWithTag("Foot_R"); // アーマチュアのToes.R

        for (int i = 0; i < attackPoints.Length; i++)
        {
            colliders[i] = attackPoints[i].GetComponent<Collider>();
        }
    }

    // TriggerをONにする、アニメーションイベントの攻撃の瞬間に登録
    private void OnCollider(int index)
    {
        colliders[index].enabled = true;
    }

    // TriggerをOFFにする、アニメーションイベントの攻撃の瞬間の直後に登録
    private void OffCollider(int index)
    {
        colliders[index].enabled = false;
    }
}
