using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.SceneManagement;

public class PlayerDataManager : MonoBehaviour
{
    //Used as distinct ID
    public string StoryID = "Space Story";
    public string MixpanelDistinctID;
    private string Token = "729533d8e11764068581e40573913081";
    private Stopwatch s;


    public Dictionary<string, string> textCaptured { get; private set; }
    public Dictionary<string, AudioClip> audioCaptured { get; private set; }
    public Dictionary<string, Texture2D> imagesCaptured { get; private set; }
    public List<string> decisions;
    public bool readByYourself = false;
    public LocationInfo locationInfo { get; set; }

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

    #region Read By Yourself
    public void ToggleReadByYourself(bool value)
    {
        readByYourself = !readByYourself;
    }
    #endregion

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

    public bool GetData<T>(Dictionary<string, T> dictionary, string key, out T value)
    {
        bool isDataPresent = dictionary.TryGetValue(key, out value);
        if (isDataPresent)
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

    public void ClearDictionaries(bool images = false, bool text = true, bool audio = true, bool decisionsMade = true)
    {
        if(images)
        {
            imagesCaptured.Clear();
        }
        if(text)
        {
            textCaptured.Clear();
        }
        if(audio)
        {
            audioCaptured.Clear();
        }
        if(decisionsMade)
        {
            decisions.Clear();
        }
    }

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
        Mixpanel.SuperProperties.Add("Is Editor", Application.isEditor.ToString());
    }

    public void PushToMixPanel(string eventName)
    {
        Mixpanel.SendEvent(eventName);
    }

    public void PushToMixPanel(string eventName, Dictionary<string, object> properties)
    {
        Mixpanel.SendEvent(eventName, properties);
    }


    #endregion

    #region Session Tracking

    #endregion

    public void OnApplicationFocus(bool focus)
    {
        Dictionary<string, object> d = new Dictionary<string, object>();
        if (focus)
        {
            print("FOCUS");
            if (s == null)
            {
                s = new Stopwatch();
                GUIDInit();
            }
            
            s.Start();
            d.Add("Platform", Application.platform.ToString());
            d.Add("Is Mobile Platform", Application.isMobilePlatform.ToString());
            d.Add("Resolution", Screen.width + "x" + Screen.height);
            Mixpanel.SendEvent("Session Start", d);


        }
        else
        {
            s.Stop();
            d.Clear();
            d.Add("Platform", Application.platform.ToString());
            d.Add("Is Mobile Platform", Application.isMobilePlatform.ToString());
            d.Add("Resolution", Screen.width + "x" + Screen.height);
            d.Add("Exit Scene", SceneManager.GetActiveScene().name);
            d.Add("Duration", (s.ElapsedMilliseconds / 1000));
            Mixpanel.SendEvent("Session End", d);
        }
    }
}
