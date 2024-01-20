using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeeManager : MonoBehaviour
{
    public Text timeText; // Text 컴포넌트에 연결할 텍스트 UI
    public Text missingText = null; // Text 컴포넌트에 연결할 텍스트 UI

    private string CurrentSceneName;

    private void Awake() 
    {
        CurrentSceneName = SceneManager.GetActiveScene().name; ;
    }

    private void Update()
    {
        // 시간을 텍스트로 변환하여 화면에 표시
        string timeString = FormatTime(RunningTime.Instance.TimerNum);
        timeText.text = timeString;

        if(missingText != null && CurrentSceneName == "clear") 
            missingText.text = RunningTime.Instance.MissingPoint.ToString();
        else if(missingText != null)
            missingText.text = (3 - RunningTime.Instance.MissingPoint).ToString(); //최대 체력인 3에서 실수 횟수 제거 
    }

    private string FormatTime(float timeInSeconds)
    {
        // 0.01초 단위로 변환
        int hundredths = Mathf.FloorToInt((timeInSeconds % 1) * 100);

        // 분과 초 계산
        int minutes = Mathf.FloorToInt(timeInSeconds / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);

        // 0.01초 표시를 포함한 형식으로 반환
        return string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, hundredths);
    }
}
