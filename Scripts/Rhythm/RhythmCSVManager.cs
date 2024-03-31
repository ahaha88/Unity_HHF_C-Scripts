using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class RhythmCSVManager : MonoBehaviour
{
    private RhythmGameManager rgm;
    private StreamWriter sw;

    private string folderPath = "Assets/Resources/CSV/NoteData/";
    private string afterResourcesPath = "CSV/NoteData/";
    [SerializeField]
    private string readFileName;
    [SerializeField]
    private string writeFileName;
    

    private void Awake()
    {
        rgm = GetComponent<RhythmGameManager>();
    }

    public void WriteNoteData()
    {
        if (writeFileName == "")
        {
            Debug.Log("書き出すファイルを指定してください");
        }

        sw = new StreamWriter(folderPath + writeFileName + ".csv", false, Encoding.GetEncoding("utf-8"));

        string s = "";

        foreach (var dic in rgm.NoteDataDic.Values)
        {
            s = "";
            s += dic.index.ToString();
            s += ",";
            s += dic.x.ToString();
            s += ",";
            s += dic.y.ToString();
            s += ",";
            s += dic.type.ToString();
            s += ",";
            s += dic.time.ToString();
            s += ",";

            sw.WriteLine(s);
        }

        sw.Close();
    }

    public void LoadNoteData()
    {
        if (readFileName == "")
        {
            Debug.Log("読み込むファイルを指定してください");
        }

        int key = 0;
        string path = afterResourcesPath + readFileName;

        TextAsset file = Resources.Load(path) as TextAsset;

        StringReader sr = new StringReader(file.text);

        while (sr.Peek() != -1)
        {
            string[] values = sr.ReadLine().Split(",");

            int index = int.Parse(values[0]);
            float x = float.Parse(values[1]);
            float y = float.Parse(values[2]);
            int type = int.Parse(values[3]);
            float time = float.Parse(values[4]);

            NoteData nd = new NoteData(index, x, y, type, time);
            rgm.NoteDataDic.Add(key,nd);

            Debug.Log(" index: " + rgm.NoteDataDic[key].index + " x: " + rgm.NoteDataDic[key].x + " y: " + rgm.NoteDataDic[key].y + " type: " + rgm.NoteDataDic[key].type + " time: " + rgm.NoteDataDic[key].time);
            key++;
        }

        sr.Close();
    }
}
