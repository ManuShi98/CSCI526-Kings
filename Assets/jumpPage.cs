using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SceneManagement;

public class jumpPage : MonoBehaviour
{
    private static int menuPage;
    private static int lastTutorialIndex;
    private static int gameOverIndex;
    private static int endGameTrigger;
    private static int level1Index;
    private static int level2Index;
    private static int level3Index;
    // Start is called before the first frame update
    void Start()
    {
        menuPage = 0;
        lastTutorialIndex = 3;
        gameOverIndex = 7;
        endGameTrigger = -1;
        level1Index = 4;
        level2Index = 5;
        level3Index = 6;
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if(SceneManager.GetActiveScene().buildIndex > lastTutorialIndex)
        {
          
            if(SceneManager.GetActiveScene().buildIndex == level3Index)
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
        if(DataManager.currentLevelIndex == level1Index)
        {
            SceneManager.LoadScene("Map");
        }
        else if(DataManager.currentLevelIndex == level2Index)
        {
            SceneManager.LoadScene("level2");
        }
        else if(DataManager.currentLevelIndex == level3Index)
        {
            SceneManager.LoadScene("level3");
        }
        
    }
}
