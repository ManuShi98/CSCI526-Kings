using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateDialog : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Singleton.Instance.numOfDiedMonster + Singleton.Instance.numOfReachEndMonster == Singleton.Instance.numOfOriginalMonster)
            {
                GameObject dialog = GameObject.Find("Dialog");
                dialog.SetActive(true);
        }
    }
}
