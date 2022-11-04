using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TransformUtil;

public class TutorialDataPanel : MonoBehaviour
{
    private int step;

    private Transform step1;
    private Transform step2;
    private Transform step3;
    private Transform step4;
    private Transform firstArrow;
    private Transform secondArrow;
    private Transform thirdArrow;
    private Transform forthArrow;
    private Transform fifthArrow;
    private Transform sixthArrow;
    private float firstY;
    private float secondY;
    private float thirdY;
    private float forthY;
    private float fifthY;
    private float sixthY;

    private GameObject TutorialLetsgoBtn;

    // Start is called before the first frame update
    void Start()
    {
        step = 0;
        step1 = transform.Find("Step1");
        step2 = transform.Find("Step2");
        step3 = transform.Find("Step3");
        step4 = transform.Find("Step4");
        firstArrow = transform.FindDeepChild("Arrow 1");
        secondArrow = transform.FindDeepChild("Arrow 2");
        thirdArrow = transform.FindDeepChild("Arrow 3");
        forthArrow = transform.FindDeepChild("Arrow 4");
        fifthArrow = transform.FindDeepChild("Arrow 5");
        sixthArrow = transform.FindDeepChild("Arrow 6");

        if (firstArrow == null)
        {
            Debug.Log("Can't find first arrow for tutorial 1");
        } else
        {
            firstY = firstArrow.position.y;
        }
        if (secondArrow == null)
        {
            Debug.Log("Can't find second arrow for tutorial 1");
        }
        else
        {
            secondY = secondArrow.position.y;
        }
        if (thirdArrow == null)
        {
            Debug.Log("Can't find third arrow for tutorial 1");
        }
        else
        {
            thirdY = thirdArrow.position.y;
        }
        if (forthArrow == null)
        {
            Debug.Log("Can't find fourth arrow for tutorial 1");
        }
        else
        {
            forthY = forthArrow.position.y;
        }
        if (fifthArrow == null)
        {
            Debug.Log("Can't find fifth arrow for tutorial 1");
        }
        else
        {
            fifthY = fifthArrow.position.y;
        }
        if (sixthArrow == null)
        {
            Debug.Log("Can't find six arrow for tutorial 1");
        }
        else
        {
            sixthY = sixthArrow.position.y;
        }
    }

    // Update is called once per frame
    void Update()
    {
        TutorialLetsgoBtn = GameObject.Find("ReadyBtnForGame");

        DestroyBtnTrue ();

        if (firstArrow != null && firstArrow.gameObject.activeSelf == true)
        {
            firstArrow.position = new Vector3(firstArrow.position.x, Mathf.PingPong(Time.time, 0.5f) + firstY, firstArrow.position.z);
        }
        if (secondArrow != null && secondArrow.gameObject.activeSelf == true)
        {
            secondArrow.position = new Vector3(secondArrow.position.x, Mathf.PingPong(Time.time, 0.5f) + secondY, secondArrow.position.z);
        }
        if (thirdArrow != null && thirdArrow.gameObject.activeSelf == true)
        {
            thirdArrow.position = new Vector3(thirdArrow.position.x, Mathf.PingPong(Time.time, 0.5f) + thirdY, thirdArrow.position.z);
        }
        if (forthArrow != null && forthArrow.gameObject.activeSelf == true)
        {
            forthArrow.position = new Vector3(forthArrow.position.x, Mathf.PingPong(Time.time, 0.5f) + forthY, forthArrow.position.z);
        }
        if (fifthArrow != null && fifthArrow.gameObject.activeSelf == true)
        {
            fifthArrow.position = new Vector3(fifthArrow.position.x, Mathf.PingPong(Time.time, 0.5f) + fifthY, fifthArrow.position.z);
        }
        if (sixthArrow != null && sixthArrow.gameObject.activeSelf == true)
        {
            sixthArrow.position = new Vector3(sixthArrow.position.x, Mathf.PingPong(Time.time, 0.5f) + sixthY, sixthArrow.position.z);
        }
    }

    public void CloseForOtherArrow()
    {
        step2.gameObject.SetActive(false);
        step3.gameObject.SetActive(false);
        step4.gameObject.SetActive(false);
    }

    public void DestroyBtnTrue () 
    {
        if (TutorialLetsgoBtn == null)
        {
            Debug.Log("1");
            if(step==0)
            {
                step1.gameObject.SetActive(false);
                step2.gameObject.SetActive(true);
                step3.gameObject.SetActive(true);
                step4.gameObject.SetActive(true);
                step = 1;
            }
            Invoke("CloseForOtherArrow",6);
        }
    }
}
