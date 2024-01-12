using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class RunningTime : MonoBehaviour
{
    private static RunningTime instance;

    public float TimerNum; // 타이머 값
    public int MissingPoint = 0; // 실수 값
    public bool isHintOpen = false; // 실수 값
    public bool isTimerRunning = false; // 타이머가 실행 중인지 여부
    public int CheckTurnNum = 0;

    public static RunningTime Instance  
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("RunningTime").AddComponent<RunningTime>();
                DontDestroyOnLoad(instance.gameObject); // 씬이 변경되어도 파괴되지 않도록 설정 
            }
            return instance;
        }
    }

    private void Awake()
    {
        // 싱글톤 패턴 구현
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 타이머를 삭제하지 않도록 설정 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        // 현재 활성화된 씬의 이름 가져오기
        string currentSceneName = SceneManager.GetActiveScene().name;

        // 특정 씬에서만 타이머 실행 여부 결정
        if (currentSceneName == "stage_unit")
        {
            isTimerRunning = true;
        }
        else if( currentSceneName == "clear")
        {
            isTimerRunning = false;
        }
        else
        {
            isTimerRunning = false; // 타이머 중지
            TimerNum = 0;
            MissingPoint = 0;
            isHintOpen = false;
        }

        // 타이머가 실행 중이면 시간 증가
        if (isTimerRunning)
        {
            TimerNum += Time.deltaTime;
        }
    }
}
