using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class RunningTime : MonoBehaviour
{
    public Text timeText; // Text 컴포넌트에 연결할 텍스트 UI
    public List<string> scenesToExcludeDeletion; // 삭제에서 제외할 특정 씬 이름 리스트
    public List<string> scenesToDeleteTimer; // 타이머 오브젝트를 삭제할 특정 씬 이름 리스트
    private float startTime;
    private bool isTimerRunning = true; // 타이머가 실행 중인지 여부

    private static RunningTime instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(transform.root.gameObject); // 타이머 오브젝트를 삭제하지 않도록 설정 
            SceneManager.sceneLoaded += OnSceneLoaded; // 씬 로딩 이벤트에 대한 구독 
        }
        else
        {
            Destroy(transform.root.gameObject);
        }

        startTime = Time.time; // 씬이 시작될 때 시간 기록  
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scenesToDeleteTimer.Contains(scene.name))
        {
            // 특정 씬에 도달하면 타이머 오브젝트 삭제
            Destroy(transform.root.gameObject);
        }
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            float elapsedTime = Time.time - startTime;

            // 시간을 텍스트로 변환하여 화면에 표시
            string timeString = FormatTime(elapsedTime);
            timeText.text = timeString;
        }

        string currentSceneName = SceneManager.GetActiveScene().name;

        if (!scenesToExcludeDeletion.Contains(currentSceneName) && isTimerRunning)
        {
            if (scenesToDeleteTimer.Contains(currentSceneName))
            {
                // 현재 씬이 타이머 오브젝트를 삭제해야 하는 씬 리스트에 포함된 경우
                Destroy(transform.root.gameObject); // 타이머 오브젝트 삭제
            }
            else
            {
                isTimerRunning = false; // 타이머 중지
                // 여기에서 시간을 기록하거나 필요한 작업을 수행합니다.
            }
        }
    }

    private string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);

        if (timeInSeconds >= 6000f)
        {
            return string.Format("{0:000}:{1:00}", minutes, seconds);
        }
        else
        {
            return string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
