using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : SingletonBase<EventManager>
{
    private readonly Dictionary<string, List<Subscription>> _subscriptions =
        new Dictionary<string, List<Subscription>>();

    public void Subscribe(string eventName, Subscription subscription)
    {
        if (!_subscriptions.ContainsKey(eventName))
            _subscriptions[eventName] = new List<Subscription>();

        _subscriptions[eventName].Add(subscription);
    }
    public void UnSubscribe(string eventName, Subscription subscription)
    {
        if (_subscriptions.ContainsKey(eventName))
             _subscriptions[eventName].Remove(subscription);
    }

    public void Publish(string eventName, params object[] arguments)
    {
        var array = _subscriptions[eventName];
        foreach (Subscription subscription in array) subscription(arguments);    
    }
}
