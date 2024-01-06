using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reload : MonoBehaviour
{

    void Start()
    {
        // 게임 시작 시 PlayerPrefs에 저장된 모든 데이터 삭제
        PlayerPrefs.DeleteAll();
    }

    public void OnMouseDown()
    {
        ReloadScene();
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
