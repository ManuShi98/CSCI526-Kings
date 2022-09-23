using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



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
        
        if(timeSpend % 5000 == 0 && flag == true){
            // Debug.Log("This game has running for " + (timeSpend / 1000) + "seconds");
            // Debug.Log("The number of original monster is " + Singleton.Instance.numOfOriginalMonster);
            // Debug.Log("The number of survival monster is " + Singleton.Instance.numOfSurviveMonster);
            // Debug.Log("The number of miss monster is " + Singleton.Instance.numOfReachEndMonster);
            // Debug.Log("The number of died monster is " + (Singleton.Instance.numOfOriginalMonster - Singleton.Instance.numOfSurviveMonster));
            // Debug.Log("**************************************************************************************");
            
            dataUnit unit = new dataUnit()
            {
                runTime = (timeSpend / 1000),
                originalMonster = Singleton.Instance.numOfOriginalMonster,
                survivalMonster = Singleton.Instance.numOfSurviveMonster,
                missMonster = Singleton.Instance.numOfReachEndMonster,
                diedMonster = (Singleton.Instance.numOfOriginalMonster - Singleton.Instance.numOfSurviveMonster)
            };
            Singleton.Instance.list.Add(unit);
        }

        if(timeSpend == 10000){
            Debug.Log("hywhahaha");
            flag = false;
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string json = JsonConvert.SerializeObject(Singleton.Instance.list, Formatting.Indented);
            Debug.Log(desktopPath);
            using (FileStream fs = new FileStream(string.Format("{0}\\info.json", desktopPath), FileMode.Create))
            {
                //写入
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine(json);
                }

            }
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
