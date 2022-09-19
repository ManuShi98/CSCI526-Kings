using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public double radius = 100; // Weapon's firing radius
    public double FiringRate = 10;  // Fire per second

    public Transform firePoint;

    public GameObject enemy; // Locked enemy instance
    public GameObject bulletPrefab;
    
    private double FiringIntervalTime;
    private double timer;

    void Start()
    {
        FiringIntervalTime = 1.0 / FiringRate;
        timer = FiringIntervalTime;
    }

    void Update()
    {
        if (enemy != null)
        {
            // 旋转
            Vector2 dir = enemy.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);

            // 射击
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                timer = FiringIntervalTime;
            }
        } else
        {
            // TODO: 临时方法。后期更换为怪物控制器 + Map(GameObject ID, Health)的方式控制目标锁定方式
            // 优先攻击距离最近的敌人
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            double minDistance = double.MaxValue;
            foreach (GameObject e in enemies)
            {
                double currDistance = TwoPointDistance2D(e.transform.position, transform.position);
                if(currDistance < minDistance)
                {
                    minDistance = currDistance;
                    enemy = e;
                }
            }
        }
    }

    /// <summary>
    /// Calculate the distance between two points in 2D game
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns></returns>
    private float TwoPointDistance2D(Vector2 p1, Vector2 p2)
    {

        float i = Mathf.Sqrt((p1.x - p2.x) * (p1.x - p2.x)
                            + (p1.y - p2.y) * (p1.y - p2.y));

        return i;
    }
}
