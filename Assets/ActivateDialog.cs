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
        if ((Singleton.Instance.curDiedMonster + Singleton.Instance.numOfReachEndMonster == Singleton.Instance.curOriginalMonster) && (Singleton.Instance.curOriginalMonster == Singleton.Instance.curMonsterNum))
            {
                Singleton.Instance.curDiedMonster = 0;
                Singleton.Instance.curMonsterNum = 0;
                Singleton.Instance.curOriginalMonster = 0;
                Singleton.Instance.curReachEndMonster = 0;
                dialogBox.SetActive(true);
            }
    }
}
