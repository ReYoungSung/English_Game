using System.Collections.Generic;
using UnityEngine;

public class SceneOption : MonoBehaviour
{
    private static SceneOption instance;

    public int ChapterNum = 0;
    public int UnitNum = 0;

    public int UnlockedChapterNum = 0; 
    public int UnlockedUnitUnitNum = 0; 

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

    private void Awake()
    {
        if(PlayerPrefs.HasKey("UnlockedChapterNum") || PlayerPrefs.HasKey("UnlockedUnitUnitNum"))
            LoadGameData(); 
    }

    // 게임 시작 시 호출할 메서드 
    public void LoadGameData()
    {
        if (PlayerPrefs.HasKey("UnlockedChapterNum"))
        {
            UnlockedChapterNum = PlayerPrefs.GetInt("UnlockedChapterNum"); 
        }

        if (PlayerPrefs.HasKey("UnlockedUnitUnitNum"))
        {
            UnlockedUnitUnitNum = PlayerPrefs.GetInt("UnlockedUnitUnitNum"); 
        }
    }

    // 게임 종료 시 호출할 메서드
    public void SaveGameData()
    {
        PlayerPrefs.SetInt("UnlockedChapterNum", UnlockedChapterNum); 
        PlayerPrefs.SetInt("UnlockedUnitUnitNum", UnlockedUnitUnitNum); 
        PlayerPrefs.Save(); 
    }

    // 어플리케이션이 종료될 때 호출되는 이벤트
    private void OnApplicationQuit()
    {
        SaveGameData();
    }   
}
