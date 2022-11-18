using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatEnemy : EnemyUnit, IEventHandler<SeasonChangeEvent>
{
    [SerializeField]
    private GameObject Hat;
    // Start is called before the first frame update
    void Start()
    {
        OnStart();
        HandleEvent(new SeasonChangeEvent() { ChangedSeason = SeasonController.GetSeason() });
    }

    // Update is called once per frame
    void Update()
    {
        OnUpdate();
    }


    public void HandleEvent(SeasonChangeEvent eventData)
    {
        SizeAndColorChange();

        ResetProperties();
        

        Season currentSeson = eventData.ChangedSeason;
        if (currentSeson == Season.SUMMER)
        {
            Hat.SetActive(false);
        }
        else
        {
            Hat.SetActive(true);
            previousHealthRate += 1f;
        }

        ChangeProperties(eventData);
        SetProperties();
        
    }
}
