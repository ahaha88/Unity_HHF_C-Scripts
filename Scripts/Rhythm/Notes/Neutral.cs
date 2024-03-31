using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Neutral : RhythmGameNotes
{
    protected float visibleSize = 800f;

    private void Update()
    {
        UpdateSize();
    }

    protected void UpdateSize()
    {
        if (rt.sizeDelta.x < visibleSize && rt.sizeDelta.y < visibleSize)
        {
            image.enabled = true;
        }
        else
        {
            image.enabled = false;
        }

        if (rgm.am.audioSource.time == 0f)
            return;

        float speed = rgm.OuterRadius * 2;
        rt.sizeDelta -= new Vector2(speed, speed) * Time.deltaTime;


        if (rt.sizeDelta.x < rgm.OuterRadius * 3)
        {
            this.tag = "NeutralTouch";
        }

        if (rt.sizeDelta.x < 0 && rt.sizeDelta.y < 0)
        {
            Debug.Log(rgm.am.audioSource.time);
            Destroy(this.gameObject);
        }

    }
}
