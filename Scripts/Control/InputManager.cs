using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// ローカル対戦におけるプレイヤーが参加したときの処理
/// </summary>

public class InputManager : MonoBehaviour
{
    private PlayerInputManager pim;
    [SerializeField]
    private GameObject[] characters;

    void Start()
    {
        pim = GetComponent<PlayerInputManager>();

        if ( pim == null)
        {
            Debug.Log("pimがnull");
        }
    }

    // プレイヤーが参加したときにUnityEventで呼ばれる
    public void OnJoined(PlayerInput playerInput)
    {
        Debug.Log(playerInput.user.index + "が参加");
        if (playerInput.user.index == 0)
        {
            //pim.playerPrefab = characters[(int)Main.CHARACTERS[0]];
        }
        if (playerInput.user.index == 1)
        {
            //pim.playerPrefab = characters[];
        }
    }

}
