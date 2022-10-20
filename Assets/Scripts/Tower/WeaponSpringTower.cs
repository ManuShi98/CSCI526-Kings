using UnityEngine;
using TMPro;

public class WeaponSpringTower : Weapon, IEventHandler<SeasonChangeEvent>
{
    private float passedTime;
    private float timeInterval;
    private int maxIncreaseTime;
    private bool isSpring;

    [SerializeField]
    private TextMeshProUGUI ReminderText;
    // Start is called before the first frame update
    void Start()
    {
        OnStart();

        timeInterval = 5f;

        maxIncreaseTime = 5;

        isSpring = SeasonController.GetSeason() == Season.SPRING;
    }

    // Update is called once per frame
    void Update()
    {
        OnUpdate();

        SpringTowerFunc();
    }

    public new void HandleEvent(SeasonChangeEvent eventData)
    {
        SeasonChangeHandleEvent(eventData);
        isSpring = eventData.ChangedSeason == Season.SPRING;
    }

    // Spring tower special
    private void SpringTowerFunc()
    {
        if (isSpring)
        {
            if (maxIncreaseTime > 0)
            {
                IncreaseSpringTower();
            }
        }
        else
        {
            ResetSpringTowerAttributes();
        }
    }

    // Season is spring, increase the damage
    private void IncreaseSpringTower()
    {
        ReminderText.enabled = true;
        if (passedTime > timeInterval)
        {
            damage += 5f;
            ReminderText.text = "Damage: " + damage.ToString();
            passedTime = 0f;
            maxIncreaseTime--;
        }
        passedTime += Time.deltaTime;
    }

    // Season is not spring, reset
    private void ResetSpringTowerAttributes()
    {
        ReminderText.enabled = false;
        ReminderText.text = "";
        passedTime = 0f;
        maxIncreaseTime = 5;
        damage = startDamage;
    }
}
