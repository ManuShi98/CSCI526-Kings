using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class jumpPage : MonoBehaviour
{ 
    private static int lastTutorialIndex;
    private static int endGameTrigger;

    // Start is called before the first frame update
    void Start()
    {
        lastTutorialIndex = 4;
        endGameTrigger = -1;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void jump()
    {
        //need to change...(temp solution)
        if(SceneManager.GetActiveScene().buildIndex <= lastTutorialIndex)
        {
            GamingDataController.getInstance().health = 3;
            GamingDataController.getInstance().coins = 20;
            DataManager.Init();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if(SceneManager.GetActiveScene().buildIndex > lastTutorialIndex)
        {
          
            if(SceneManager.GetActiveScene().buildIndex == DataManager.level3Index)
            {
                DataManager.isPass = true;
                GamingDataController.getInstance().health = endGameTrigger;
            }
            else
            {
                Singleton.Instance.updateTime();
                DataManager.SumCurrentLevelData();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
        
        
    }

    public void returnMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void restart()
    {
        if(DataManager.currentLevelIndex == DataManager.level1Index)
        {
            SceneManager.LoadScene("Map");
        }
        else if(DataManager.currentLevelIndex == DataManager.level2Index)
        {
            SceneManager.LoadScene("level2");
        }
        else if(DataManager.currentLevelIndex == DataManager.level3Index)
        {
            SceneManager.LoadScene("level3");
        }
        
    }
}
