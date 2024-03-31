using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI winnerText;

    private void Start()
    {
        winnerText.SetText("Winner "+ MainData.winner);
    }
}
