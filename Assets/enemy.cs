using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour {

    public float speed;

    private Transform target;
    private int wavePointIndex = 0;

    void Start() {

        target = path1.points[0];    
    }

    void Update() { 
        if (Season.seasonRouteMapping[Season.season][0] == false)
        {
            Destroy(gameObject);
        }
        if(transform.position == target.position) {
            GetNextPath1Point();
        } else {
            Vector2 dir = target.position - transform.position;
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        }
    }

    private void GetNextPath1Point() {
        if (wavePointIndex >= path1.points.Length-1) {
            Destroy(gameObject);
            return;
        }
        wavePointIndex++;
        target = path1.points[wavePointIndex];
    }
}