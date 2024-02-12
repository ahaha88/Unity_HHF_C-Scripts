using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controls : MonoBehaviour
{
    private PlayerControls controls;
    private Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    // InputSystemのコールバック登録
    private void Start()
    {
        controls = new PlayerControls();
        
        if (this.gameObject.tag == "1P")
        {
            controls.Player.Enable();
        }



        controls.Player.Punch.started += ctx => player.OnAction(ctx, Player.State.Punch);
        controls.Player.Punch.performed += ctx => player.OnAction(ctx, Player.State.Punch);
        controls.Player.Punch.canceled += ctx => player.OnAction(ctx, Player.State.Punch);

        controls.Player.Kick.started += ctx => player.OnAction(ctx, Player.State.Kick);
        controls.Player.Kick.performed += ctx => player.OnAction(ctx, Player.State.Kick);
        controls.Player.Kick.canceled += ctx => player.OnAction(ctx, Player.State.Kick);

        /*
                controls.Player.Throw.started += ctx => player.OnThrow(ctx);
                controls.Player.Throw.performed += ctx => player.OnThrow(ctx);
                controls.Player.Throw.canceled += ctx => player.OnThrow(ctx);
        */

        controls.Player.Move.started += ctx => player.OnMove(ctx);
        controls.Player.Move.performed += ctx => player.OnMove(ctx);
        controls.Player.Move.canceled += ctx => player.OnMove(ctx);

    }

    // InputSystemのコールバック解除（Controlsクラスに移動予定）
    private void OnDisable()
    {
        controls.Player.Punch.started -= ctx => player.OnAction(ctx, Player.State.Punch);
        controls.Player.Punch.performed -= ctx => player.OnAction(ctx, Player.State.Punch);
        controls.Player.Punch.canceled -= ctx => player.OnAction(ctx, Player.State.Punch);

        controls.Player.Kick.started -= ctx => player.OnAction(ctx, Player.State.Kick);
        controls.Player.Kick.performed -= ctx => player.OnAction(ctx, Player.State.Kick);
        controls.Player.Kick.canceled -= ctx => player.OnAction(ctx, Player.State.Kick);

        /*
                controls.Player.Throw.started -= ctx => player.OnThrow(ctx);
                controls.Player.Throw.performed -= ctx => player.OnThrow(ctx);
                controls.Player.Throw.canceled -= ctx => player.OnThrow(ctx);
        */

        controls.Player.Move.started -= ctx => player.OnMove(ctx);
        controls.Player.Move.performed -= ctx => player.OnMove(ctx);
        controls.Player.Move.canceled -= ctx => player.OnMove(ctx);


        controls.Disable();
    }
}
