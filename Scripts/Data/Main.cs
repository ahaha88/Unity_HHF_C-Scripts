using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 主にパラメーターを管理する静的クラス
/// </summary>

public static class Main
{
    public enum Character
    {
        Bunny,
    }

    public enum GameState
    {
        Ready,       // 試合開始前
        NowFighting, // 戦闘中
        Finished,    // 試合終了
        Pause        // ポーズ
    }

    // 動的パラメータの参照に必要 以下の配列では０番目に１P、１番目に２Pの情報が格納されていることに注意
    public static Player[] players = new Player[2];
    public static CSVManager csvm;



    // 定数（同じシーン間で値が変化しない）
    public static float FIELD_END { get; private set; } = 8.5f;

    public static float MAX_TIME { get; private set; } = 60f;

    public static float MAX_DIST { get; private set; } = 9f;

    public static float MIN_DIST { get; private set; } = 1.2f;

    public static Vector3[] START_POS { get; private set; } = { new Vector3(-3f, 1.985f, 0), new Vector3(3f, 1.985f, 0) };
    
    public static Character[] CHARACTERS { get; private set; } = { Character.Bunny, Character.Bunny }; // 0:1Pのキャラ　1:2Pのキャラ

    public static float DEAD_ZONE { get; private set; } = 0.4f; // 移動入力のデッドゾーン

    public static int SOUND_VOLUME { get; private set; } = 50;



    // 変数
    public static GameState currentGameState { get; set; }
    public static float distance { get; private set; }

    public static Vector3 midPoint { get; private set; }

    public static float timeLimit { get; set; } // シーンマネージャーでアップデート

    public static int round {  get; set; } 



    // 以下のメソッドはEnvironmentクラスのStart()とUpdate()で実行される。
    public static void MainStart()
    {
        csvm = GameObject.FindWithTag("CSVManager").GetComponent<CSVManager>();

        players[0] = GameObject.FindWithTag("1P").GetComponent<Player>();
        players[1] = GameObject.FindWithTag("2P").GetComponent<Player>();
        
        if (players[0] == null || players[1] == null)
        {
            Debug.Log("Main.player1 / 2がnull");
        }
        else
        {
            players[0].isLeft = players[0].transform.position.x <= players[1].transform.position.x ? true : false;
            players[1].isLeft = players[0].isLeft == true ? false : true;
            players[0].wasLeft = players[0].isLeft;
            players[1].wasLeft = players[1].isLeft;

            distance = Mathf.Abs(players[0].transform.position.x - players[1].transform.position.x);
            midPoint = new Vector3((players[0].transform.position.x + players[1].transform.position.x) / 2f, (players[0].transform.position.y + players[1].transform.position.y) / 2f, 0f);

            timeLimit = MAX_TIME;
        }
    }

    public static void MainUpdate()
    {
        distance = Mathf.Abs(players[0].transform.position.x - players[1].transform.position.x);
        midPoint = new Vector3((players[0].transform.position.x + players[1].transform.position.x) / 2f, (players[0].transform.position.y + players[1].transform.position.y) / 2f, 0f);

        players[0].isLeft = players[0].transform.position.x <= players[1].transform.position.x ? true : false;
        players[1].isLeft = players[0].isLeft == true ? false : true;
    }
}
