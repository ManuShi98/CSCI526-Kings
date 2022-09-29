using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class jumpPage : MonoBehaviour
{
    private static int menuPage;
    private static int lastTutorialIndex;
    private static int gameOverIndex;
    private static int lastLevel;
    private static int endGameTrigger;
    // Start is called before the first frame update
    void Start()
    {
        menuPage = 0;
        lastTutorialIndex = 3;
        gameOverIndex = 7;
        lastLevel = 6;
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if(SceneManager.GetActiveScene().buildIndex > lastTutorialIndex
            && SceneManager.GetActiveScene().buildIndex < gameOverIndex)
        {
          
            if(SceneManager.GetActiveScene().buildIndex == lastLevel)
            {
                GamingDataController.getInstance().health = endGameTrigger;
            }
            else
            {
                Singleton.Instance.updateTime();
                DataManager.sumCurrentLevelData();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
        
        
    }

    public void returnMenu()
    {
       
        SceneManager.LoadScene(menuPage);
    }
}
