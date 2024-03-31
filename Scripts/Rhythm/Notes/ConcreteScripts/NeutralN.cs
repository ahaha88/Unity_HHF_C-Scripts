using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutralN : Neutral
{
    void Awake()
    {
        Init();

        MyNoteType = RhythmGameManager.NoteType.N;
    }

    void Update()
    {
        UpdateSize();
    }
}