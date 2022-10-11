using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneratorController : MonoBehaviour
{
    public GameObject generatorGroup;
    private GamingDataController gamingDataController;


    private void Start()
    {
        
    }

    void Update()
    {
        if(!GamingDataController.GetInstance().isAlive())
        {
            Destroy(generatorGroup);
        }
    }
}
