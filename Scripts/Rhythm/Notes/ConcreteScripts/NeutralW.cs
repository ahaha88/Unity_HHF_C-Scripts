using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RhythmGameManager;

public class NeutralW : Neutral
{
    void Awake()
    {
        Init();

        MyNoteType = RhythmGameManager.NoteType.W;
    }

    void Update()
    {
        UpdateSize();
    }
}
