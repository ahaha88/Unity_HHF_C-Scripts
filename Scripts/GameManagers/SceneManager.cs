using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Environmentオブジェクトにアタッチ
/// 主にオブジェクトの設置やパラメーターの初期化、更新を行う
/// </summary>

public class SceneManager : MonoBehaviour
{ 
    // inspecter上でMain.Characterと同じ順に対応するprefab variantをセットしなければならないことに注意
    [SerializeField]
    private GameObject[] characters;
    private TextMeshProUGUI fpsText;
    private TextMeshProUGUI timerText;
    private TextMeshProUGUI winnerText;

    private bool isStopTime = false;

    private void Awake()
    {
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 60;
    }

    private void Start()
    {
        QualitySettings.vSyncCount = 0; // スクリーンの性能によってフレームレートが可変にならないように設定（エディタの設定でも同様にしている）
        Application.targetFrameRate = 60; // エディタの画面では60を下回ることがあるため注意

        InstantiateCaracters();
        Main.MainStart();

        fpsText = GameObject.FindWithTag("fps").GetComponent<TextMeshProUGUI>();
        timerText = GameObject.FindWithTag("Timer").GetComponent<TextMeshProUGUI>();
        winnerText = GameObject.Find("ShowWinner").GetComponent<TextMeshProUGUI>();

    }

    private void Update()
    {
        Main.MainUpdate();

        if (isStopTime == false)
            TimeLimitUpdate();

        fpsText.SetText(Time.deltaTime.ToString());
        timerText.SetText(Mathf.Ceil(Main.timeLimit).ToString());
    }

    private void InstantiateCaracters()
    {
        Vector3 pos1 = new Vector3(Main.START_POS[0].x, Main.START_POS[0].y, Main.START_POS[0].z);
        Vector3 pos2 = new Vector3(Main.START_POS[1].x, Main.START_POS[1].y, Main.START_POS[1].z);

        Instantiate(characters[(int)Main.CHARACTERS[0]], pos1, Quaternion.Euler(0f,90f,0f)).tag = "1P";
        Instantiate(characters[(int)Main.CHARACTERS[1]], pos2, Quaternion.Euler(0f, -90f, 0f)).tag = "2P";
    }

    private void DestroyCharacters()
    {
        Destroy(Main.players[0].gameObject);
        Destroy(Main.players[1].gameObject);
    }


    // 負けたプレイヤー(stateがLoseになったとき)が渡される
    public void Judge(Player loser)
    {
        if (Main.timeLimit <= 0f)
            return;

        Player winner = loser.PLAYER_NUMBER == 1 ? Main.players[1] : Main.players[0];
        winner.SetState(Player.State.Win);
        winnerText.SetText(winner.PLAYER_NUMBER + "P WIN");
        isStopTime = true;

        Invoke(nameof(NewRound), 3f);
    }

    private void TimeUp()
    {
        if (Main.players[0].hitPoint < Main.players[1].hitPoint)
        {
            Main.players[0].SetState(Player.State.Lose);
            Main.players[1].SetState(Player.State.Win);

            winnerText.SetText("2P WIN");
        }
        else if (Main.players[0].hitPoint > Main.players[1].hitPoint)
        {
            Main.players[0].SetState(Player.State.Win);
            Main.players[1].SetState(Player.State.Lose);
            winnerText.SetText("1P WIN");
        }
        else
        {
            Main.players[0].SetState(Player.State.Lose);
            Main.players[1].SetState(Player.State.Lose);
            winnerText.SetText("DRAW");
        }

        Invoke(nameof(NewRound), 3f);
    }

    public void TimeLimitUpdate()
    {
        if (Main.timeLimit <= 0f)
        {
            TimeUp();
        }
        else
        {
            Main.timeLimit -= Time.deltaTime;
            Debug.Log(Main.timeLimit);
        }

        if (Main.timeLimit <= 3f)
        {
            timerText.color = Color.red; 
        }
    }

    private void NewRound()
    {
        Main.players[0].transform.position = Main.START_POS[0];
        Main.players[1].transform.position = Main.START_POS[1];

        Main.players[0].SetState(Player.State.Idle);
        Main.players[1].SetState(Player.State.Idle);

        Main.players[0].hitPoint = 10000;
        Main.players[1].hitPoint = 10000;

        Main.timeLimit = Main.MAX_TIME;
        isStopTime = false;
        
        Time.timeScale = 1f;
        winnerText.SetText("");
        timerText.color = Color.white;

        Main.round++;

    }
}
