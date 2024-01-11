using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeeManager : MonoBehaviour
{
    public Text timeText; // Text ������Ʈ�� ������ �ؽ�Ʈ UI
    public Text missingText = null; // Text ������Ʈ�� ������ �ؽ�Ʈ UI

    private void Update()
    {
        // �ð��� �ؽ�Ʈ�� ��ȯ�Ͽ� ȭ�鿡 ǥ��
        string timeString = FormatTime(RunningTime.Instance.TimerNum);
        timeText.text = timeString;

        if(missingText != null)
            missingText.text = RunningTime.Instance.MissingPoint.ToString();
    }

    private string FormatTime(float timeInSeconds)
    {
        // 0.01�� ������ ��ȯ
        int hundredths = Mathf.FloorToInt((timeInSeconds % 1) * 100);

        // �а� �� ���
        int minutes = Mathf.FloorToInt(timeInSeconds / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);

        // 0.01�� ǥ�ø� ������ �������� ��ȯ
        return string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, hundredths);
    }
}
