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

    public void Start()
    {
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
        foreach (string x in splitText)
        {
            body.text += x+" ";
            yield return new WaitForSeconds(textDelay);
            yield return null;
        }
        body.

        GetComponent<Authenticator>().isAuthenticated = true;
        yield return null;
    }

}
