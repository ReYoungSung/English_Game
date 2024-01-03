using System.Collections.Generic;
using UnityEngine;

public class SceneOption : MonoBehaviour
{
    private static SceneOption instance;

    public int ChapterNum = 0;
    public int UnitNum = 0;  

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
}
