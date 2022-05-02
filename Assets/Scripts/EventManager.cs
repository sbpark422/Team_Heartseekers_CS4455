using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    Dictionary<string, UnityEvent> eventDict;
    static EventManager eventManager;

    public static EventManager Instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;
                if (eventManager)
                {
                    eventManager.Init();
                    DontDestroyOnLoad(eventManager.gameObject);
                }
            }
            return eventManager;
        }
    }


    void Init()
    {
        if (eventDict == null)
        {
            eventDict = new Dictionary<string, UnityEvent>();
        }
    }

    public static void StartListening(string eventName, UnityAction listener)
    {
        if (!Instance.eventDict.TryGetValue(eventName, out UnityEvent thisEvent))
        {
            thisEvent = new UnityEvent();
            Instance.eventDict.Add(eventName, thisEvent);
        }
        thisEvent.AddListener(listener);
    }

    public static void StopListening(string eventName, UnityAction listener)
    {
        if (Instance.eventDict.TryGetValue(eventName, out UnityEvent thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName)
    {
        if (Instance.eventDict.TryGetValue(eventName, out UnityEvent thisEvent))
        {
            thisEvent.Invoke();
        }
    }
}
