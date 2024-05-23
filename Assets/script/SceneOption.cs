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
                DontDestroyOnLoad(instance.gameObject); // 씬이 변경되어도 파괴되지 않도록 설정   
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
        // 각 씬에 따라 BGM 변경
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
        //PlayerPrefs.DeleteAll(); //나중에 지워야 함
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

    // 게임 시작 시 호출할 메서드 
    public void LoadGameData()  
    {
        UnlockedStageList.Clear(); //기존 데이터 refresh 
         
        if (PlayerPrefs.HasKey("UnlockedChapterNum") && PlayerPrefs.HasKey("UnlockedFinalUnitNum"))  //플레이 데이터가 있을 경우 최종 클리어결과만큼 해금   
        {
            // 마지막 chapter를 제외한 모든 chapter에 최종 Unit인 15 할당   
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
            UnlockedStageList.Add(0); //튜토리얼 용 Chapter Index 추가 
            UnlockedStageList.Add(1); //Chapter1 Unit1 해금
        }
    }

    // 게임 스테이지 클리어 시 호출할 메서드 
    public void SaveGameData(GameManager.GameMode gameMode)
    {
        if (LicenseUnlockManager.Instance.LicensesUnlocked)
        {
            Debug.Log("License unlocked");
            if (gameMode == GameManager.GameMode.test)
            {
                if (UnlockedStageList.Count - 1 == ChapterNum) //현재 열린 챕터가 현재 챕터와 동일한지 판단
                {
                    if (UnitNum == MAX_UNIT) //현재 열린 스테이지 중 마지막 단계를 클리어했을 때 새로운 챕터 잠금 해금
                    {
                        UnlockedStageList[ChapterNum] = MAX_UNIT;
                        UnlockedStageList.Add(1);
                    }
                    //UnlockedStageList.Add(1); // 테스트 통과시 마지막 유닛 클리어 여부 상관 없이 챕터 해금
                }
            }
            else if (gameMode == GameManager.GameMode.practice)
            {
                if (UnlockedStageList.Count - 1 == ChapterNum) //현재 열린 챕터가 현재 챕터와 동일한지 판단
                {
                    if (UnlockedStageList[ChapterNum] < MAX_UNIT) //현재 열린 마지막 스테이지지만 유닛은 마지막이 아니면 새로운 유닛 해금
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

    // 어플리케이션이 종료될 때 호출되는 이벤트
    private void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }
}
