using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnMouseDown_SwitchScene : MonoBehaviour
{
    public string sceneName;  // 씬 이름：Inspector에 지정

    void Start()
    {
        // 게임 시작 시 PlayerPrefs에 저장된 모든 데이터 삭제
        PlayerPrefs.DeleteAll(); 
    }

    private void Update()
    {
        Debug.Log(SceneOption.Instance.ChapterNum + " / " + SceneOption.Instance.UnitNum);
    }

    public void ChangeChapter(int a)  
    {
        SceneOption.Instance.ChapterNum = a;  
    }

    public void ChangeUnit(int b) 
    {
        SceneOption.Instance.UnitNum = b; 
    }

    public void LoadOtherScene() 
    {
        // 씬을 전환한다
        SceneManager.LoadScene(sceneName); 
    }
}
