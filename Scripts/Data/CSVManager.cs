using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;
public class fileStatus
{
    public CSVManager.File fileType {  get; private set; }
    public string path { get; private set; }

    public fileStatus(CSVManager.File fileType, string path)
    {
        this.fileType = fileType;
        this.path = path;
    }
}

public class CSVManager : MonoBehaviour
{
    public enum File
    {
        AttackData,
        CharacterData,
    }

    private FightingGameManager fgm;

    private void Awake()
    {
        fgm = GetComponent<FightingGameManager>();

        fileStatus attackData = new fileStatus(File.AttackData, "CSV/AttackData");
        fileStatus characterData = new fileStatus(File.CharacterData, "CSV/CharacterData");

        ReadFile(attackData);
        ReadFile(characterData);
    }


    private void ReadFile(fileStatus status)
    {
        TextAsset csvFile = Resources.Load(status.path) as TextAsset; // Resources以下のPATH（.csvなどの拡張子は省略）
        StringReader sr = new StringReader(csvFile.text);

        switch (status.fileType)
        {
            case File.AttackData:
                LoadAttackData(sr);
            break;
            case File.CharacterData: 
                LoadCharacterData(sr); 
            break;


            default: 
            break;
        }
    }

    private void LoadCharacterData(StringReader sr)
    {
        sr.ReadLine(); // 1行目を飛ばす

        while (sr.Peek() != -1)
        {
            string[] values = sr.ReadLine().Split(",");

            string character = values[0];
            int HP_Max = int.Parse(values[1]);
            int ATK = int.Parse(values[2]);
            int DEF = int.Parse(values[3]);
            int POP_Init = int.Parse(values[4]);
            
            // キャラクター名をキー、CharacterDataクラス型のインスタンスをvalueに持つディクショナリに代入しデータを格納
            CharacterData cd = new CharacterData(character, HP_Max, ATK, DEF, POP_Init);
            fgm.characterDataDic.Add(character, cd);
            Debug.Log(cd.Character + " " + fgm.characterDataDic.ContainsKey(cd.Character));

        }
    }

    private void LoadAttackData(StringReader sr)
    {
        sr.ReadLine(); // 1行目を飛ばす

        while (sr.Peek() != -1)
        {
            string[] values = sr.ReadLine().Split(",");
            string attackName = values[0];
            int phase = int.Parse(values[1]);
            float multiplier = float.Parse(values[2]);
            int pop_damage = int.Parse(values[3]);
            bool isKD = values[4] == "Y" ? true : false;
            int effect = int.Parse(values[5]);
            int SE = int.Parse(values[6]);
            int obj = int.Parse(values[7]);

            // 攻撃名をキー、AttackDataクラス型のインスタンスをvalueに持つディクショナリに代入しデータを格納
            AttackData ad = new AttackData(attackName, phase, multiplier, pop_damage, isKD, effect, SE, obj);
            fgm.attackDataDic.Add(attackName, ad);
        }

    }
}
