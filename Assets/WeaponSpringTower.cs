using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponSpringTower : Weapon
{
  // [SerializeField]
  // private GameObject ReminderText;
  // Start is called before the first frame update
  void Start()
  {
    OnStart();

    SceneController.OnSeasonChangeHandler += SpringTowerReceiveSeasonChangedValue;
    SpringTowerReceiveSeasonChangedValue(gameObject, new SeasonArgs(SceneController.GetSeason().ToString().ToLower()));

    // ReminderText.transform.position = new Vector3(transform.position.x, transform.position.y + 3, 0);

    Debug.Log("son start");
  }

  // Update is called once per frame
  void Update()
  {
    OnUpdate();
  }

  private void SpringTowerReceiveSeasonChangedValue(object sender, System.EventArgs args)
  {
    SeasonArgs seasonArgs = (SeasonArgs)args;
    SpringTowerFunc(seasonArgs.CurrentSeason);
  }

  // Spring tower special
  private void SpringTowerFunc(string currentSeason)
  {
    if (currentSeason == "spring")
    {
      // Current season is spring
      // Increase damage with coroutine
      // ReminderText.enabled = true;
      StartCoroutine("StartSpringTowerCoroutine");
    }
    else
    {
      // Current season is not spring
      // Reset damage and stop coroutine
      // ReminderText.enabled = false;
      StopCoroutine("StartSpringTowerCoroutine");
      damage = startDamage;
      Debug.Log("spring down " + damage);
    }
  }

  private IEnumerator StartSpringTowerCoroutine()
  {
    for (int i = 0; i < 5; ++i)
    {
      yield return new WaitForSeconds(5f);
      damage += 5f;
      // ReminderText.text = "Up: " + damage.ToString();
      Debug.Log("spring up " + damage);
    }
  }
}
