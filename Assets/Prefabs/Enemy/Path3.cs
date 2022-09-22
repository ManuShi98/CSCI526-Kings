using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path3 : MonoBehaviour
{
    public static Transform[] positions3;
    // Start is called before the first frame update
    void Awake()
    {
        positions3 = new Transform[transform.childCount];
        for (int i = 0; i < positions3.Length; i++)
        {
            positions3[i] = transform.GetChild(i);
        }
    }
}
