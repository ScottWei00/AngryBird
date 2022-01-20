using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Screen.SetResolution(960, 600, false);
        Invoke("Load", 3f);
    }

    void Load()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
