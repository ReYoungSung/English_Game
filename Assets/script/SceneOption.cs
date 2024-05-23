using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneOption : MonoBehaviour
{
    private static SceneOption instance;

    public int ChapterNum = 1;
    public int UnitNum = 1;

    public int MAX_UNIT = 21;
     
    public int CurrentLevelNumber = 1;

    public List<int> UnlockedStageList = new List<int>();

    [HideInInspector] public string previousModeName;
    LicenseUnlockManager licenceManager =
        GameObject.Find("LicenseManager").gameObject.GetComponent<LicenseUnlockManager>();

    public static SceneOption Instance
    {
        get
        {
            if (instance == null)
            {    
                instance = new GameObject("SceneOption").AddComponent<SceneOption>();             
                DontDestroyOnLoad(instance.gameObject); // ���� ����Ǿ �ı����� �ʵ��� ����   
            }   
            return instance;   
        }
    }

    public void UnlockLicense()
    {
        PlayerPrefs.SetInt("UnlockedChapterNum", ++CurrentLevelNumber);
        Debug.Log(CurrentLevelNumber);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)      
    {
        // �� ���� ���� BGM ����
        string sceneName = scene.name;

        if (sceneName == "stage_unit" || sceneName == "Test_unit")
        {
            previousModeName = sceneName;
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Awake()
    {
        //PlayerPrefs.DeleteAll(); //���߿� ������ ��
        if (IsInitialLaunch() || !PlayerPrefs.HasKey("InitialLaunch"))
        {
            PlayerPrefs.SetInt("UnlockedChapterNum", 1);
            PlayerPrefs.SetInt("UnlockedFinalUnitNum", 21);
            PlayerPrefs.SetInt("InitialLaunch", 1);
        }
        LicenseUnlockManager.Instance.VerifyLicense();
        Debug.Log("chapter number" + PlayerPrefs.GetInt("UnlockedChapterNum"));
        Debug.Log("unit number" + PlayerPrefs.GetInt("UnlockedFinalUnitNum"));

        LoadGameData();
    }

    private bool IsInitialLaunch()
    {
        return PlayerPrefs.GetInt("InitialLaunch") == 0;
    }

    private void Start()
    {
        
    }

    // ���� ���� �� ȣ���� �޼��� 
    public void LoadGameData()  
    {
        UnlockedStageList.Clear(); //���� ������ refresh 
         
        if (PlayerPrefs.HasKey("UnlockedChapterNum") && PlayerPrefs.HasKey("UnlockedFinalUnitNum"))  //�÷��� �����Ͱ� ���� ��� ���� Ŭ��������ŭ �ر�   
        {
            // ������ chapter�� ������ ��� chapter�� ���� Unit�� 15 �Ҵ�   
            for (int i = 0; i < PlayerPrefs.GetInt("UnlockedChapterNum"); i++)   
            {
                UnlockedStageList.Add(1);    
            }
            UnlockedStageList.Add(PlayerPrefs.GetInt("UnlockedFinalUnitNum"));
        }
        else
        {
            PlayerPrefs.SetInt("UnlockedChapterNum",1);
            PlayerPrefs.SetInt("UnlockedFinalUnitNum", 6);
            UnlockedStageList.Add(0); //Ʃ�丮�� �� Chapter Index �߰� 
            UnlockedStageList.Add(1); //Chapter1 Unit1 �ر�
        }
    }

    // ���� �������� Ŭ���� �� ȣ���� �޼��� 
    public void SaveGameData(GameManager.GameMode gameMode)
    {
        if (LicenseUnlockManager.Instance.LicensesUnlocked)
        {
            Debug.Log("License unlocked");
            if (gameMode == GameManager.GameMode.test)
            {
                if (UnlockedStageList.Count - 1 == ChapterNum) //���� ���� é�Ͱ� ���� é�Ϳ� �������� �Ǵ�
                {
                    if (UnitNum == MAX_UNIT) //���� ���� �������� �� ������ �ܰ踦 Ŭ�������� �� ���ο� é�� ��� �ر�
                    {
                        UnlockedStageList[ChapterNum] = MAX_UNIT;
                        UnlockedStageList.Add(1);
                    }
                    //UnlockedStageList.Add(1); // �׽�Ʈ ����� ������ ���� Ŭ���� ���� ��� ���� é�� �ر�
                }
            }
            else if (gameMode == GameManager.GameMode.practice)
            {
                if (UnlockedStageList.Count - 1 == ChapterNum) //���� ���� é�Ͱ� ���� é�Ϳ� �������� �Ǵ�
                {
                    if (UnlockedStageList[ChapterNum] < MAX_UNIT) //���� ���� ������ ������������ ������ �������� �ƴϸ� ���ο� ���� �ر�
                    {
                        UnlockedStageList[ChapterNum] = UnitNum + 1;
                    }
                }
            }
            PlayerPrefs.SetInt("UnlockedChapterNum", UnlockedStageList.Count - 1);
            PlayerPrefs.SetInt("UnlockedFinalUnitNum", UnlockedStageList[UnlockedStageList.Count - 1]);
            PlayerPrefs.Save();
        }
        else
        {
            Debug.Log("License locked");
            PlayerPrefs.SetInt("UnlockedChapterNum", 1);
            PlayerPrefs.SetInt("UnlockedFinalUnitNum", 6);
            PlayerPrefs.Save();
        }
    }

    public void SaveGameData()
    {
        PlayerPrefs.SetInt("UnlockedChapterNum", UnlockedStageList.Count - 1);
        PlayerPrefs.SetInt("UnlockedFinalUnitNum", UnlockedStageList[UnlockedStageList.Count - 1]);
        PlayerPrefs.Save();
    }

    // ���ø����̼��� ����� �� ȣ��Ǵ� �̺�Ʈ
    private void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }
}
