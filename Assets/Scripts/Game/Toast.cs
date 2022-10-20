using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toast : MonoBehaviour
{
    private static Toast instance;
    public static Toast INSTANCE()
    {
        return instance;
    }
    private Toast()
    {

    }

    public GameObject toastPrefab;
    private bool isShow;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        isShow = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MakeText(string text)
    {
        if(isShow)
        {
            return;
        }
        isShow = true;
        Vector2 pos = new Vector2(Screen.width/2,Screen.height/2);
        var toast = Instantiate(toastPrefab, pos, Quaternion.identity);
        var comp = toast.GetComponent<ToastHandler>();
        comp.initToast(text, () =>
        {
            isShow = false;
        });
    }
}
