using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// OuterCircleにアタッチ
/// </summary>

public class RhythmGameManager : MonoBehaviour
{
    public enum NoteType
    {
        E,
        N,
        W,
        S,

        StickE,
        StickN,
        StickW,
        StickS,

        Beep,
    }

    private RectTransform innerRT;
    private RectTransform octagonRT;
    private GameObject NeutralNotesFolder;
    private GameObject StickNotesFolder;
    [SerializeField] private List<GameObject> NotesObj;
    public List<RectTransform> NoteRT = new List<RectTransform>();
    public AudioManager am {  get; private set; }
    private RhythmCSVManager rcm;
    public Image image { get; private set; }

    [SerializeField]
    private TextMeshProUGUI textMode;
    public Dictionary<int, NoteData> NoteDataDic = new Dictionary<int, NoteData>();

    // private int score = 0;
    public float OuterRadius { get; private set; } = 100f;
    public float InnerRadius { get; private set; } = 30f;
    public Vector3 InnerLocalPos { get; private set; }
    public float NoteSpeed { get; private set; } = 200f;

    public bool IsDebugMode { get; set; } = false;
    public float DebugTime { get; private set; } = 0f;


    private void Awake()
    {
        innerRT = GameObject.Find("InnerCircle").GetComponent<RectTransform>();
        octagonRT = GetComponent<RectTransform>();
        NeutralNotesFolder = GameObject.Find("NeutralNotes");
        StickNotesFolder = GameObject.Find("StickNotes");
        am = GetComponent<AudioManager>();
        rcm = GetComponent<RhythmCSVManager>();
        image = GetComponent<Image>();
    }

    private void Start()
    {
        rcm.LoadNoteData();
        GenerateNotes();
    }

    private void Update()
    {
        InnerLocalPos = innerRT.localPosition;
        if (IsDebugMode)
        {
            DebugTime += Time.deltaTime;
        }
    }

    public void GenerateNotes()
    {
        foreach (NoteData values in NoteDataDic.Values)
        {
            Vector3 nom = new Vector3(values.x, values.y, 0f);

            if (nom == Vector3.zero)
            {
                Vector2 size = octagonRT.sizeDelta + octagonRT.sizeDelta * values.time;
                Instantiate(NotesObj[values.type], octagonRT.transform.position, Quaternion.identity, NeutralNotesFolder.transform).GetComponent<RectTransform>().sizeDelta = size;
            }
            else
            {
                Vector3 pos = nom * NoteSpeed * (values.time + OuterRadius / NoteSpeed);
                Instantiate(NotesObj[values.type + 4], octagonRT.transform.position + pos, Quaternion.identity, StickNotesFolder.transform);
            }
        }
    }

    public void OnMusicStart()
    {
        if (am.IsPlayingBGM == false)
        {
            textMode.SetText("");
            am.PlayBGM(0);
        }
    }

    // debugモードと通常モードの切替
    public void OnSwitchMode()
    {
        textMode.SetText("debug mode to make a score");
        IsDebugMode = IsDebugMode == true ? false : true;

        if (IsDebugMode == true)
        {
            am.PlayBGM(0);
            DebugTime = 0f;
            NoteDataDic.Clear();
            NeutralNotesFolder.SetActive(false);
            StickNotesFolder.SetActive(false);
        }
        else
        {
            rcm.WriteNoteData();
            NoteDataDic.Clear();
            textMode.SetText("");
            am.Stop();
            NeutralNotesFolder.SetActive(true);
            StickNotesFolder.SetActive(true);
        }
    }


}
