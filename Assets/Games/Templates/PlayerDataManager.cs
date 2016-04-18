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
}
