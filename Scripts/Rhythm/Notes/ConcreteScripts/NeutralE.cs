using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutralE : Neutral
{
    void Awake()
    {
        Init();

        MyNoteType = RhythmGameManager.NoteType.E;
    }

    void Update()
    {
        UpdateSize();
    }
}
