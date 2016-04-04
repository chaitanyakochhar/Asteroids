using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;

/// <summary>
/// This script is attached to a persistent object in the scene in order to handle the inter-app communication
/// </summary>
public class InterAppCommunicationManager : MonoBehaviour
{
    public string gameName;
    public bool result;

    public void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Update()
    {
        if(Input.GetKeyUp(KeyCode.Backspace))
        {
            if(gameName.Length>0)
                CallbackSmartyPalNativeApp(gameName, result);
        }
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            LoadSceneFromURL(GameNames.DodgingAsteroidsGame);
        }
        //if (Input.GetKeyUp(KeyCode.Alpha2))
        //{
        //    LoadSceneFromURL(GameNames.ShootingAsteroidsGame);
        //}
        //if (Input.GetKeyUp(KeyCode.Alpha3))
        //{
        //    LoadSceneFromURL(GameNames.FixingInternationalSpaceStationGame);
        //}

    }

    public void LoadSceneFromURL(string name)
    {

        switch (name)
        {
            case GameNames.DodgingAsteroidsGame:
                {
                    gameName = GameNames.DodgingAsteroidsGame.ToString();
                    SceneManager.LoadScene(GameNames.DodgingAsteroidsGame);
                    
                    break;
                }
            case GameNames.FixingInternationalSpaceStationGame:
                {
                    gameName = GameNames.FixingInternationalSpaceStationGame.ToString();
                    SceneManager.LoadScene(GameNames.FixingInternationalSpaceStationGame);
                    
                    break;
                }
            case GameNames.ShootingAsteroidsGame:
                {
                    gameName = GameNames.ShootingAsteroidsGame.ToString();
                    SceneManager.LoadScene(GameNames.ShootingAsteroidsGame);
                    
                    break;
                }
        }
    }

    public void CallbackSmartyPalNativeApp(string game, bool result)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            Application.OpenURL("smartypalmasterapp://" + game + "/" + result.ToString().ToLower());
        }
        else
        {
            print("Not on iOS, hence only printing out what application will be loaded");
            print("smartypalmasterapp://" + game + "/" + result.ToString().ToLower());
        }
    }

    public void HackLoad(string URL)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            Application.OpenURL("smartypalmasterapp://" + URL);
        }
        else
        {
            print("Not on iOS, hence only printing out what application will be loaded");
            print("smartypalmasterapp://" + URL);
        }
    }
}
