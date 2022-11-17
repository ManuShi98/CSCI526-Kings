using System.Collections;
using UnityEngine;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;


public class EnemySpawner : MonoBehaviour
{
  public Wave[] waves;
  public Transform START;
  public Path path;
  [SerializeField]
  private int waveInterval = 5;

  void Start()
  {
    foreach (Wave wave in waves)
    {
      Singleton.Instance.curOriginalMonster += wave.count;
    }
    
    }

  void Update()
  {
    
  }

  public void OnGenerateEnemyBtnClicked()
  {
    StartCoroutine(SpawnEnemy());
  }

  IEnumerator SpawnEnemy()
  {
    foreach (Wave wave in waves)
    {
      for (int i = 0; i < wave.count; i++)
      {
        Singleton.Instance.numOfOriginalMonster++;
                 
        Singleton.Instance.curMonsterNum++;
        EnemyUnit newEnemy = GameObject.Instantiate(wave.enemyPrefab, START.position, Quaternion.identity).GetComponent<EnemyUnit>();
        newEnemy.SetPath(path);
        yield return new WaitForSeconds(wave.rate);
      }
      yield return new WaitForSeconds(waveInterval);
    }
  }
}
