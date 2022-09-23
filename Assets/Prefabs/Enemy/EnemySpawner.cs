using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Wave[] waves;
    public Transform START;

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    void Update()
    {
        bool flag = true;
        System.DateTime now = System.DateTime.Now;
        int timeSpend = (int)(now - Singleton.Instance.beginTime).TotalMilliseconds;
        // Debug.Log(Singleton.Instance.numOfOriginalMonster);
        
        if(timeSpend % 5000 == 0 && flag == true){
            Debug.Log("This game has running for " + (timeSpend / 1000) + "seconds");
            Debug.Log("The number of original monster is " + Singleton.Instance.numOfOriginalMonster);
            Debug.Log("The number of survival monster is " + Singleton.Instance.numOfSurviveMonster);
            Debug.Log("The number of miss monster is " + Singleton.Instance.numOfReachEndMonster);
            Debug.Log("The number of died monster is " + (Singleton.Instance.numOfOriginalMonster - Singleton.Instance.numOfSurviveMonster));
            Debug.Log("**************************************************************************************");
            Debug.Log("");
            //    Debug.Log(timeSpend);
        }

        if(timeSpend == 600000){
            flag = false;
        }
    }

    IEnumerator SpawnEnemy()
    {
        foreach(Wave wave in waves)
        {
                Singleton.Instance.numOfOriginalMonster += wave.count;
                Singleton.Instance.numOfSurviveMonster += wave.count;
            for (int i = 0; i < wave.count; i++)
            {

                GameObject.Instantiate(wave.enemyPrefab, START.position, Quaternion.identity);
                yield return new WaitForSeconds(wave.rate);
            }
        }
    }
}
