using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneController : MonoBehaviour
{

    string[] inGameSceneNames = { "Path", "Monster" };

    public void LoadLevel()
    {
        foreach (string name in inGameSceneNames)
        {
            SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
