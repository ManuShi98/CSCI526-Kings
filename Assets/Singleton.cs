using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
// public sealed class Singleton
{
    public int numOfSurviveMonster = 0;
    public int numOfOriginalMonster = 0;
    public int numOfReachEndMonster = 0;
    public bool globalStatus = true;
    public System.DateTime beginTime = System.DateTime.Now;
    public List<dataUnit> list = new List<dataUnit>();


    private static Singleton instance;

    public static Singleton Instance{
        get{
            if(instance == null)
            {
                GameObject obj = new GameObject();
                obj.name = "Singleton";
                instance = obj.AddComponent<Singleton>();
            }
            return instance;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Awake()
    {
        instance = this;
    }
}
