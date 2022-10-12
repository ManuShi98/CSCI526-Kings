using UnityEngine;

public class DisplayCircleAttackRange : MonoBehaviour
{
    //空白区域色值
    [SerializeField] Color emptyColor = new Color(0, 0, 0, 0);
    //圆环区域色值
    [SerializeField] Color circleColor = new Color(96, 96, 96, 0.5f);

    //圆环内径/外径
    private int radius;
    private int border;

    SpriteRenderer spriteRenderer;

    Weapon towerWeapon;

    // todo: 临时系数，后期分辨率问题解决后需要重新调整。
    private readonly int radiusZoomFactor = 20;

    private void Awake()
    {
        spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = 11;
        towerWeapon = gameObject.GetComponentInParent<Weapon>();
        radius = (int)towerWeapon.GetRadius() * radiusZoomFactor;
        Debug.Log("init radius: " + radius);
        border = radius;
        spriteRenderer.sprite = CreateSprite();
    }

    private void Update()
    {
        if ((int)towerWeapon.GetRadius() * radiusZoomFactor != radius)
        {
            radius = (int)towerWeapon.GetRadius() * radiusZoomFactor;
            border = radius;
            Debug.Log("New origin radius: " + towerWeapon.GetRadius());
            Debug.Log("New inted radius: " + radius);
            spriteRenderer.sprite = CreateSprite();
        }
    }

    private Sprite CreateSprite()
    {
        //图片尺寸
        int spriteSize = radius * 2;
        int minRadius = radius - border;
        //创建Texture2D
        Texture2D texture2D = new Texture2D(spriteSize, spriteSize);
        //图片中心像素点坐标
        Vector2 centerPixel = new Vector2(spriteSize / 2, spriteSize / 2);
        Vector2 tempPixel;
        float tempDisSqr;

        //遍历像素点，绘制圆环
        for (int x = 0; x < spriteSize; x++)
        {
            for (int y = 0; y < spriteSize; y++)
            {
                //以中心作为起点，获取像素点向量
                tempPixel.x = x - centerPixel.x;
                tempPixel.y = y - centerPixel.y;
                //是否在半径范围内
                tempDisSqr = tempPixel.sqrMagnitude;
                if (tempDisSqr >= minRadius * minRadius && tempDisSqr <= radius * radius)
                {
                    //设置像素色值
                    texture2D.SetPixel(x, y, circleColor);
                    continue;
                }
                //设置为透明
                texture2D.SetPixel(x, y, emptyColor);
            }
        }

        texture2D.Apply();

        Sprite radiusCircle = Sprite.Create(texture2D, new Rect(0, 0, spriteSize, spriteSize), new Vector2(0.5f, 0.5f));

        //创建Sprite
        return radiusCircle;
    }
}
