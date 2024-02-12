using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu(fileName = "AttackData", menuName = "SctiptableObjects/AttackData")]
public class AttackData
{
    public string attackName; // 攻撃名
    public int phase; // 攻撃のフェーズ
    public float multiplier; // 攻撃倍率


    public AttackData(string attackName, int phase, float multi) 
    { 
        this.attackName = attackName;
        this.phase = phase;
        this.multiplier = multi;
    }
}
