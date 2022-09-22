using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path1 : MonoBehaviour
{
    public static Transform[] positions1;
    // Start is called before the first frame update
    void Awake()
    {
        positions1 = new Transform[transform.childCount];
        for (int i = 0; i < positions1.Length; i++)
        {
            positions1[i] = transform.GetChild(i);
        }
    }
}
