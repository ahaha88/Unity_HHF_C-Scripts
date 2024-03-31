using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RhythmGameManager;

public class StickS : Stick
{
    void Awake()
    {
        Init();

        MyNoteType = RhythmGameManager.NoteType.StickS;
    }

    void Update()
    {
        UpdateMovement();
    }
}
