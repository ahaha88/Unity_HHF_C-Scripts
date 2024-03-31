using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controls : MonoBehaviour
{
    private PlayerInput input;
    private Player player;

    // InputSystemのコールバック登録
    private void OnEnable()
    {
        player = GetComponent<Player>();
        input = GetComponent<PlayerInput>();

        RegisterCallback();
    }

    // InputSystemのコールバック解除（Controlsクラスに移動予定）
    private void OnDisable()
    {
        UnregisterCallback();
    }

    private void RegisterCallback()
    {
        input.actions["Punch"].started += ctx => player.OnAction(ctx, Player.State.Punch);
        input.actions["Punch"].performed += ctx => player.OnAction(ctx, Player.State.Punch);
        input.actions["Punch"].canceled += ctx => player.OnAction(ctx, Player.State.Punch);

        input.actions["Kick"].started += ctx => player.OnAction(ctx, Player.State.Kick);
        input.actions["Kick"].performed += ctx => player.OnAction(ctx, Player.State.Kick);
        input.actions["Kick"].canceled += ctx => player.OnAction(ctx, Player.State.Kick);

        /*
                input.actions["Throw"].started += ctx => player.OnThrow(ctx);
                input.actions["Throw"].performed += ctx => player.OnThrow(ctx);
                input.actions["Throw"].canceled += ctx => player.OnThrow(ctx);
        */

        input.actions["Move"].started += ctx => player.OnMove(ctx);
        input.actions["Move"].performed += ctx => player.OnMove(ctx);
        input.actions["Move"].canceled += ctx => player.OnMove(ctx);
    }

    private void UnregisterCallback()
    {
        input.actions["Punch"].started -= ctx => player.OnAction(ctx, Player.State.Punch);
        input.actions["Punch"].performed -= ctx => player.OnAction(ctx, Player.State.Punch);
        input.actions["Punch"].canceled -= ctx => player.OnAction(ctx, Player.State.Punch);

        input.actions["Kick"].started -= ctx => player.OnAction(ctx, Player.State.Kick);
        input.actions["Kick"].performed -= ctx => player.OnAction(ctx, Player.State.Kick);
        input.actions["Kick"].canceled -= ctx => player.OnAction(ctx, Player.State.Kick);

        /*
                input.actions["Throw"].started -= ctx => player.OnThrow(ctx);
                input.actions["Throw"].performed -= ctx => player.OnThrow(ctx);
                input.actions["Throw"].canceled -= ctx => player.OnThrow(ctx);
        */

        input.actions["Move"].started -= ctx => player.OnMove(ctx);
        input.actions["Move"].performed -= ctx => player.OnMove(ctx);
        input.actions["Move"].canceled -= ctx => player.OnMove(ctx);
    }
}
