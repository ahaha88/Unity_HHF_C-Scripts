using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : RhythmGameNotes
{
    protected void UpdateMovement()
    {
        if (rgm.am.audioSource.time == 0f)
            return;


        InnerCircleDist = Vector3.Distance(rgm.InnerLocalPos, rt.localPosition);

        rt.localPosition -= rt.localPosition.normalized * rgm.NoteSpeed * Time.deltaTime;
        if ((rt.localPosition.x > -NoteDeadZone.x && rt.localPosition.x < NoteDeadZone.x) && (rt.localPosition.y > -NoteDeadZone.y && rt.localPosition.y < NoteDeadZone.y))
        {
            Destroy(rt.gameObject);
        }

        if (InnerCircleDist < rgm.InnerRadius + rgm.InnerRadius)
        {
            transform.tag = "StickTouch";
        }
    }
}
