using UnityEngine;


public class AudioPlayer : MonoBehaviour
{
    private AudioSource source;
    private AudioClip clip;
    private PlayerDataManager playerDataManager;

    public void Start()
    {

    }    

    public void PlayAudio(string key)
    {
        if(playerDataManager.audioCaptured.TryGetValue(key,out clip))
        {
            source.PlayOneShot(clip);
        }
    }
}
