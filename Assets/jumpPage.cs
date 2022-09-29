using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class jumpPage : MonoBehaviour
{
    private static int maxRounds;
    // Start is called before the first frame update
    void Start()
    {
        maxRounds = 6;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void jump()
    {
        if(SceneManager.GetActiveScene().buildIndex != maxRounds)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            returnMenu();
        }
    }

    public void returnMenu()
    {
        SceneManager.LoadScene(0);
    }
}
