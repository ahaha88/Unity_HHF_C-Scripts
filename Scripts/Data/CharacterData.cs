using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData
{
    public string Character { get; private set; }
    public int HP_Max { get; private set; }
    public int ATK { get; private set; }
    public int DEF { get; private set; }
    public int POP_Init { get; private set; }

    public CharacterData(string Character, int HP_Max, int ATK, int DEF, int POP_Init)
    { 
        this.Character = Character;
        this.HP_Max = HP_Max;
        this.ATK = ATK;
        this.DEF = DEF;
        this.POP_Init = POP_Init;
    }
}
