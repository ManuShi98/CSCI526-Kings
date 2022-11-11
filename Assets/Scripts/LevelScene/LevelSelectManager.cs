using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectManager : MonoBehaviour
{
    public GameObject levelSelectMenu;
    Button[] levelSelectButtons;
    int unlockedLevelIndex;

    // Start is called before the first frame update
    void Start()
    {
        unlockedLevelIndex = PlayerPrefs.GetInt("unlockedLevelIndex");
        levelSelectButtons = new Button[levelSelectMenu.transform.childCount];
        for (int i = 0; i < levelSelectMenu.transform.childCount; i++)
        {
            levelSelectButtons[i] = levelSelectMenu.transform.GetChild(i).GetComponent<Button>();
        }

        //Setup those buttons and interactable
        for (int i = 0; i < levelSelectButtons.Length; i++)
        {
            levelSelectButtons[i].interactable = false;
        }

        //Setup interactable and setup onclick listener
        for (int i = 0; i < unlockedLevelIndex + 1; i++)
        {
            levelSelectButtons[i].interactable = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // DataManager.SumCurrentLevelData();
        // DataManager.currentLevelIndex = SceneManager.GetActiveScene().name;
        // Debug.Log(unlockedLevelIndex);
        // DataManager.endLevel = SceneManager.GetActiveScene().name;

        //logic
        // if(DataManager.currentLevelIndex > unlockedLevelIndex)
        // {
        //     unlockedLevelIndex = DataManager.currentLevelIndex;
        //     PlayerPrefs.SetInt("unlockedLevelIndex",unlockedLevelIndex);
        // }

        // if(DataManager.currentLevelIndex == "level1")
        // {

        // }
        // else if(DataManager.currentLevelIndex == "level2")
        // {
        //      unlockedLevelIndex = 2;
        //      PlayerPrefs.SetInt("unlockedLevelIndex",unlockedLevelIndex);
        // }
        // else if(DataManager.currentLevelIndex == "level3")
        // {
        //     unlockedLevelIndex = 3;
        //     PlayerPrefs.SetInt("unlockedLevelIndex",unlockedLevelIndex);
        // }
        // else if (DataManager.currentLevelIndex == "level4")
        // {
        //     unlockedLevelIndex = 4;
        //     PlayerPrefs.SetInt("unlockedLevelIndex",unlockedLevelIndex);
        // }
        // else if (DataManager.currentLevelIndex == "level5")
        // {
        //     unlockedLevelIndex = 5;
        //     PlayerPrefs.SetInt("unlockedLevelIndex",unlockedLevelIndex);
        // }
        // else if (DataManager.currentLevelIndex == "level6")
        // {
        //     unlockedLevelIndex = 6;
        //     PlayerPrefs.SetInt("unlockedLevelIndex",unlockedLevelIndex);
        // }
        // else if (DataManager.currentLevelIndex == "level7")
        // {
        //     //unlock all levels
        //     unlockedLevelIndex = 6;
        //     PlayerPrefs.SetInt("unlockedLevelIndex",unlockedLevelIndex);
        // }
    }
    public void ResetBtn()
    {
        PlayerPrefs.SetInt("unlockedLevelIndex",0);
    }
    public void GetAllBtn()
    {
        PlayerPrefs.SetInt("unlockedLevelIndex",6);
    }
}
