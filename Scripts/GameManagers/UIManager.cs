using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private Slider[] HPbar = new Slider[2];

    private void Awake()
    {
        HPbar[0] = GameObject.FindWithTag("HPbar_1P").GetComponent<Slider>();
        HPbar[1] = GameObject.FindWithTag("HPbar_2P").GetComponent<Slider>();
    }

    private void Start()
    {
        if (HPbar[0] != null && HPbar[1] != null)
        {
            HPbar[0].value = Main.players[0].hitPoint;
            HPbar[1].value = Main.players[1].hitPoint;
        }

    }

    private void Update()
    {
        HPbar[0].value = Main.players[0].hitPoint;
        HPbar[1].value = Main.players[1].hitPoint;

    }
}
