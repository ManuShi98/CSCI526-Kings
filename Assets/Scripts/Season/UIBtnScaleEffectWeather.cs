using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class UIBtnScaleEffectWeather : MonoBehaviour/*, IPointerDownHandler*/
{
    [SerializeField, Header("Scale to"), Range(0, 1)]
    private float _downScale = 0.7f;
    [SerializeField, Header("Down Duration Time")]
    private float _downDuration = 1f;
    [SerializeField, Header("Up Duration Time")]
    private float _upDuration = 1f;

    public RectTransform SunnyBtn;
    public RectTransform RainyBtn;
    public RectTransform CloudyBtn;
    public RectTransform FoggyBtn;

    private Season currentWeather;

    private WeatherIcon CurrentBtn;

    public WeatherIcon SunnyButton;
    public WeatherIcon RainyButton;
    public WeatherIcon CloudyButton;
    public WeatherIcon FoggyButton;

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

    void update ()
    {
        
    }

    private RectTransform _rectTransform;

    public void ChangeBtnSeason(WeatherIcon weatherBtn)
    {
        CurrentBtn = weatherBtn;
        Debug.Log(CurrentBtn);
        if(CurrentBtn == SunnyButton)
        {
            Debug.Log("1");
            StopAllCoroutines();
            StartCoroutine(ChangeScaleCoroutine(SunnyBtn, 1, _downScale, _downDuration));
            StartCoroutine(ChangeScaleCoroutine(RainyBtn, RainyBtn.localScale.x, 1, _upDuration));
            StartCoroutine(ChangeScaleCoroutine(CloudyBtn, CloudyBtn.localScale.x, 1, _upDuration));
            StartCoroutine(ChangeScaleCoroutine(FoggyBtn, FoggyBtn.localScale.x, 1, _upDuration));
        }
        if(CurrentBtn == RainyButton)
        {
            Debug.Log("2");
            StopAllCoroutines();
            StartCoroutine(ChangeScaleCoroutine(RainyBtn, 1, _downScale, _downDuration));
            StartCoroutine(ChangeScaleCoroutine(SunnyBtn, SunnyBtn.localScale.x, 1, _upDuration));
            StartCoroutine(ChangeScaleCoroutine(CloudyBtn, CloudyBtn.localScale.x, 1, _upDuration));
            StartCoroutine(ChangeScaleCoroutine(FoggyBtn, FoggyBtn.localScale.x, 1, _upDuration));
        }
        if(CurrentBtn == CloudyButton)
        {
            Debug.Log("3");
            StopAllCoroutines();
            StartCoroutine(ChangeScaleCoroutine(CloudyBtn, 1, _downScale, _downDuration));
            StartCoroutine(ChangeScaleCoroutine(SunnyBtn, SunnyBtn.localScale.x, 1, _upDuration));
            StartCoroutine(ChangeScaleCoroutine(RainyBtn, RainyBtn.localScale.x, 1, _upDuration));
            StartCoroutine(ChangeScaleCoroutine(FoggyBtn, FoggyBtn.localScale.x, 1, _upDuration));
        }
        if(CurrentBtn == FoggyButton)
        {
            Debug.Log("4");
            StopAllCoroutines();
            StartCoroutine(ChangeScaleCoroutine(FoggyBtn, 1, _downScale, _downDuration));
            StartCoroutine(ChangeScaleCoroutine(SunnyBtn, SunnyBtn.localScale.x, 1, _upDuration));
            StartCoroutine(ChangeScaleCoroutine(RainyBtn, RainyBtn.localScale.x, 1, _upDuration));
            StartCoroutine(ChangeScaleCoroutine(CloudyBtn, CloudyBtn.localScale.x, 1, _upDuration));
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
