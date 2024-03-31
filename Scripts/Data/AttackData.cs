using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu(fileName = "AttackData", menuName = "SctiptableObjects/AttackData")]
public class AttackData
{
    public string attackName { get; private set; } // 攻撃名
    public int phase { get; private set; } // 攻撃のフェーズ
    public float multiplier { get; private set; } // 攻撃倍率
    public int pop_damage { get; private set; }
    public bool isKD { get; private set; }
    public int effectIndex { get; private set; }
    public int SEIndex { get; private set; }
    public int objIndex { get; private set; }

    public AttackData(string attackName, int phase, float multi, int pop_damage, bool isKD, int effect, int SE, int obj) 
    { 
        this.attackName = attackName;
        this.phase = phase;
        this.multiplier = multi;
        this.pop_damage = pop_damage;
        this.isKD = isKD;
        this.effectIndex = effect;
        this.SEIndex = SE;
        this.objIndex = obj;
    }
}
