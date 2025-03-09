using GooglePlayGames.BasicApi;
using GooglePlayGames;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class GoogleAutoLogin : MonoBehaviour
{
    public Text message;
    public void Start()
    {
        SignIn();
    }
    public void SignIn()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }
    internal void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            message.text = $"Login with Google Succesful";
            Debug.Log("LoginSucessful");
            PlayGamesPlatform.Instance.GetUserId();
        }
        else
        {
            message.text = "Login Fail!";
            Debug.Log("Login Fail");
            // Disable your integration with Play Games Services or show a login button
            // to ask users to sign-in. Clicking it should call
            // PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication).
        }
    }

}
