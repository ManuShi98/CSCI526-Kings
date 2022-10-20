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
        lastTutorialIndex = 10;
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
            GamingDataController.GetInstance().health = 3;
            GamingDataController.GetInstance().coins = 20;
            DataManager.Init();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if(SceneManager.GetActiveScene().buildIndex > lastTutorialIndex)
        {
          
            if(DataManager.currentLevelIndex == "level7")
            {
                DataManager.isPass = true;
                GamingDataController.GetInstance().health = endGameTrigger;
            }
            else
            {
                Singleton.Instance.updateTime();
                DataManager.SumCurrentLevelData();
                recordLevelTime();
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
        Debug.Log(SceneManager.GetActiveScene().name);
        if(DataManager.currentLevelIndex == "level1")
        {
            SceneManager.LoadScene("level1");
        }
        else if(DataManager.currentLevelIndex == "level2")
        {
            SceneManager.LoadScene("level2");
        }
        else if (DataManager.currentLevelIndex == "level3")
        {
            SceneManager.LoadScene("level3");
        }
        else if (DataManager.currentLevelIndex == "level4")
        {
            SceneManager.LoadScene("level4");
        }
        else if (DataManager.currentLevelIndex == "level5")
        {
            SceneManager.LoadScene("level5");
        }
        else if (DataManager.currentLevelIndex == "level6")
        {
            SceneManager.LoadScene("level6");
        }
        else if (DataManager.currentLevelIndex == "level7")
        {
            SceneManager.LoadScene("level7");
        }

    }

    private void recordLevelTime()
    {
        if (SceneManager.GetActiveScene().name == "level1")
        {
            DataManager.level1Time += Singleton.Instance.totalTime;
        }
        else if (SceneManager.GetActiveScene().name == "level2")
        {
            DataManager.level2Time += Singleton.Instance.totalTime;
        }
        else if (SceneManager.GetActiveScene().name == "level3")
        {
            DataManager.level3Time += Singleton.Instance.totalTime;
        }
        else if (SceneManager.GetActiveScene().name == "level4")
        {
            DataManager.level4Time += Singleton.Instance.totalTime;
        }
        else if (SceneManager.GetActiveScene().name == "level5")
        {
            DataManager.level5Time += Singleton.Instance.totalTime;
        }
        else if (SceneManager.GetActiveScene().name == "level6")
        {
            DataManager.level6Time += Singleton.Instance.totalTime;
        }
        else if (SceneManager.GetActiveScene().name == "level7")
        {
            DataManager.level7Time += Singleton.Instance.totalTime;
        }
    }
}
