using UnityEngine;
using System.Collections;
using System.Net;
using System.Text;
using System.Collections.Generic;
using UnityEngine.Experimental.Networking;

public class RESTWrapper : MonoBehaviour
{

    private string URL = "http://dev.smartypal.com";
    //private string Method = "/parent_login";
    private string email = "info+demo@smartypal.com";
    private string password = "123";
    public string loginResult;

    // Use this for initialization
    void Start()
    {
        LoginAsync(email, password);

    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator testURL(WWW obj)
    {
        yield return obj;
        print(obj.text);
    }

    public void LoginAsync(string email, string password)
    {
        LoginData loginData = new LoginData(email, password);
        StartCoroutine(Login(loginData));
    }

    public void LogoutAsync()
    {
        StartCoroutine(Logout());
    }

    public void UploadAsync()
    {
        ;
    }

    private IEnumerator Upload<T>(T data)
    {
        yield return null;
    }

    private IEnumerator Logout()
    {
        UnityWebRequest r = UnityWebRequest.Delete(URL + "/logout");
        yield return r.Send();
        print(r.responseCode);
    }

    private IEnumerator Login(LoginData loginData) 
    {
        string json = JsonUtility.ToJson(loginData);
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Content-Type", "application/json");
        byte[] body = Encoding.UTF8.GetBytes(json);
        WWW www = new WWW(URL + "/parent_login", body, headers);
        yield return www;
        loginResult = www.text;
        print(loginResult);
    }
    
}
