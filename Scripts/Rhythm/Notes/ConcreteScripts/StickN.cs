using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickN : Stick
{
    void Awake()
    {
        Init();

        MyNoteType = RhythmGameManager.NoteType.StickN;
    }

    void Update()
    {
        UpdateMovement();
    }
}
