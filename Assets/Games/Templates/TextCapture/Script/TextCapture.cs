using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextCapture : MonoBehaviour
{

    public string key;
    private InputField inputField;

    public void Start()
    {
        inputField = GetComponent<InputField>();
    }

    public void AddToTextInput()
    {
        if (key.Length > 0 && inputField.text.Length > 0)
        {
            GameObject.Find("Data Manager").GetComponent<PlayerDataManager>().textCaptured.Add(key, inputField.text);
        }
        else
        {
            if (key.Length == 0)
            {
                print("No key defined");
            }
            if (inputField.text.Length == 0)
            {
                print("No value defined");
            }
        }
    }

}
