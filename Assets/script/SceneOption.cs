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
                DontDestroyOnLoad(instance.gameObject); // ���� ����Ǿ �ı����� �ʵ��� ���� 
            } 
            return instance;  
        }
    } 

    private void Awake()
    {
        if(PlayerPrefs.HasKey("UnlockedChapterNum") || PlayerPrefs.HasKey("UnlockedUnitUnitNum"))
            LoadGameData(); 
    }

    // ���� ���� �� ȣ���� �޼��� 
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

    // ���� ���� �� ȣ���� �޼���
    public void SaveGameData()
    {
        PlayerPrefs.SetInt("UnlockedChapterNum", UnlockedChapterNum); 
        PlayerPrefs.SetInt("UnlockedUnitUnitNum", UnlockedUnitUnitNum); 
        PlayerPrefs.Save(); 
    }

    // ���ø����̼��� ����� �� ȣ��Ǵ� �̺�Ʈ
    private void OnApplicationQuit()
    {
        SaveGameData();
    }   
}
