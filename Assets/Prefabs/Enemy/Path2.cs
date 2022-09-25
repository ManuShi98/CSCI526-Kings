using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path2 : MonoBehaviour
{
    public static Transform[] positions2;
    // Start is called before the first frame update
    void Awake()
    {
        positions2 = new Transform[transform.childCount];
        for (int i = 0; i < positions2.Length; i++)
        {
            positions2[i] = transform.GetChild(i);
        }
    }
}
