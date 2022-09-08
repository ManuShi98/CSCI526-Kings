using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public double FiringRate = 10;
    public GameObject bulletPrefab;

    private double FiringIntervalTime;
    private double timer;
    // Start is called before the first frame update
    void Start()
    {
        FiringIntervalTime = 1.0 / FiringRate;
        timer = FiringIntervalTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            timer = FiringIntervalTime;
        }
    }
}
