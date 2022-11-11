using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateDialog : MonoBehaviour
{
    public GameObject dialogBox;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Singleton.Instance.curOriginalMonster);
        Debug.Log(Singleton.Instance.numOfDiedMonster + " + " + Singleton.Instance.numOfReachEndMonster + " + " + Singleton.Instance.curMonsterNum);
        if ((Singleton.Instance.curDiedMonster + Singleton.Instance.numOfReachEndMonster == Singleton.Instance.curOriginalMonster)  )
            {
                Singleton.Instance.curDiedMonster = 0;
                Singleton.Instance.curMonsterNum = 0;
                Singleton.Instance.curOriginalMonster = 0;
                Singleton.Instance.curReachEndMonster = 0;
                dialogBox.SetActive(true);
            }
    }
}
