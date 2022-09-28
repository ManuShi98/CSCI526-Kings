using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerBtn : MonoBehaviour
{
  public GameObject towerPrefab;

  public Sprite sprite;

  [SerializeField]
  private int price = 1;

  private Button Btn;

  void Start()
  {
    Btn = this.GetComponent<Button>();
  }

  void Update()
  {
    ChangeBtnStatus();
  }

  public int GetPrice()
  {
    return price;
  }

  public void ChangePrice(int newPrice)
  {
    price = newPrice;
  }

  private void ChangeBtnStatus()
  {
    int currentCoins = GamingDataController.getInstance().getCoinCount();
    if (currentCoins < price)
    {
      Btn.enabled = false;
    }
    else
    {
      Btn.enabled = true;
    }
  }
}
