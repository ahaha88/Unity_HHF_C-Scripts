using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MoveInnerCircle : MonoBehaviour
{
    private RhythmGameManager rgm;
    private RectTransform rt;
    private AudioManager am;
    private Image image;
    
    private List<RhythmGameNotes> touchedNotes = new List<RhythmGameNotes>();
    private int noteIndex = 0;

    private Vector3 joyStickInput = new Vector3(0f, 0f, 0f);
    private Vector2 nom = new Vector2(0f, 0f);
    private Color32 initialColor;
    private int newsType = 0;
    

    private void Start()
    {
        rgm = transform.parent.gameObject.GetComponent<RhythmGameManager>();
        rt = GetComponent<RectTransform>();
        am = GetComponent<AudioManager>();  
        image = GetComponent<Image>();
        initialColor = image.color;
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        joyStickInput = ctx.ReadValue<Vector2>();
        float angle = Mathf.Atan2(joyStickInput.y, joyStickInput.x);
        angle = Mathf.RoundToInt(angle / (Mathf.PI / 4)) * Mathf.PI / 4; // 8分割した角度

        float cos = (float)Math.Round(Mathf.Cos(angle), 6, MidpointRounding.AwayFromZero);
        float sin = (float)Math.Round(Mathf.Sin(angle), 6, MidpointRounding.AwayFromZero);

        nom = new Vector2(cos, sin).normalized; // 単位ベクトル

        rt.localPosition = nom * joyStickInput.magnitude * rgm.OuterRadius;
        if (ctx.canceled)
        {
            nom = Vector2.zero;
            rt.localPosition = Vector3.zero;
        }
    }

    public void OnTap(InputAction.CallbackContext ctx)
    {
        Vector2 input = ctx.ReadValue<Vector2>();

        if (input.x != 0 && input.y != 0)
        {
            return; // 同時押し
        }

        if (ctx.started)
        {
            int angle = Mathf.RoundToInt(Mathf.Atan2(input.y, input.x) / (Mathf.PI / 2));
            angle = angle < 0 ? angle + 4 : angle;
            newsType = angle;
            if (rgm.IsDebugMode)
            {
                SaveNotes();
            }
            else
            {
                DestroyNearestNote();
            }
            image.color -= new Color32(30, 30, 30, 0);
        }

        if (ctx.canceled)
        {
            touchedNotes.Clear();
            image.color = initialColor;
        }
    }

    private void DestroyNearestNote()
    {
        List<GameObject> tmp = new List<GameObject>();
        float minDist;
        int minDistIndex = 0;

        if (joyStickInput == Vector3.zero)
        {
            tmp.AddRange(GameObject.FindGameObjectsWithTag("NeutralTouch"));
        }
        else
        {
            tmp.AddRange(GameObject.FindGameObjectsWithTag("StickTouch"));
        }
        

        foreach (GameObject go in tmp)
        {
            touchedNotes.Add(go.GetComponent<RhythmGameNotes>());
        }

        if (touchedNotes.Count == 0)
        {
            am.PlaySE((int)RhythmGameManager.NoteType.Beep);
            return;
        }
        else if (touchedNotes.Count == 1 && newsType == (int)touchedNotes[0].MyNoteType)
        {
            Destroy(touchedNotes[0].gameObject);
        }
        else // 内側の円に触れたノーツのうち最も距離の近いノーツを消す
        {
            if (joyStickInput == Vector3.zero)
            {
                minDist = Mathf.Abs(touchedNotes[0].rt.sizeDelta.x - rgm.OuterRadius * 2); // 配列のindexは0にしておく

                for (int i = 0; i < touchedNotes.Count; i++)
                {
                    if (Mathf.Abs(touchedNotes[i].rt.sizeDelta.x - rgm.OuterRadius * 2 ) < minDist)
                    {
                        minDist = Mathf.Abs(touchedNotes[i].rt.sizeDelta.x - rgm.OuterRadius * 2);
                        minDistIndex = i;
                    }
                }

            }
            else
            {
                minDist = touchedNotes[0].InnerCircleDist; // 配列のindexは0にしておく
                for (int i = 0; i < touchedNotes.Count; i++)
                {
                    if (touchedNotes[i].InnerCircleDist < minDist)
                    {
                        minDist = touchedNotes[i].InnerCircleDist;
                        minDistIndex = i;
                    }
                }
            }
            
            
            
        }
        
        int type = joyStickInput == Vector3.zero ? newsType : newsType + 4;

        // ノーツに対応したボタンなら消す
        if (type == (int)touchedNotes[minDistIndex].MyNoteType)
        {
            Destroy(touchedNotes[minDistIndex].gameObject);
            am.PlaySE(type);
        }
        else
        {
            am.PlaySE((int)RhythmGameManager.NoteType.Beep);
        }
        
    }

    private void SaveNotes()
    {
        NoteData nd = new NoteData(noteIndex, nom.x, nom.y, newsType, rgm.am.audioSource.time);
        rgm.NoteDataDic.Add(noteIndex, nd);
        
        NoteData debug = rgm.NoteDataDic[noteIndex];
        Debug.Log(" index: " + debug.index + " x: " + debug.x + " y: " + debug.y + " type: " + debug.type + " time: " + debug.time);

        int type = joyStickInput == Vector3.zero ? newsType : newsType + 4;
        am.PlaySE(type);

        noteIndex++;
    }
}
