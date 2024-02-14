using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// このクラスは単にPlayerStateに関するスクリプトを配列に格納するクラスである
/// inspectorで格納し、Player.csのenumであるStatesと同じ順番に格納しなければならない
/// </summary>


[System.Serializable]
public class PlayerStateScripts : MonoBehaviour
{
    public PlayerState[] Scripts = new PlayerState[10];

}
