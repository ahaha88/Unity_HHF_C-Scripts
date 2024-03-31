using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickE : Stick
{
    void Awake()
    {
        Init();

        MyNoteType = RhythmGameManager.NoteType.StickE;
    }

    void Update()
    {
        UpdateMovement();
    }
}
