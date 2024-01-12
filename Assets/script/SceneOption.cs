using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneOption : MonoBehaviour
{
    private static SceneOption instance;

    public int ChapterNum = 1;
    public int UnitNum = 1;
     
    public int CurrentLevelNumber = 1;

    public List<int> UnlockedStageList = new List<int>();

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
    
    private void Awake()
    {
        //PlayerPrefs.DeleteAll(); //���߿� ������ ��

        LoadGameData(); 
    }

    // ���� ���� �� ȣ���� �޼��� 
    public void LoadGameData()  
    {
        UnlockedStageList.Clear(); //���� ������ refresh
        CurrentLevelNumber = 1; //�������� ���� �ʱ�ȭ 
         
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
            if (UnitNum == 15) //���� ���� �������� �� ������ �ܰ踦 Ŭ�������� �� ���ο� é�� ��� �ر�
            {
                UnlockedStageList.Add(1);  
            }
            else if (UnlockedStageList[ChapterNum] < 15) //���� ���� ������ ������������ ������ �������� �ƴϸ� ���ο� ���� �ر�
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
