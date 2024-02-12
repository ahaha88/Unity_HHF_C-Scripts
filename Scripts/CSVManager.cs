using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CSVManager : MonoBehaviour
{
    public Dictionary<string, AttackData> attackDataDic = new Dictionary<string, AttackData>();

    private void Start()
    {
        LoadAttackData();
    }

    private void LoadAttackData()
    {
        string filePath = "CSV/AttackData"; // Resources以下のPATH（.csvなどの拡張子は省略）
        TextAsset csvFile = Resources.Load(filePath) as TextAsset;

        StringReader sr = new StringReader(csvFile.text);

        sr.ReadLine(); // 1行目を飛ばす

        while (sr.Peek() != -1)
        {
            string[] values = sr.ReadLine().Split(",");

            string attackName = values[0];
            int phase = int.Parse(values[1]);
            float multiplier = float.Parse(values[2]);

            // 攻撃名をキー、AttackDataクラス型のインスタンスをvalueに持つディクショナリに代入しデータを格納
            AttackData ad = new AttackData(attackName, phase, multiplier);
            attackDataDic.Add(attackName, ad);
        }

    }

    public AttackData GetAttackData(string attackName)
    {
        if (attackDataDic.ContainsKey(attackName))
        {
            return attackDataDic[attackName];
        }
        else
        {
            Debug.Log("該当する攻撃名がありません");
            return null;
        }
    }
}
