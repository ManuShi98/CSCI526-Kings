using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TransformUtil;

public class TowerTutorial : MonoBehaviour, IEventHandler<CollidersClickEvent>, IEventHandler<UIClickEvent>
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
    private float firstX;
    private float secondX;
    private float thirdX;
    private float forthX;
    private float fifthX;

    private GameObject targetTower;
    public GameObject targetTower2;

    private void OnEnable()
    {
        EventBus.register<CollidersClickEvent>(this);
        EventBus.register<UIClickEvent>(this);
    }

    private void OnDisable()
    {
        EventBus.unregister<CollidersClickEvent>(this);
        EventBus.unregister<UIClickEvent>(this);
    }

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
        
        targetTower = GameObject.Find("TutorialTowerBase");
        if (firstArrow == null)
        {
            Debug.Log("Can't find first arrow for tutorial 2");
        } else
        {
            firstX = firstArrow.position.x;
        }
        if (secondArrow == null)
        {
            Debug.Log("Can't find second arrow for tutorial 2");
        }
        else
        {
            secondX = secondArrow.position.x;
        }
        if (thirdArrow == null)
        {
            Debug.Log("Can't find second arrow for tutorial 2");
        }
        else
        {
            thirdX = thirdArrow.position.x;
        }
        if (forthArrow == null)
        {
            Debug.Log("Can't find second arrow for tutorial 2");
        }
        else
        {
            forthX = forthArrow.position.x;
        }
        if (fifthArrow == null)
        {
            Debug.Log("Can't find second arrow for tutorial 2");
        }
        else
        {
            fifthX = fifthArrow.position.x;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (firstArrow != null && firstArrow.gameObject.activeSelf == true)
        {
            firstArrow.position = new Vector3(Mathf.PingPong(Time.time, 0.5f) + firstX, firstArrow.position.y, firstArrow.position.z);
        }
        if (secondArrow != null && secondArrow.gameObject.activeSelf == true)
        {
            secondArrow.position = new Vector3(Mathf.PingPong(Time.time, 0.5f) + secondX, secondArrow.position.y, secondArrow.position.z);
        }
        if (thirdArrow != null && thirdArrow.gameObject.activeSelf == true)
        {
            thirdArrow.position = new Vector3(Mathf.PingPong(Time.time, 0.5f) + thirdX, thirdArrow.position.y, thirdArrow.position.z);
        }
        if (forthArrow != null && forthArrow.gameObject.activeSelf == true)
        {
            forthArrow.position = new Vector3(Mathf.PingPong(Time.time, 0.5f) + forthX, forthArrow.position.y, forthArrow.position.z);
        }
        if (fifthArrow != null && fifthArrow.gameObject.activeSelf == true)
        {
            fifthArrow.position = new Vector3(Mathf.PingPong(Time.time, 0.5f) + fifthX, fifthArrow.position.y, fifthArrow.position.z);
        }
    }

    public void HandleEvent(CollidersClickEvent eventData)
    {
        if(eventData.obj == null)
        {
            if (step == 1)
            {
                step2.gameObject.SetActive(false);
                step1.gameObject.SetActive(true);
                step = 0;
            }
            else if (step == 3)
            {
                step4.gameObject.SetActive(false);
                step3.gameObject.SetActive(true);
                step = 2;
            }
        }
        else if (eventData.obj == targetTower)
        {
            if(step==0)
            {
                step1.gameObject.SetActive(false);
                step2.gameObject.SetActive(true);
                step = 1;
            } 
        }
        else if(eventData.obj.tag == "Tower")
        {
            if (step == 2)
            {
                step3.gameObject.SetActive(false);
                step4.gameObject.SetActive(true);
                step = 3;
            }
        }
    }

    public void HandleEvent(UIClickEvent eventData)
    {
        if (eventData.obj != null && eventData.obj.tag == "ClickableIcon")
        {
            if(step==1)
            {
                step2.gameObject.SetActive(false);
                step3.gameObject.SetActive(true);
                step = 2;
            }
            else if(step == 3)
            {
                step4.gameObject.SetActive(false);
                step = 4;
            }
        }
    }
}
