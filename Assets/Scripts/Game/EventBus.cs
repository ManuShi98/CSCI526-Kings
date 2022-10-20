using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IEventData
{
}
public interface IEventHandler<TEventData> where TEventData : IEventData
{
    void HandleEvent(TEventData eventData);
}

// Use eventbus to prevent too many interactions between different parts.
public class EventBus : MonoBehaviour
{
    // Singleton
    public static EventBus instance;

    private EventBus()
    {

    }

    private ConcurrentDictionary<Type, List<object>> eventDictionary = new ConcurrentDictionary<Type, List<object>>();
    private ConcurrentDictionary<Type, List<object>> stickyEvents = new ConcurrentDictionary<Type, List<object>>();

    private void OnDestroy()
    {
        instance = null;
    }

    public static void register<TEventData>(IEventHandler<TEventData> eventHandler, bool sticky = false) where TEventData : IEventData
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
        if (!instance.eventDictionary.ContainsKey(typeof(TEventData)))
        {
            instance.eventDictionary[typeof(TEventData)] = new List<object>();
        }
        if (sticky == true && instance.stickyEvents.ContainsKey(typeof(TEventData)))
        {
            foreach (var stickyevent in instance.stickyEvents[typeof(TEventData)])
            {
                var currentStickyEvent = stickyevent as TEventData;
                eventHandler.HandleEvent(currentStickyEvent);
            }
        }
        instance.eventDictionary[typeof(TEventData)].Add(eventHandler);
    }

    public static void unregister<TEventData>(IEventHandler<TEventData> eventHandler) where TEventData : IEventData
    {
        if (instance == null)
        {
            return;
        }
        List<object> handlers = instance.eventDictionary[typeof(TEventData)];
        if (handlers != null && handlers.Contains(eventHandler))
        {
            handlers.Remove(eventHandler);
        }
    }

    public static void post<TEventData>(TEventData eventData) where TEventData : IEventData
    {
        if (instance == null)
        {
            return;
        }
        if (instance.eventDictionary.ContainsKey(eventData.GetType()))
        {
            List<object> handlers = new List<object>(instance.eventDictionary[eventData.GetType()]);
            if (handlers != null && handlers.Count > 0)
            {
                foreach (var handler in handlers)
                {
                    if (handler != null)
                    {
                        var eventHandler = handler as IEventHandler<TEventData>;
                        eventHandler.HandleEvent(eventData);
                    }
                }
            }
        }
    }

    public static void postSticky<TEventData>(TEventData eventData) where TEventData : IEventData
    {
        if (!instance.stickyEvents.ContainsKey(typeof(TEventData)))
        {
            instance.stickyEvents[typeof(TEventData)] = new List<object>();
        }
        instance.stickyEvents[typeof(TEventData)].Add(eventData);
        post<TEventData>(eventData);
    }
}
