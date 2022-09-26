using System.Collections;
using UnityEngine;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Runtime.InteropServices;



public class EnemySpawner : MonoBehaviour
{
  public Wave[] waves;
  public Transform START;
  public Path path;

  void Start()
  {
    foreach (Wave wave in waves)
    {
      Singleton.Instance.numOfOriginalMonster += wave.count;
    }
  }

  void Update()
  {
    // bool flag = true;
    // System.DateTime now = System.DateTime.Now;
    // int timeSpend = (int)(now - Singleton.Instance.beginTime).TotalMilliseconds;

    // if (timeSpend % 5000 == 0 && flag == true)
    // {
    //   Debug.Log("This game has running for " + (timeSpend / 1000) + "seconds");
    //   Debug.Log("The number of original monster is " + Singleton.Instance.numOfOriginalMonster);
    //   Debug.Log("The number of survival monster is " + Singleton.Instance.numOfSurviveMonster);
    //   Debug.Log("The number of miss monster is " + Singleton.Instance.numOfReachEndMonster);
    //   Debug.Log("The number of died monster is " + (Singleton.Instance.numOfOriginalMonster - Singleton.Instance.numOfSurviveMonster));
    //   Debug.Log("**************************************************************************************");

    //   dataUnit unit = new dataUnit()
    //   {
    //     runTime = (timeSpend / 1000),
    //     originalMonster = Singleton.Instance.numOfOriginalMonster,
    //     survivalMonster = Singleton.Instance.numOfSurviveMonster,
    //     missMonster = Singleton.Instance.numOfReachEndMonster,
    //     diedMonster = (Singleton.Instance.numOfOriginalMonster - Singleton.Instance.numOfSurviveMonster)
    //   };
    //   Singleton.Instance.list.Add(unit);
    // }

    // if (timeSpend % 10000 == 0)
    // {
    //   Debug.Log("the json file has stored in the desktop");
    //   flag = false;
    //   string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
    //   string json = JsonConvert.SerializeObject(Singleton.Instance.list, Formatting.Indented);
    //   Debug.Log(desktopPath);


    //   if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
    //   {
    //     // Windows 相关逻辑
    //     using (FileStream fs = new FileStream(string.Format("{0}\\info.json", desktopPath), FileMode.Create))
    //     {
    //       //写入
    //       using (StreamWriter sw = new StreamWriter(fs))
    //       {
    //         sw.WriteLine(json);
    //       }

    //     }
    //   }
    //   else
    //   {
    //     using (FileStream fs = new FileStream(string.Format("{0}/info.json", desktopPath), FileMode.Create))
    //     {
    //       //写入
    //       using (StreamWriter sw = new StreamWriter(fs))
    //       {
    //         sw.WriteLine(json);
    //       }

    //     }
    //   }


    // }


  }

  public void OnGenerateEnemyBtnClicked()
  {
    StartCoroutine(SpawnEnemy());
  }

  IEnumerator SpawnEnemy()
  {
    foreach (Wave wave in waves)
    {

      Singleton.Instance.numOfSurviveMonster += wave.count;
      for (int i = 0; i < wave.count; i++)
      {
        EnemyUnit newEnemy = GameObject.Instantiate(wave.enemyPrefab, START.position, Quaternion.identity).GetComponent<EnemyUnit>();
        newEnemy.SetPath(path);
        // GameObject.Instantiate(wave.enemyPrefab, START.position, Quaternion.identity);
        yield return new WaitForSeconds(wave.rate);
      }
    }
  }
}
