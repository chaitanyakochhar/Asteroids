using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace FixingISSGame
{
    public class GameManager : MonoBehaviour
    {
        public int currentPhase = 1;
        public Phase[] phases;
        public GameObject[] cameraWaypoints;
        public GameObject intermediateDestination;
        public AudioClip playForStickers;
        public int lerpDuration = 2;

        public AudioClip failSound;
        public AudioClip passSound;
        public string nextScene = "Ending";

        public void evaluateCurrentPhase()
        {
            bool result = phases[currentPhase - 1].EvaluatePhase();
            if (result)
            {
                currentPhase++;
                if (currentPhase >= phases.Length)
                {
                    print("We're done here!");
                    InterAppCommunicationManager i = GameObject.Find("Manager").GetComponent<InterAppCommunicationManager>();
                    i.result = true;
                    //i.CallbackSmartyPalNativeApp(i.gameName, i.result);
                    SceneManager.LoadScene(nextScene);
                }
                else
                {
                    print("Transition to the next part of the game");
                    Camera.main.GetComponent<AudioSource>().PlayOneShot(passSound);
                    StartCoroutine(CameraLerp(Camera.main.transform.position, intermediateDestination.transform.position, cameraWaypoints[currentPhase - 1].transform.position, Camera.main.gameObject, lerpDuration));
                }
            }
            else
            {
                Camera.main.GetComponent<AudioSource>().PlayOneShot(failSound);
            }
        }

        public IEnumerator CameraLerp(Vector3 startPos, Vector3 via, Vector3 endPos, GameObject obj, float duration)
        {
            Camera.main.orthographic = false;
            float startTime = Time.time;
            float diff = Time.time - startTime;
            duration /= 2;
            while (diff <= duration)
            {
                obj.transform.position = Vector3.Lerp(startPos, via, diff / duration);
                diff = Time.time - startTime;
                yield return null;
            }

            startTime = Time.time;
            diff = Time.time - startTime;
            while(diff <= duration)
            {
                obj.transform.position = Vector3.Lerp(via, endPos, diff / duration);
                diff = Time.time - startTime;
                yield return null;
            }
            Camera.main.orthographic = true;
            Camera.main.GetComponent<AudioSource>().PlayOneShot(playForStickers);
            yield return null;
        }

    }
}
