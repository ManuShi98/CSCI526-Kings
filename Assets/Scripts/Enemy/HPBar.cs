using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public Slider slider;

    public void SetHP(float currentHP)
    {
        slider.value = currentHP;
    }

    public void SetMaxHP(float maxHP)
    {
        slider.maxValue = maxHP;
        slider.value = maxHP;
    }

    public void HPRateEffect(float previousHealthRate)
    {
        slider.maxValue *= previousHealthRate;
        slider.value *= previousHealthRate;
    }
}
