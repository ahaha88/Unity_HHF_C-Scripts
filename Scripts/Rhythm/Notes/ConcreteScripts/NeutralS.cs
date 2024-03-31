using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutralS : Neutral
{
    void Awake()
    {
        Init();

        MyNoteType = RhythmGameManager.NoteType.S;
    }

    void Update()
    {
        UpdateSize();
    }
}
