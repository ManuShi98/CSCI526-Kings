using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PreGamePanel : MonoBehaviour
{
    public Sprite[] images;
    public string[] introductiuons;
    private int curIndex;

    [SerializeField]
    private GameObject prevBtn;
    [SerializeField]
    private GameObject nextBtn;
    [SerializeField]
    private GameObject overBtn;
    [SerializeField]
    private Image contentImage;
    [SerializeField]
    private TextMeshProUGUI contentText;

    // Start is called before the first frame update
    public void Start()
    {
        curIndex = 0;
        Time.timeScale = 0;
        UpdateBtnStatus();
        UpdateContents();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPrevBtnClicked()
    {
        curIndex--;
        UpdateBtnStatus();
        UpdateContents();
    }

    public void OnNextBtnClicked()
    {
        curIndex++;
        UpdateBtnStatus();
        UpdateContents();
    }

    public void OnOverBtnClicked()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    private void UpdateContents()
    {
        //GameObject.Find("Canvas/Panel/ContentImage").GetComponent<Image>().sprite = images[curIndex];
        //GameObject.Find("Canvas/Panel/ContentText").GetComponent<TextMeshProUGUI>().text = introductiuons[curIndex];
        contentImage.sprite = images[curIndex];
        contentImage.preserveAspect = true;
        contentText.text = introductiuons[curIndex];
    }

    private void UpdateBtnStatus()
    {
        // Prev btn
        if (curIndex == 0)
        {
            prevBtn.SetActive(false);
        }
        else
        {
            prevBtn.SetActive(true);
        }

        // Next btn
        if (curIndex == images.Length - 1)
        {
            nextBtn.SetActive(false);
        }
        else
        {
            nextBtn.SetActive(true);
        }

        // Over btn
        TextMeshProUGUI overBtnText = GameObject.Find("Canvas/Panel/OverBtn/OverBtnText").GetComponent<TextMeshProUGUI>();
        if (curIndex == images.Length - 1)
        {
            overBtnText.text = "Done";
        }
        else
        {
            overBtnText.text = "Skip";
        }
    }
}
