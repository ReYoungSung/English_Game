using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneOption : MonoBehaviour
{
    private static SceneOption instance;

    public int ChapterNum = 1;
    public int UnitNum = 1;
     
    public int CurrentLevelNumber = 1;

    public List<int> UnlockedStageList = new List<int>();

    [HideInInspector] public string previousModeName;

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

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // �� ���� ���� BGM ����
        string sceneName = scene.name;

        if (sceneName == "stage_unit" || sceneName == "Test_unit")
        {
            previousModeName = sceneName;
        }
        Debug.Log(previousModeName);
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
        PlayerPrefs.SetInt("UnlockedChapterNum", 2);  
        PlayerPrefs.SetInt("UnlockedFinalUnitNum", 1);  
        LoadGameData();  
    }   

    // ���� ���� �� ȣ���� �޼��� 
    public void LoadGameData()  
    {
        UnlockedStageList.Clear(); //���� ������ refresh 
         
        if (PlayerPrefs.HasKey("UnlockedChapterNum") || PlayerPrefs.HasKey("UnlockedFinalUnitNum"))  //�÷��� �����Ͱ� ���� ��� ���� Ŭ��������ŭ �ر�   
        {
            // ������ chapter�� ������ ��� chapter�� ���� Unit�� 15 �Ҵ�   
            for (int i = 0; i < PlayerPrefs.GetInt("UnlockedChapterNum"); i++)   
            {
                UnlockedStageList.Add(15);    
            }
            UnlockedStageList.Add(PlayerPrefs.GetInt("UnlockedFinalUnitNum"));
        }
        else
        {
            PlayerPrefs.SetInt("UnlockedChapterNum",1);
            PlayerPrefs.SetInt("UnlockedFinalUnitNum", 1);
            UnlockedStageList.Add(0); //Ʃ�丮�� �� Chapter Index �߰� 
            UnlockedStageList.Add(1); //Chapter1 Unit1 �ر�
        }
    }

    // ���� �������� Ŭ���� �� ȣ���� �޼��� 
    public void SaveGameData()  
    {
        if (UnlockedStageList.Count-1 == ChapterNum) //���� ���� é�Ͱ� ���� é�Ϳ� �������� �Ǵ�
        {
            if (UnitNum == 21) //���� ���� �������� �� ������ �ܰ踦 Ŭ�������� �� ���ο� é�� ��� �ر�
            {
                UnlockedStageList.Add(1);  
            }
            else if (UnlockedStageList[ChapterNum] < 21) //���� ���� ������ ������������ ������ �������� �ƴϸ� ���ο� ���� �ر�
            {
                UnlockedStageList[ChapterNum] = UnitNum + 1;     
            }
        }

        PlayerPrefs.SetInt("UnlockedChapterNum", UnlockedStageList.Count-1);  
        PlayerPrefs.SetInt("UnlockedFinalUnitNum", UnlockedStageList[UnlockedStageList.Count-1]);  
        PlayerPrefs.Save(); 
    }

    // ���ø����̼��� ����� �� ȣ��Ǵ� �̺�Ʈ
    private void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }   
}
