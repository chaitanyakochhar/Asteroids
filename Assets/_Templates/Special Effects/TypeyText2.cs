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
    public GameObject TextUpButton;
    public GameObject TextDownButton;
    private Text body;

    private int minText;
    private int maxText;
    private int currentText;
    private Button UpButton;
    private Button DownButton;


    public void Start()
    {
        minText = 0;
        maxText = textToTypeOut.Length - 1;
        currentText = maxText;

        if(TextUpButton!= null && TextDownButton!= null)
        {
            UpButton = TextUpButton.GetComponent<Button>();
            DownButton = TextDownButton.GetComponent<Button>();
            UpButton.interactable = false;
            DownButton.interactable = false;
        }

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
            UpdateTextBox(images[i],textToTypeOut[i]);
            yield return new WaitForSeconds(textDelay * splitText.Length);
            //foreach (string x in splitText)
            //{
            //    body.text += x + " ";
            //    yield return new WaitForSeconds(textDelay);
            //    yield return null;
            //}
            yield return new WaitForSeconds(delays[i]);
        }
        GetComponent<Authenticator>().isAuthenticated = true;
        UpButton.interactable = true;
        DownButton.interactable = false;
        yield return null;
    }

    private void UpdateTextBox(Sprite image, string text)
    {
        imagePlaceholder.GetComponent<Image>().sprite = image;
        body.text = text;
    }

    public void ScrollUp()
    {
        if (UpButton != null)
        {
            currentText--;
            Validate();
            UpdateTextBox(images[currentText], textToTypeOut[currentText]);
        }
    }
    public void ScrollDown()
    {
        if (DownButton != null)
        {
            currentText++;
            Validate();
            UpdateTextBox(images[currentText], textToTypeOut[currentText]);
            
        }
    }

    private void Validate()
    {
        DownButton.interactable = true;
        UpButton.interactable = true;
        if(currentText == maxText)
        {
            DownButton.interactable = false;
        }
        if(currentText == minText)
        {
            UpButton.interactable = false;
        }
        
    }
}
