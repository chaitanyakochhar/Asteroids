using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Authenticator))]
[RequireComponent(typeof(Text))]
public class TypeyText2 : MonoBehaviour
{
    public GameObject imagePlaceholder;
    public bool showFullText = true;
    public string[] textToTypeOut;
    public Sprite[] images;
    public float[] delays;
    public float textDelay = 0.1f;
    private Text body;


    public void Start()
    {
        body = GetComponent<Text>();
        if(textToTypeOut.Length>0)
        {
            TypeThisOut();
        }
    }

    public void TypeThisOut()
    {
        StartCoroutine(typeOutCoroutine());
    }

    private IEnumerator typeOutCoroutine()
    {
        char[] separators = { ' ' };
        string[] splitText;
        for (int i = 0; i<textToTypeOut.Length;i++)
        {
            body.text = "";
            
            splitText = textToTypeOut[i].Split(separators);
            imagePlaceholder.GetComponent<Image>().sprite = images[i];
            foreach (string x in splitText)
            {
                body.text += x + " ";
                yield return new WaitForSeconds(textDelay);
                yield return null;
            }
            yield return new WaitForSeconds(delays[i]);
        }
        GetComponent<Authenticator>().isAuthenticated = true;
        yield return null;
    }


}
