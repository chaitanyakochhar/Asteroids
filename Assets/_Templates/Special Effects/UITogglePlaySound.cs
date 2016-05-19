using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(AudioSource))]
public class UITogglePlaySound : MonoBehaviour {
    public AudioClip onSound;
    public AudioClip offSound;
    public void ToggleListener(bool value)
    {
        Toggle t = GetComponent<Toggle>();
        if(t.isOn)
        {
            GetComponent<AudioSource>().PlayOneShot(onSound);
        }
        else
        {
            GetComponent<AudioSource>().PlayOneShot(offSound);
        }
    }



}
