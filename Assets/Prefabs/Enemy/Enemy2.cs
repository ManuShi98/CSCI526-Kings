using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public float speed = 10;
    private Transform[] positions;
    private int index = 0;

    void Start()
    {   
        positions = Path2.positions2;
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        if (index > positions.Length - 1) 
        {
            Singleton.Instance.numOfReachEndMonster++;
            Destroy(gameObject);
            return;
        }
        transform.Translate((positions[index].position - transform.position).normalized * Time.deltaTime * speed);
        if (Vector3.Distance(positions[index].position, transform.position) < 0.3f)
        {
            index++;
        }
    }
}
