using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandstormStartEvent : IEventData
{
    public bool isSandstormStart { get; set; }
}

public class Sandstorm : MonoBehaviour, IEventHandler<SeasonChangeEvent>, IEventHandler<SandstormEnemyChangeEvent>
{
    public volatile int SandstormEnemyNumber;

    private bool isAutumn;

    // Start is called before the first frame update
    void Start()
    {
        SandstormEnemyNumber = 0;
        isAutumn = false;
        EventBus.register<SeasonChangeEvent>(this);
        EventBus.register<SandstormEnemyChangeEvent>(this);
        HandleEvent(new SeasonChangeEvent() { ChangedSeason = SeasonController.GetSeason() });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy()
    {
        EventBus.unregister<SeasonChangeEvent>(this);
        EventBus.unregister<SandstormEnemyChangeEvent>(this);
    }

    public void HandleEvent(SeasonChangeEvent eventData)
    {
        if (eventData.ChangedSeason == Season.AUTUMN)
        {
            isAutumn = true;
        }
        else
        {
            isAutumn = false;
        }
        StartSandstorm();
    }

    public void HandleEvent(SandstormEnemyChangeEvent eventData)
    {
        int numberOfChange = eventData.NumberOfEnemy;
        SandstormEnemyNumber += numberOfChange;
        StartSandstorm();
    }

    private void StartSandstorm()
    {
        if (!isAutumn && SandstormEnemyNumber > 0)
        {
            gameObject.SetActive(true);
            EventBus.post(new SandstormStartEvent() { isSandstormStart = true });
        }
        else
        {
            gameObject.SetActive(false);
            EventBus.post(new SandstormStartEvent() { isSandstormStart = false });
        }
    }
}
