using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class RhythmInput : MonoBehaviour
{
    private MoveInnerCircle mic;
    private RhythmGameManager rgm;
    private PlayerInput playerInput;

    private void Awake()
    {
        mic = GetComponent<MoveInnerCircle>();
        rgm = transform.parent.gameObject.GetComponent<RhythmGameManager>();
        playerInput = GetComponent<PlayerInput>();

        playerInput.actions["LeftStick"].started += ctx => mic.OnMove(ctx);
        playerInput.actions["LeftStick"].performed += ctx => mic.OnMove(ctx);
        playerInput.actions["LeftStick"].canceled += ctx => mic.OnMove(ctx);

        playerInput.actions["NEWS"].started += ctx => mic.OnTap(ctx);
        playerInput.actions["NEWS"].performed += ctx => mic.OnTap(ctx);
        playerInput.actions["NEWS"].canceled += ctx => mic.OnTap(ctx);

        playerInput.actions["Start"].started += ctx => rgm.OnMusicStart();

        playerInput.actions["Debug"].started += ctx => rgm.OnSwitchMode();

    }

    private void OnDisable()
    {
        playerInput.actions["LeftStick"].started -= ctx => mic.OnMove(ctx);
        playerInput.actions["LeftStick"].performed -= ctx => mic.OnMove(ctx);
        playerInput.actions["LeftStick"].canceled -= ctx => mic.OnMove(ctx);

        playerInput.actions["NEWS"].started -= ctx => mic.OnTap(ctx);
        playerInput.actions["NEWS"].performed -= ctx => mic.OnTap(ctx);
        playerInput.actions["NEWS"].canceled -= ctx => mic.OnTap(ctx);

        playerInput.actions["Start"].started -= ctx => rgm.OnMusicStart();

        playerInput.actions["Debug"].started -= ctx => rgm.OnSwitchMode();
    }
}
