using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Use eventbus to prevent too many interactions between different parts.
public class EventBus : MonoBehaviour
{
    // Singleton
    public static EventBus instance;

    private Dictionary<string, UnityEvent<GameObject, string>> eventDictionary = new Dictionary<string, UnityEvent<GameObject, string>>();

    private void OnDestroy()
    {
        instance = null;
    }

    public static void registerEvent(string eventName, UnityAction<GameObject, string> listener)
    {
        if (instance == null)
        {
            instance = FindObjectOfType(typeof(EventBus)) as EventBus;
            if (instance == null)
            {
                Debug.Log("No event bus in this scene");
                return;
            }
        }
        UnityEvent<GameObject, string> thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEvent<GameObject, string>();
            thisEvent.AddListener(listener);
            instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void unregisterEvent(string eventName, UnityAction<GameObject, string> listener)
    {
        if (instance == null)
        {
            return;
        }
        UnityEvent<GameObject, string> thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName, GameObject obj, string param)
    {
        if (instance == null)
        {
            return;
        }
        UnityEvent<GameObject, string> thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(obj, param);
        }
    }
}
