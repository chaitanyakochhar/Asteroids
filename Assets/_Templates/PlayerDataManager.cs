using UnityEngine;
using System.Collections.Generic;

public class PlayerDataManager : MonoBehaviour
{
    //Used as distinct ID
    public string MixpanelDistinctID;
    private string Token = "729533d8e11764068581e40573913081";

    public Dictionary<string, string> textCaptured { get; private set; }
    public Dictionary<string, AudioClip> audioCaptured { get; private set; }
    public Dictionary<string, Texture2D> imagesCaptured { get; private set; }
    public List<string> decisions;

    // Use this for initialization
    void Start()
    {

        GUIDInit();

        textCaptured = new Dictionary<string, string>();
        audioCaptured = new Dictionary<string, AudioClip>();
        imagesCaptured = new Dictionary<string, Texture2D>();
        if (decisions == null)
        {
            decisions = new List<string>();
        }
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(this);
    }


    #region Capturing PlayerData Information
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

    public void AddDecision(string decision)
    {
        decisions.Add(decision);
    }
    #endregion

    #region MixPanel Calls
    private void GUIDInit()
    {
        if (PlayerPrefs.HasKey("GUID"))
        {
            MixpanelDistinctID = PlayerPrefs.GetString("GUID");
        }
        else
        {
            System.Guid guid = System.Guid.NewGuid();
            MixpanelDistinctID = guid.ToString();
            PlayerPrefs.SetString("GUID", MixpanelDistinctID);
        }
        Mixpanel.DistinctID = MixpanelDistinctID;
        Mixpanel.Token = Token;
    }

    public void PushToMixPanel(string eventName)
    {
        if()
        Mixpanel.SendEvent(eventName);
    }
    #endregion

}
