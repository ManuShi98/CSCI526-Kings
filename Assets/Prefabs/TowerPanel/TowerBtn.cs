using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerBtn : MonoBehaviour
{
  public GameObject towerPrefab;

  public Sprite sprite;

  [SerializeField]
  private int price = 1;

  private Button Btn;

  [SerializeField]
  private TextMeshProUGUI PriceText;


  void Start()
  {
    Btn = this.GetComponent<Button>();
    PriceText.text = price.ToString();
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
    PriceText.text = price.ToString();
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
