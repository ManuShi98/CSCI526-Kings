using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIClickEvent : IEventData
{
    public GameObject obj { get; set; }
}
public class CollidersClickEvent : IEventData
{
    public GameObject obj { get; set; }
}

public class UIManager : MonoBehaviour
{
    public bool paused;
    // Start is called before the first frame update
    void Start()
    {
        paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (paused == false)
        {
            if (Input.GetMouseButtonDown(0) == true)
            {
                // For UI components
                GameObject hittedObj = null;
                PointerEventData pointerData = new PointerEventData(EventSystem.current);
                pointerData.position = Input.mousePosition;
                List<RaycastResult> results = new List<RaycastResult>();
                EventSystem.current.RaycastAll(pointerData, results);
                if (results.Count > 0)
                {
                    foreach (RaycastResult res in results)
                    {
                        Debug.Log(res.gameObject.tag);
                        if (res.gameObject.CompareTag("ClickableIcon"))
                        {
                            hittedObj = res.gameObject;
                            break;
                        }
                    }
                    EventBus.post(new UIClickEvent() { obj = hittedObj });
                }
                else
                {
                    // For colliders
                    RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Camera.main.transform.forward);
                    foreach (RaycastHit2D hit in hits)
                    {
                        if (hit.collider.gameObject.CompareTag("Tower"))
                        {
                            hittedObj = hit.collider.gameObject;
                            break;
                        }
                    }
                    EventBus.post(new CollidersClickEvent() { obj = hittedObj });
                }
            }
        }
    }
}
