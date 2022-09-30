using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponSpringTower : Weapon
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

    isSpring = false;

    SceneController.OnSeasonChangeHandler += SpringTowerReceiveSeasonChangedValue;
    SpringTowerReceiveSeasonChangedValue(gameObject, new SeasonArgs(SceneController.GetSeason().ToString().ToLower()));

    Debug.Log("son start");
  }

  // Update is called once per frame
  void Update()
  {
    OnUpdate();

    SpringTowerFunc();
  }

  private void SpringTowerReceiveSeasonChangedValue(object sender, System.EventArgs args)
  {
    SeasonArgs seasonArgs = (SeasonArgs)args;
    isSpring = seasonArgs.CurrentSeason == "spring" ? true : false;
  }

  // Spring tower special
  private void SpringTowerFunc()
  {
    if (isSpring && maxIncreaseTime > 0)
    {
      IncreaseSpringTower();
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
      ReminderText.text = "Up: " + damage.ToString();
      passedTime = 0f;
      maxIncreaseTime--;
      Debug.Log("spring up " + damage);
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
    Debug.Log("spring down " + damage);
  }
}
