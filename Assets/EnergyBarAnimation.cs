using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnergyBarAnimation : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private Slider slider;
    public TextMeshProUGUI textObject;
    public Image barImage;

    private readonly float alphaSpeed = 5f;  //闪烁速度
    private readonly float alpha = 0.3f;     //最低透明度
    private bool isShow = true;
    private Color originalColor;

    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        slider = gameObject.GetComponent<Slider>();
        originalColor = barImage.color;
    }

    // Update is called once per frame
    private void Update()
    {
        if(slider.value == slider.maxValue)
        {
            textObject.text = "Maximum season energy";
            barImage.color = new Color(253, 201, 0, 255);
            if (isShow)
            {
                if (canvasGroup.alpha != alpha)
                {
                    canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, alpha, alphaSpeed * Time.deltaTime);
                    if (Mathf.Abs(canvasGroup.alpha - alpha) <= 0.01)
                    {
                        canvasGroup.alpha = alpha; isShow = false;
                    }
                }
            }
            else
            {
                if (canvasGroup.alpha != 1)
                {
                    canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, 1, alphaSpeed * Time.deltaTime);
                    if (Mathf.Abs(1 - canvasGroup.alpha) <= 0.01)
                    {
                        canvasGroup.alpha = 1; isShow = true;
                    }
                }
            }
        } else
        {
            textObject.text = "Accumulating energy...";
            canvasGroup.alpha = 1;
            barImage.color = originalColor;
        }
    }
}
