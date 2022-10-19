using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIBtnScaleEffect : MonoBehaviour, IPointerDownHandler
{
    [SerializeField, Header("Scale to"), Range(0, 1)]
    private float _downScale = 0.7f;
    [SerializeField, Header("Down Duration Time")]
    private float _downDuration = 1f;
    [SerializeField, Header("Up Duration Time")]
    private float _upDuration = 1f;

    public RectTransform SpringBtn;
    public RectTransform SummerBtn;
    public RectTransform AutumnBtn;
    public RectTransform WinterBtn;

    public Season currentSeason;

    public RectTransform RectTransform
    {
        get
        {
            if (_rectTransform == null)
            {
                _rectTransform = GetComponent<RectTransform>();
            }
            return _rectTransform;
        }
    }

    void start () 
    {

    }

    private RectTransform _rectTransform;

    public void OnPointerDown(PointerEventData eventData)
    {
        if(currentSeason == Season.SPRING)
        {
            StopAllCoroutines();
            StartCoroutine(ChangeScaleCoroutine(SpringBtn, 1, _downScale, _downDuration));
            StartCoroutine(ChangeScaleCoroutine(SummerBtn, SummerBtn.localScale.x, 1, _upDuration));
            StartCoroutine(ChangeScaleCoroutine(AutumnBtn, AutumnBtn.localScale.x, 1, _upDuration));
            StartCoroutine(ChangeScaleCoroutine(WinterBtn, WinterBtn.localScale.x, 1, _upDuration));
        }
        if(currentSeason == Season.SUMMER)
        {
            StopAllCoroutines();
            StartCoroutine(ChangeScaleCoroutine(SummerBtn, 1, _downScale, _downDuration));
            StartCoroutine(ChangeScaleCoroutine(SpringBtn, SpringBtn.localScale.x, 1, _upDuration));
            StartCoroutine(ChangeScaleCoroutine(AutumnBtn, AutumnBtn.localScale.x, 1, _upDuration));
            StartCoroutine(ChangeScaleCoroutine(WinterBtn, WinterBtn.localScale.x, 1, _upDuration));
        }
        if(currentSeason == Season.AUTUMN)
        {
            StopAllCoroutines();
            StartCoroutine(ChangeScaleCoroutine(AutumnBtn, 1, _downScale, _downDuration));
            StartCoroutine(ChangeScaleCoroutine(SpringBtn, SpringBtn.localScale.x, 1, _upDuration));
            StartCoroutine(ChangeScaleCoroutine(SummerBtn, SummerBtn.localScale.x, 1, _upDuration));
            StartCoroutine(ChangeScaleCoroutine(WinterBtn, WinterBtn.localScale.x, 1, _upDuration));
        }
        if(currentSeason == Season.WINTER)
        {
            StopAllCoroutines();
            StartCoroutine(ChangeScaleCoroutine(WinterBtn, 1, _downScale, _downDuration));
            StartCoroutine(ChangeScaleCoroutine(SpringBtn, SpringBtn.localScale.x, 1, _upDuration));
            StartCoroutine(ChangeScaleCoroutine(SummerBtn, SummerBtn.localScale.x, 1, _upDuration));
            StartCoroutine(ChangeScaleCoroutine(AutumnBtn, AutumnBtn.localScale.x, 1, _upDuration));
        }
    }

    private IEnumerator ChangeScaleCoroutine(RectTransform RectTransform, float beginScale, float endScale, float duration)
    {
        float timer = 0f;
        while (timer < duration)
        {
            RectTransform.localScale = Vector3.one * Mathf.Lerp(beginScale, endScale, timer / duration);
            timer += Time.fixedDeltaTime;
            yield return null;
        }
        RectTransform.localScale = Vector3.one * endScale;
    }

    private void OnDisable() 
    {
        RectTransform.localScale = Vector3.one;
    }
}
