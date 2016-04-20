using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent(typeof(Authenticator))]
public class TextCapture : MonoBehaviour
{

    public string key;
    private InputField inputField;
    private PlayerDataManager playerDataManager;

    public void Start()
    {
        inputField = GetComponent<InputField>();
        playerDataManager = GameObject.Find("Data Manager").GetComponent<PlayerDataManager>();
    }

    public void AddToTextInput()
    {
        if (key.Length > 0 && inputField.text.Length > 0)
        {
            playerDataManager.AddData<string>(playerDataManager.textCaptured, key, inputField.text);
            GetComponent<Authenticator>().isAuthenticated = true;

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
