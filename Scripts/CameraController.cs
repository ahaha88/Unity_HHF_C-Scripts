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

        initialPos = this.gameObject.transform.position;
    }

    void Update()
    {
        this.gameObject.transform.position = new Vector3(Main.midPoint.x, Main.midPoint.y + 2.5f, initialPos.z);
    }
}
