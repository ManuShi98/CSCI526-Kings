using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // int timeSpend = (int)(System.DateTime.Now - DataCollector.Instance.beginTime).TotalMilliseconds;
        // if(timeSpend % 10000 == 0){
        //     Send();
        // }
    }

    void Awake(){
        _sessionId = System.DateTime.Now.Ticks;
    }

    public void Send()
    {

       
        _gameTime = ((int)(System.DateTime.Now - Singleton.Instance.beginTime).TotalMilliseconds) / 1000;
        _originalMonsterNumber = Singleton.Instance.numOfOriginalMonster;
        _diedMonsterNumber = Singleton.Instance.numOfReachEndMonster;
        _totalCoins = Singleton.Instance.numOfCoins;
        _numOfReachEndMonster = Singleton.Instance.numOfReachEndMonster;
        _numOfSpringReachEndMonster = Singleton.Instance.numOfSpringReachEndMonster;
        _numOfSummerReachEndMonster = Singleton.Instance.numOfSummerReachEndMonster;
        _numOfFallReachEndMonster = Singleton.Instance.numOfFallReachEndMonster;
        _numOfWinterReachEndMonster = Singleton.Instance.numOfWinterReachEndMonster;
        _timeOfSpring = Singleton.Instance.timeOfSpring;
        _timeOfSummer = Singleton.Instance.timeOfSummer;
        _timeOfFall = Singleton.Instance.timeOfFall;
        _timeOfWinter = Singleton.Instance.timeOfWinter;

        
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