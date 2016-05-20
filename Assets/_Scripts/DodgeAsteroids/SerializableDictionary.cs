using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public struct KeyValuePair
{
    public string Key;
    public string Value;
}

[Serializable]
public struct KeyObjectPair
{
    public string Key;
    public GameObject Object;
}

[Serializable]
public class SerializableDictionary
{
    public KeyValuePair[] items;
    public Dictionary<string, object> getDictionary()
    {
        Dictionary<string, object> dictionary = new Dictionary<string, object>();
        foreach (KeyValuePair item in items)
        {
            if (dictionary.ContainsKey(item.Key))
            {
                dictionary[item.Key] = item.Value;
            }
            else
            {
                dictionary.Add(item.Key, item.Value);
            }
        }
        return dictionary;
    }
    public void Add(string key, string value)
    {
        KeyValuePair item;
        item.Key = key;
        item.Value = value;
        List<KeyValuePair> tempItems = new List<KeyValuePair>(items);
        if (tempItems.Contains(item))
        {
            tempItems.Remove(item);
        }
        tempItems.Add(item);
        items = tempItems.ToArray();
    }
}