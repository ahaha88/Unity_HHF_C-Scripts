using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private FightingGameManager fgm; 

    // inspecter上でセット
    [SerializeField] private Slider[] HPbar = new Slider[2];
    [SerializeField] private Image[] POPs = new Image[2];
    [SerializeField] private Image[] Stomachs = new Image[2];
    [SerializeField] private Image[] Point_1P = new Image[2];
    [SerializeField] private Image[] Point_2P = new Image[2];

    [SerializeField] private TextMeshProUGUI fpsText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI judgeText;
    [SerializeField] private TextMeshProUGUI[] playerInfos = new TextMeshProUGUI[2];
    [SerializeField] private TextMeshProUGUI[] plessText = new TextMeshProUGUI[2];

    private void Start()
    {
        fgm = GetComponent<FightingGameManager>();
    }

    private void Update()
    {
        HPbar[0].value = fgm.players[0].status.hitPoint;
        HPbar[1].value = fgm.players[1].status.hitPoint;

        POPs[0].fillAmount = 1.0f * fgm.players[0].status.popularity / MainData.MAX_POP;
        POPs[1].fillAmount = 1.0f * fgm.players[1].status.popularity / MainData.MAX_POP;

        Stomachs[0].fillAmount = 1.0f * fgm.players[0].status.stomack / MainData.MAX_STOMACH;
        Stomachs[1].fillAmount = 1.0f * fgm.players[1].status.stomack / MainData.MAX_STOMACH;

        if (MainData.currentGameState != MainData.GameState.Ready)
        {
            plessText[0].SetText("");
            plessText[1].SetText("");
        }

        fpsText.SetText(Time.deltaTime.ToString());
        timerText.SetText(Mathf.Ceil(MainData.timeLimit).ToString());


        playerInfos[0].SetText("1P State:" + fgm.players[0].currentState.ToString() + "\nAttackName: " + fgm.players[0].currentAttackTag);
        playerInfos[1].SetText("2P State:" + fgm.players[1].currentState.ToString() + "\nAttackName: " + fgm.players[1].currentAttackTag);

        if (MainData.timeLimit <= 3f)
        {
            timerText.color = Color.red;
        }
    }

    public void SetJudgeText(string text)
    {
        judgeText.SetText(text);
    }

    public void SetTimerColor(Color color) 
    {
        timerText.color = color;
    }

    public void SetPoint(int player, int num)
    {
        num--;

        if (player == 1)
        {
            Point_1P[num].color = Color.red;
        }
        else
        {
            Point_2P[num].color = Color.red;
        }
    }
}
