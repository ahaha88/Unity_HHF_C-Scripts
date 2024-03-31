using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickW : Stick
{
    void Awake()
    {
        Init();

        MyNoteType = RhythmGameManager.NoteType.StickW;
    }

    void Update()
    {
        UpdateMovement();
    }
}
