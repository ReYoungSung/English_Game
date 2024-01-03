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

    public void OnMouseDown()
    {
        // 씬을 전환한다
        SceneManager.LoadScene(sceneName);
    }
}
