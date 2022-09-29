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

    private int gameOverPageIndex; 

    // Start is called before the first frame update
    void Start()
    {
        gameOverPageIndex = 7;
    }

    // Update is called once per frame
    void Update()
    {
        bool aliveTag = GamingDataController.getInstance().isAlive();
        if (aliveTag == false && Singleton.Instance.isGameOver == false)
        {
            
            Singleton.Instance.updateTime();
            DataManager.sumCurrentLevelData();

            Singleton.Instance.isGameOver = true;



            //Debug.Log("numOfOriginalMonster: " + Singleton.Instance.numOfOriginalMonster);
            //Debug.Log("totalTime: " + Singleton.Instance.totalTime);
            //Debug.Log("numOfCoins: " + Singleton.Instance.numOfCoins);
            //Debug.Log("numOfDiedMonster: " + Singleton.Instance.numOfDiedMonster);
            //Debug.Log("numOfReachEndMonster: " + Singleton.Instance.numOfReachEndMonster);
            //Debug.Log("timeOfSpring: " + Singleton.Instance.timeOfSpring);
            //Debug.Log("timeOfSummer: " + Singleton.Instance.timeOfSummer);
            //Debug.Log("timeOfFall: " + Singleton.Instance.timeOfFall);
            //Debug.Log("timeOfWinter: " + Singleton.Instance.timeOfWinter);
            //Debug.Log("numOfSpringReachEndMonster: " + Singleton.Instance.numOfSpringReachEndMonster);
            //Debug.Log("numOfSummerReachEndMonster: " + Singleton.Instance.numOfSummerReachEndMonster);
            //Debug.Log("numOfFallReachEndMonster: " + Singleton.Instance.numOfFallReachEndMonster);
            //Debug.Log("numOfWinterReachEndMonster: " + Singleton.Instance.numOfWinterReachEndMonster);
            Send();
            SceneManager.LoadScene(gameOverPageIndex);

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

        
        StartCoroutine(Post(_sessionId.ToString(), _gameTime.ToString(), _originalMonsterNumber.ToString(), _diedMonsterNumber.ToString(),
        _totalCoins.ToString(), _numOfReachEndMonster.ToString(), _numOfSpringReachEndMonster.ToString(), _numOfSummerReachEndMonster.ToString(),
        _numOfFallReachEndMonster.ToString(), _numOfWinterReachEndMonster.ToString(), _timeOfSpring.ToString(), _timeOfSummer.ToString(),
        _timeOfFall.ToString(), _timeOfWinter.ToString()));
    }

    private IEnumerator Post(string sessionID, string gameTime, string originalMonsterNumber, string diedMonsterNumber, string totalCoins,
    string numOfReachEndMonster, string numOfSpringReachEndMonster, string numOfSummerReachEndMonster, string numOfFallReachEndMonster,
    string numOfWinterReachEndMonster, string timeOfSpring, string timeOfSummer, string timeOfFall, string timeOfWinter)
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

        using(UnityWebRequest www = UnityWebRequest.Post(URL, form))
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