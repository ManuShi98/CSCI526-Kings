using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy2 : MonoBehaviour {

    public float speed;

    private Transform target;
    private int wavePointIndex = 0;

    void Start() {

        target = path2.points[0];    
    }

    void Update() { 
        if (Season.seasonRouteMapping[Season.season][1] == false)
        {
            Destroy(gameObject);
        }
        if(transform.position == target.position) {
            GetNextPath2Point();
        } else {
            Vector2 dir = target.position - transform.position;
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        }
    }

    private void GetNextPath2Point() {
        if (wavePointIndex >= path2.points.Length-1) {
            Destroy(gameObject);
            return;
        }
        wavePointIndex++;
        target = path2.points[wavePointIndex];
    }
}