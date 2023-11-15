using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private float time;
    [SerializeField] private float curTime;
    [SerializeField] private string sceneToStopTimer;

    int minute;
    int second;

    private bool timerRunning = true;
    private static bool created = false;

    private void Awake()
    {
        // 이미 Timer 오브젝트가 존재하는지 확인
        if (!created)
        {
            created = true;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // 이미 존재하는 경우, 현재의 GameObject 파기
            Destroy(gameObject);
            return;
        }

        time = 90;
        StartCoroutine(StartTimer());

        // 부모 GameObject
        GameObject parent = transform.parent.gameObject;

        // 부모 GameObject가 씬 전환시에도 유지되도록 설정
        DontDestroyOnLoad(parent);

        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    IEnumerator StartTimer()
    {
        curTime = time;
        while (curTime > 0 && timerRunning)
        {
            curTime -= Time.deltaTime;
            minute = (int)curTime / 60;
            second = (int)curTime % 60;
            text.text = minute.ToString("00") + ":" + second.ToString("00");
            yield return null;

            if (curTime <= 0)
            {
                Debug.Log("시간 종료");
                curTime = 0;
                SceneManager.LoadScene("gameover");
            }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == sceneToStopTimer) 
        {
            timerRunning = false;
            enabled = false;

            // 타이머 오브젝트를 파괴
            Destroy(transform.parent.gameObject);
        }
        else if (scene.name == "clear") 
        {
            Destroy(transform.parent.gameObject);
        }
        
    }
}
