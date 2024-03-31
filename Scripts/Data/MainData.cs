using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 主にパラメーターを管理する静的クラス
/// </summary>

public static class MainData
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

    // 定数（同じシーン間で値が変化しない）
    public static float FIELD_END { get; private set; } = 8.5f;

    public static int MAX_POINT { get; private set; } = 2;

    public static float MAX_TIME { get; private set; } = 60f;

    public static float MAX_DIST { get; private set; } = 9f;

    public static float MIN_DIST { get; private set; } = 1.2f;

    public static Vector3[] START_POS { get; private set; } = { new Vector3(-3f, 1.985f, 0), new Vector3(3f, 1.985f, 0) };

    public static Vector3 CAMERA_START_POS { get; private set; } = new Vector3(0f, 4.5f, -5f);

    public static Vector3 CAMERA_START_ROT { get; private set; } = new Vector3(0f, 0f, 0f);
    
    public static Character[] CHARACTERS { get; private set; } = { Character.Bunny, Character.Bunny }; // 0:1Pのキャラ　1:2Pのキャラ

    public static int MAX_POP { get; private set; } = 1000;

    public static int MAX_STOMACH { get; private set; } = 1000;

    public static float DEAD_ZONE { get; private set; } = 0.4f; // 移動入力のデッドゾーン

    public static int SOUND_VOLUME { get; private set; } = 50;



    // 変数
    public static GameState currentGameState { get; set; } = GameState.Ready;

    public static string winner { get; set; } = "";

    public static float distance { get; set; }

    public static Vector3 midPoint { get; set; }

    public static float timeLimit { get; set; } // シーンマネージャーでアップデート

    public static int round {  get; set; }

    public static Dictionary<string, AttackData> attackDataDic = new Dictionary<string, AttackData>();

    public static Dictionary<string, CharacterData> characterDataDic = new Dictionary<string, CharacterData>();



}
