using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using UnityEngine.Purchasing;
using UnityEngine.Events;
using System.Text;
using Unity.VisualScripting;
using System.Runtime.CompilerServices;

public class LicenseUnlockManager : MonoBehaviour
{
    private static LicenseUnlockManager instance;
    public UnityAction<UnityEngine.Purchasing.Product> OnUnlockChapterAction;

    private string fileName = "License.dat";

    public static LicenseUnlockManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("LicenseManager").AddComponent<LicenseUnlockManager>();
                DontDestroyOnLoad(instance.gameObject); // 씬이 변경되어도 파괴되지 않도록 설정   
            }
            return instance;
        }
    }

    private bool licenseUnlocked = false;
    public bool LicensesUnlocked
    {
        get { return licenseUnlocked;}
        set { licenseUnlocked = value; }
    }

    private void Awake()
    {
        OnUnlockChapterAction += OnUnlockChapter;
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
            LoadData();
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


    public void OnUnlockChapter(UnityEngine.Purchasing.Product product)
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

    public void SaveData()
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.OpenWithAutomaticConflictResolution(
            fileName,
            DataSource.ReadCacheOrNetwork,
            ConflictResolutionStrategy.UseLastKnownGood,
            OnSavedGameOpened
            );
    }

    private void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        if(status == SavedGameRequestStatus.Success)
        {
            Debug.Log("저장 성공");

            GameData gameData = new GameData();
            gameData.chapter = PlayerPrefs.GetInt("UnlockedChapterNum");
            gameData.unit = PlayerPrefs.GetInt("UnlockedFinalUnitNum");
            gameData.licenseUnlocked = licenseUnlocked;

            var update = new SavedGameMetadataUpdate.Builder().Build();
            var json = JsonUtility.ToJson(gameData);
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            savedGameClient.CommitUpdate(game, update, bytes, OnSavedGameWritten);
        }
    }

    private void OnSavedGameWritten(SavedGameRequestStatus status, ISavedGameMetadata data)
    {
        if(status != SavedGameRequestStatus.Success)
        {
            Debug.Log("저장 성공");
        }
        else
        {
            Debug.Log("저장 실패");
        }
    }

    public void LoadData()
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.OpenWithAutomaticConflictResolution(
            fileName,
            DataSource.ReadCacheOrNetwork,
            ConflictResolutionStrategy.UseLastKnownGood,
            LoadGameData
            );
    }

    private void LoadGameData(SavedGameRequestStatus status, ISavedGameMetadata data)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        if (status == SavedGameRequestStatus.Success)
        {
            Debug.Log("로드 성공");
            var update = new SavedGameMetadataUpdate.Builder().Build();
            var json = JsonUtility.ToJson(licenseUnlocked);
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            savedGameClient.ReadBinaryData(data, OnSavedDataRead);
        }
    }

    private void OnSavedDataRead(SavedGameRequestStatus status, byte[] loadedData)
    {
        string data = System.Text.Encoding.UTF8.GetString(loadedData);
        if(data == "")
        {
            Debug.Log("데이터 없음 초기 데이터 저장");
            SaveData();
        }
        else
        {
            Debug.Log("데이터 로드");
            GameData gameData = new GameData();
            gameData = JsonUtility.FromJson<GameData>(data);

            PlayerPrefs.SetInt("UnlockedChapterNum", gameData.chapter);
            PlayerPrefs.SetInt("UnlockedFinalUnitNum", gameData.unit);
            licenseUnlocked = gameData.licenseUnlocked;
            VerifyLicense();
        }
    }
}

public class GameData
{
    public int chapter;
    public int unit;
    public bool licenseUnlocked;
}
