using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
[RequireComponent(typeof(Authenticator))]
public class TypeyText : MonoBehaviour
{
    public string textToTypeOut;
    public string DataManagerKey;
    public float textDelay = 0.1f;

    private Text body;
    private GameObject playerDataManager;
    private PlayerDataManager p;

    public void Start()
    {
        playerDataManager = GameObject.Find("Data Manager");
        if(playerDataManager!=null)
        {
            p = playerDataManager.GetComponent<PlayerDataManager>();
        }

        body = GetComponent<Text>();
        if(textToTypeOut.Length>0)
        {
            TypeThisOut(textToTypeOut);
        }
        else if(DataManagerKey.Length>0)
        {
            bool test = GameObject.Find("Data Manager").GetComponent<PlayerDataManager>().textCaptured.TryGetValue(DataManagerKey, out textToTypeOut);
            if(test)
            {
                TypeThisOut(textToTypeOut);
            }
        }
    }

    public void TypeThisOut(string text)
    {
        StartCoroutine(typeOutCoroutine(text));
    }

    private IEnumerator typeOutCoroutine(string text)
    {
        body.text = "";
        char[] separators = { ' ' };
        string[] splitText = text.Split(separators);
        body.text = text;
        yield return new WaitForSeconds(textDelay * splitText.Length);
        //foreach (string x in splitText)
        //{
        //    body.text += x+" ";
        //    yield return new WaitForSeconds(textDelay);
        //    yield return null;
        //}

        GetComponent<Authenticator>().isAuthenticated = true;
        yield return null;
    }

}
