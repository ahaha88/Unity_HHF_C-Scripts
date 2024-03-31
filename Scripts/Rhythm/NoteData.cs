using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteDataDic
{
    public int index;
    public float x;
    public float y;
    public int type;
    public float time;

    public NoteDataDic(int index, float x, float y, int type, float time)
    { 
        this.index = index;
        this.x = x;
        this.y = y;
        this.type = type;
        this.time = time;
    }
}
