using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour {

    //public float speed;

    //private Transform target;
    public float speed = 10;
    private Transform[] positions;
    private int index = 0;

    void Start() {
        positions = Path1.positions1;
    }

    void Update () {
        Move();
    }

    void Move() {
        if (index > positions.Length - 1) 
        {
            Destroy(gameObject);
            return;
        }
        transform.Translate((positions[index].position - transform.position).normalized * Time.deltaTime * speed);
        if (Vector3.Distance(positions[index].position, transform.position) < 0.3f) {
            index++;
        }
    }
}