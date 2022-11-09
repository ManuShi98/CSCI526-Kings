using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowEnemy : EnemyUnit, IEventHandler<SeasonChangeEvent>
{
    [SerializeField]
    private GameObject Snowman;
    [SerializeField]
    private GameObject LeftEye;
    [SerializeField]
    private GameObject RightEye;

    void Start()
    {
        OnStart();
        HandleEvent(new SeasonChangeEvent() { ChangedSeason = SeasonController.GetSeason() });
    }

    void Update()
    {
        OnUpdate();
    }

    public void HandleEvent(SeasonChangeEvent eventData)
    {
        SizeAndColorChange();

        ResetProperties();
        Debug.Log("reset speed: " + speed);
        Debug.Log("reset speed ratio: " + speedRatio);

        Season currentSeson = eventData.ChangedSeason;
        if (currentSeson == Season.WINTER)
        {
            Snowman.SetActive(true);
            LeftEye.SetActive(false);
            RightEye.SetActive(false);
        }
        else
        {
            Snowman.SetActive(false);
            LeftEye.SetActive(true);
            RightEye.SetActive(true);
            speedRatio += 0.4f;
        }

        ChangeProperties(eventData);
        SetProperties();
        Debug.Log("after speed: " + speed);
        Debug.Log("after speed ratio: " + speedRatio);
    }
}
