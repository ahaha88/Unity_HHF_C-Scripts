using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadFighting : MonoBehaviour
{
    public void OnLoadFighting()
    {
        SceneManager.LoadScene("Fighting");
    }

}
