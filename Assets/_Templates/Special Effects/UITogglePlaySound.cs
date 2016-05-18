using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class UITogglePlaySound : MonoBehaviour {
    public AudioClip onSound;
    public AudioClip offSound;
    public void ToggleListener(bool value)
    {
        if(value)
        {
            GetComponent<AudioSource>().PlayOneShot(onSound);
        }
        else
        {
            GetComponent<AudioSource>().PlayOneShot(offSound);
        }
    }


}
