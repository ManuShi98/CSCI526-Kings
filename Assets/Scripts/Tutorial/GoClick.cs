using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GoClick : MonoBehaviour
{
    public GameObject DialogForTutorial;

    public void OnClickForTutorial ()
    {
        DialogForTutorial.SetActive(true);
        Invoke("CloseDialog",4);
    }

    public void CloseDialog()
    {
        DialogForTutorial.SetActive(false);

    }

    void Start()
    {
        OnClickForTutorial();
    }
    // Update is called once per frame
    void Update()
    {

    }

}
