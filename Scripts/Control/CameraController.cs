using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Camera cam;

    Vector3 initialPos;

    void Start()
    {
        cam = GetComponent<Camera>();

        initialPos = MainData.CAMERA_START_POS;
        this.transform.eulerAngles = MainData.CAMERA_START_ROT;
    }

    void Update()
    {
        if (MainData.currentGameState == MainData.GameState.Ready)
        {
            this.transform.position = initialPos;
        }
        if (MainData.currentGameState == MainData.GameState.NowFighting)
        {
            this.gameObject.transform.position = new Vector3(MainData.midPoint.x, MainData.midPoint.y + 2.5f, initialPos.z);
        }
    }
}
