using UnityEngine;
using System.Collections.Generic;

public class PlayerDataManager : MonoBehaviour
{
    public Dictionary<string, string> textCaptured { get; private set; }
    public Dictionary<string, AudioClip> audioCaptured { get; private set; }
    public Dictionary<string, Texture2D> imagesCaptured { get; private set; }

    // Use this for initialization
    void Start()
    {
        textCaptured = new Dictionary<string, string>();
        audioCaptured = new Dictionary<string, AudioClip>();
        imagesCaptured = new Dictionary<string, Texture2D>();
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(this);
    }

    public void AddData<T>(Dictionary<string, T> dictionary, string key, T value)
    {
        T data;
        bool isDataPresent;
        isDataPresent = dictionary.TryGetValue(key, out data);
        if (isDataPresent)
        {
            dictionary[key] = value;
        }
        else
        {
            dictionary.Add(key, value);
        }
    }

    public bool GetData<T>(Dictionary<string,T> dictionary, string key, out T value)
    {
        bool isDataPresent = dictionary.TryGetValue(key, out value);
        if(isDataPresent)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
