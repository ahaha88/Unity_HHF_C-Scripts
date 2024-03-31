using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class RhythmGameNotes : MonoBehaviour
{


    protected RhythmGameManager rgm;
    public RectTransform rt {  get; private set; }
    protected Image image;

    protected Vector3 NoteDeadZone = new Vector3(1f, 1f, 0);
    public RhythmGameManager.NoteType MyNoteType { get; protected set; } = 0;
    public float InnerCircleDist { get; protected set; }

    private void Awake()
    {
        Init();
    }


    protected void Init()
    {
        rgm = transform.root.Find("OuterCircle").GetComponent<RhythmGameManager>();
        rt = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }
}
