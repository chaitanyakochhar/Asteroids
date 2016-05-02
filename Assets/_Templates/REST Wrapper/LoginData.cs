using UnityEngine;
using System.Collections;

[System.Serializable]
public class LoginData
{
    public string email;
    public string password;
    public LoginData(string eMail,string Password)
    {
        email = eMail;
        password = Password;
    }
}
