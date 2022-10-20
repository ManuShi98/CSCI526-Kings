using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;


public class ToastHandler : MonoBehaviour
{
    public Image container;
    public TextMeshProUGUI priceText;

    public void initToast(string text, System.Action callback)
    {
        priceText.text = text;
        FadeOut(callback);
    }

    public void FadeOut(System.Action callback)
    {
        priceText.DOFade(0, 3).OnComplete(() =>
        {
            callback.Invoke();
            Destroy(gameObject);
        });
        container.DOFade(0, 3);
    }
}
