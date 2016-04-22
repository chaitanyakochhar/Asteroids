using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class OnTriggerEnterPlay : MonoBehaviour
{
    public AudioClip clip;
    public string nameOfObject;
    public bool playEveryTime = false;
    public void OnTriggerEnter(Collider other)
    {
        StartCoroutine(playAudioThenDisable(other.name, nameOfObject));
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(playAudioThenDisable(collision.name, nameOfObject));
    }

    private IEnumerator playAudioThenDisable(string objName, string testName)
    {
        if (objName == testName)
        {
            GetComponent<AudioSource>().PlayOneShot(clip);
        }
        yield return new WaitForSeconds(clip.length);
        if(!playEveryTime)
        {
            this.enabled = false;
        }
        yield return null;
    }
}
