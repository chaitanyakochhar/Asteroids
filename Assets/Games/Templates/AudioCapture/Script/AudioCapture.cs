using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioCapture : MonoBehaviour
{

    public int lengthOfRecording = 10;
    public string id;

    private string[] devices;
    private string device;

    //AudioClip stuff
    private AudioClip clip;
    private int deviceMinFreq;
    private int deviceMaxFreq;

    //AudioSource stuff
    private AudioSource source;

    //Player Data Manager
    private PlayerDataManager playerDataManager;

    //Buttons
    private GameObject startRecording;
    private GameObject stopRecording;

    void Start()
    {
        devices = Microphone.devices;
        print(devices[0]);
        source = GetComponent<AudioSource>();
        playerDataManager = GameObject.Find("Data Manager").GetComponent<PlayerDataManager>();
        startRecording = transform.GetChild(0).gameObject;
        stopRecording = transform.GetChild(1).gameObject;
        stopRecording.SetActive(false);
        StartCoroutine(getPermission());
        if (devices.Length>0)
        {
            device = devices[0];
        }
    }

    public void StartRecording()
    {
        if (device.Length > 0)
        {
            Microphone.GetDeviceCaps(device, out deviceMinFreq, out deviceMaxFreq);
            print(deviceMinFreq);
            print(deviceMaxFreq);
            clip = Microphone.Start(device, false, lengthOfRecording, 44100);
            startRecording.SetActive(false);
            stopRecording.SetActive(true);
        }
    }

    public void StopRecording()
    {
        if(device.Length>0 && Microphone.IsRecording(device))
        {
            Microphone.End(device);
            playerDataManager.audioCaptured.Add(id, clip);
        }
    }

    public void PlayRecording(string key)
    {
        if(playerDataManager.audioCaptured.TryGetValue(key, out clip))
        {
            source.PlayOneShot(clip);
        }
    }

    private IEnumerator getPermission()
    {
        yield return Application.RequestUserAuthorization(UserAuthorization.Microphone);
    }
}
