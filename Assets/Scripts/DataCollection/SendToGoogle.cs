using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class SendToGoogle : MonoBehaviour
{
 
    //https://docs.google.com/forms/u/0/d/e/1FAIpQLSfD8V_JN0t_oowN-vi0JkNkBzZGJxBdYXgVsfuUfTJKtv5gzg/formResponse
    [SerializeField]
    private string URL;

    private long _sessionId;
    private int _gameTime;
    private int _totalCoins;
    private int _numOfReachEndMonster;
    private int _originalMonsterNumber;
    private int _diedMonsterNumber;

    private int _timeOfSpring;
    private int _timeOfSummer;
    private int _timeOfFall;
    private int _timeOfWinter;

    private int _numOfSpringReachEndMonster;
    private int _numOfSummerReachEndMonster;
    private int _numOfFallReachEndMonster;
    private int _numOfWinterReachEndMonster;
    private string _endLevel;
    private int _level1Time;
    private int _level2Time;
    private int _level3Time;
    private int _level4Time;
    private int _level5Time;
    private int _level6Time;
    private int _level7Time;

    private int _buttonClickLevel1Season;
    private int _buttonClickLevel2Season;
    private int _buttonClickLevel3Season;
    private int _buttonClickLevel4Season;
    private int _buttonClickLevel5Season;
    private int _buttonClickLevel6Season;
    private int _buttonClickLevel7Season;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        bool aliveTag = GamingDataController.GetInstance().isAlive();
        if(SceneManager.GetActiveScene().buildIndex >= DataManager.level1Index)
        {
            if (aliveTag == false && Singleton.Instance.isGameOver == false)
            {
            
                Singleton.Instance.updateTime();
                DataManager.SumCurrentLevelData();

                Singleton.Instance.isGameOver = true;

                
                DataManager.currentLevelIndex = SceneManager.GetActiveScene().name;
                    Debug.Log(DataManager.currentLevelIndex);
                    DataManager.endLevel = SceneManager.GetActiveScene().name;
                if(DataManager.currentLevelIndex == "level1")
                {
                    DataManager.level1Time += Singleton.Instance.totalTime;
                }
                else if(DataManager.currentLevelIndex == "level2")
                {
                    DataManager.level2Time += Singleton.Instance.totalTime;
                }
                else if(DataManager.currentLevelIndex == "level3")
                {
                    DataManager.level3Time += Singleton.Instance.totalTime;
                }
                else if (DataManager.currentLevelIndex == "level4")
                {
                    DataManager.level4Time += Singleton.Instance.totalTime;
                }
                else if (DataManager.currentLevelIndex == "level5")
                {
                    DataManager.level5Time += Singleton.Instance.totalTime;
                }
                else if (DataManager.currentLevelIndex == "level6")
                {
                    DataManager.level6Time += Singleton.Instance.totalTime;
                }
                else if (DataManager.currentLevelIndex == "level7")
                {
                    DataManager.level7Time += Singleton.Instance.totalTime;
                }

                Send();

                    //DataManager.Init();
                if (DataManager.isPass == true)
                {
                    SceneManager.LoadScene("Menu");
                    DataManager.isPass = false;
                }
                else
                {
                    SceneManager.LoadScene("GameOver");
                }
            

            }
        }
    }

    void Awake(){
        _sessionId = System.DateTime.Now.Ticks;
    }

    public void Send()
    {

       
        _gameTime = DataManager.totalTime;
        _originalMonsterNumber = DataManager.numOfOriginalMonster;
        _diedMonsterNumber = DataManager.numOfDiedMonster;
        _totalCoins = DataManager.numOfCoins;
        _numOfReachEndMonster = DataManager.numOfReachEndMonster;
        _numOfSpringReachEndMonster = DataManager.numOfSpringReachEndMonster;
        _numOfSummerReachEndMonster = DataManager.numOfSummerReachEndMonster;
        _numOfFallReachEndMonster = DataManager.numOfFallReachEndMonster;
        _numOfWinterReachEndMonster = DataManager.numOfWinterReachEndMonster;
        _timeOfSpring = DataManager.timeOfSpring;
        _timeOfSummer = DataManager.timeOfSummer;
        _timeOfFall = DataManager.timeOfFall;
        _timeOfWinter = DataManager.timeOfWinter;
        _endLevel = DataManager.endLevel;
        _level1Time = DataManager.level1Time;
        _level2Time = DataManager.level2Time;
        _level3Time = DataManager.level3Time;
        _level4Time = DataManager.level4Time;
        _level5Time = DataManager.level5Time;
        _level6Time = DataManager.level6Time;
        _level7Time = DataManager.level7Time;
        _buttonClickLevel1Season = DataManager.buttonClickLevel1Season;
        _buttonClickLevel2Season = DataManager.buttonClickLevel2Season;
        _buttonClickLevel3Season = DataManager.buttonClickLevel3Season;
        _buttonClickLevel4Season = DataManager.buttonClickLevel4Season;
        _buttonClickLevel5Season = DataManager.buttonClickLevel5Season;
        _buttonClickLevel6Season = DataManager.buttonClickLevel6Season;
        _buttonClickLevel7Season = DataManager.buttonClickLevel7Season;

        
        StartCoroutine(Post(_sessionId.ToString(), _gameTime.ToString(), _originalMonsterNumber.ToString(), _diedMonsterNumber.ToString(),
        _totalCoins.ToString(), _numOfReachEndMonster.ToString(), _numOfSpringReachEndMonster.ToString(), _numOfSummerReachEndMonster.ToString(),
        _numOfFallReachEndMonster.ToString(), _numOfWinterReachEndMonster.ToString(), _timeOfSpring.ToString(), _timeOfSummer.ToString(),
        _timeOfFall.ToString(), _timeOfWinter.ToString(), _endLevel.ToString(), _level1Time.ToString(), _level2Time.ToString(), _level3Time.ToString(),
        _level4Time.ToString(), _level5Time.ToString(), _level6Time.ToString(), _level7Time.ToString(), _buttonClickLevel1Season.ToString(),
        _buttonClickLevel2Season.ToString(), _buttonClickLevel3Season.ToString(), _buttonClickLevel4Season.ToString(), _buttonClickLevel5Season.ToString(),
        _buttonClickLevel6Season.ToString(), _buttonClickLevel7Season.ToString()));
    }

    private IEnumerator Post(string sessionID, string gameTime, string originalMonsterNumber, string diedMonsterNumber, string totalCoins,
    string numOfReachEndMonster, string numOfSpringReachEndMonster, string numOfSummerReachEndMonster, string numOfFallReachEndMonster,
    string numOfWinterReachEndMonster, string timeOfSpring, string timeOfSummer, string timeOfFall, string timeOfWinter, string endLevel,
    string level1Time, string level2Time, string level3Time, string level4Time, string level5Time, string level6Time, string level7Time,
    string buttonClickLevel1Season, string buttonClickLevel2Season, string buttonClickLevel3Season, string buttonClickLevel4Season,
    string buttonClickLevel5Season, string buttonClickLevel6Season, string buttonClickLevel7Season)
    {
        

        WWWForm form = new WWWForm();
        form.AddField("entry.585129582", sessionID);
        form.AddField("entry.1378326867", gameTime);
        form.AddField("entry.58821787", totalCoins);
        form.AddField("entry.434618556", originalMonsterNumber);
        form.AddField("entry.1436676327", diedMonsterNumber);
        form.AddField("entry.1930984140", numOfReachEndMonster);
        form.AddField("entry.1979413239", numOfSpringReachEndMonster);
        form.AddField("entry.565381325", numOfSummerReachEndMonster);
        form.AddField("entry.1633041290", numOfFallReachEndMonster);
        form.AddField("entry.310386478", numOfWinterReachEndMonster);
        form.AddField("entry.868565855", timeOfSpring);
        form.AddField("entry.1083367176", timeOfSummer);
        form.AddField("entry.750371371", timeOfFall);
        form.AddField("entry.759825660", timeOfWinter);
        form.AddField("entry.734859192", endLevel);
        form.AddField("entry.1118265377", level1Time);
        form.AddField("entry.223071512", level2Time);
        form.AddField("entry.857080875", level3Time);
        form.AddField("entry.1187604322", level4Time);
        form.AddField("entry.391897502", level5Time);
        form.AddField("entry.401723518", level6Time);
        form.AddField("entry.1675488209", level7Time);
        form.AddField("entry.779801300", buttonClickLevel1Season);
        form.AddField("entry.2114881390", buttonClickLevel2Season);
        form.AddField("entry.1595674512", buttonClickLevel3Season);
        form.AddField("entry.1407407311", buttonClickLevel4Season);
        form.AddField("entry.860121711", buttonClickLevel5Season);
        form.AddField("entry.725998613", buttonClickLevel6Season);
        form.AddField("entry.232639601", buttonClickLevel7Season);
        

        using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
        {
            yield return www.SendWebRequest();
            if(www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }
}