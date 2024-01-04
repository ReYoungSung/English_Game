using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnMouseDown_SwitchScene : MonoBehaviour
{
    void Start()
    {
        // 게임 시작 시 PlayerPrefs에 저장된 모든 데이터 삭제
        PlayerPrefs.DeleteAll(); 
    }

    private void Update()
    {
        Debug.Log("챕터 번호:"+SceneOption.Instance.ChapterNum + " | 유닛 번호:"+ SceneOption.Instance.UnitNum);
    }

    public void ChangeChapter(int a)  
    {
        SceneOption.Instance.ChapterNum = a;  
    }

    public void ChangeUnit(int b) 
    {
        SceneOption.Instance.UnitNum = b; 
    }

    public void LoadOtherScene(string sceneName) 
    {
        // 씬을 전환한다
        SceneManager.LoadScene(sceneName); 
    }
}
