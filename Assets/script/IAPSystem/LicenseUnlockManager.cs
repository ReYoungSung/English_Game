using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class LicenseUnlockManager : MonoBehaviour
{
    private bool licenseUnlocked = false;
    public bool LicensesUnlocked
    {
        get { return licenseUnlocked;}
        set { licenseUnlocked = value; }
    }

    private void Start()
    {
        //var config = new PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();
        //PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        SignIn();
    }

    private void SignIn()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    internal void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            VerifyLicense();
            Debug.Log("Android login success");
        }
        else
        {
            // Disable your integration with Play Games Services or show a login button
            // to ask users to sign-in. Clicking it should call
            // PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication).
            Debug.Log("Android login failed");
        }
    }

    public void OnUnlockChapter()
    {
        licenseUnlocked = true;
        PlayerPrefs.SetInt("HasLicense", 1);
    }

    public void OnUnlockFailed()
    {
        Debug.Log("Chapter unlock failed");
    }

    public void VerifyLicense()
    {
        int licenseStatus = PlayerPrefs.GetInt("HasLicense");
        if (licenseStatus == 1)
        {
            licenseUnlocked = true;
        }
        else
        {
            licenseUnlocked = false;
        }
    }
}
