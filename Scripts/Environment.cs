using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Environmentオブジェクトにアタッチ
/// 主にオブジェクトの設置やパラメーターの初期化、更新を行う
/// </summary>

public class Environment : MonoBehaviour
{ 
    // inspecter上でMain.Characterと同じ順に対応するprefab variantをセットしなければならないことに注意
    [SerializeField]
    private GameObject[] characters;

    private Slider[] HPbar = new Slider[2];
    public TextMeshProUGUI fpsText;

    private void Awake()
    {
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 60;
    }

    private void Start()
    {
        QualitySettings.vSyncCount = 0; // スクリーンの性能によってフレームレートが可変にならないように設定（エディタの設定でも同様にしている）
        Application.targetFrameRate = 60; // エディタの画面では30を下回ることがあるため注意

        InstantiateCaracters();
        Main.MainStart();

        HPbar[0] = GameObject.FindWithTag("HPbar_1P").GetComponent<Slider>();
        HPbar[1] = GameObject.FindWithTag("HPbar_2P").GetComponent<Slider>();
        

        HPbar[0].value = Main.players[0].hitPoint;
        HPbar[1].value = Main.players[1].hitPoint;

        fpsText = GameObject.FindWithTag("fps").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        Main.MainUpdate();

        HPbar[0].value = Main.players[0].hitPoint;
        HPbar[1].value = Main.players[1].hitPoint;

        fpsText.SetText(Time.deltaTime.ToString());
    }

    private void InstantiateCaracters()
    {
        Vector3 pos1 = new Vector3(Main.START_POS[0].x, Main.START_POS[0].y, Main.START_POS[0].z);
        Vector3 pos2 = new Vector3(Main.START_POS[1].x, Main.START_POS[1].y, Main.START_POS[1].z);

        Instantiate(characters[(int)Main.CHARACTERS[0]], pos1, Quaternion.Euler(0f,90f,0f)).tag = "1P";
        Instantiate(characters[(int)Main.CHARACTERS[1]], pos2, Quaternion.Euler(0f, -90f, 0f)).tag = "2P";
    }
}
