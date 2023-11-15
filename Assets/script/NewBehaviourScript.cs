using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeRecorder : MonoBehaviour
{
    public Text timeText;
    private float startTime;
    private bool isRecording = false;
    public string sceneToStartRecording; // 기록 시작할 씬의 이름
    public string sceneToStopRecording;  // 기록 중지할 씬의 이름

    void Start()
    {
        timeText.text = "Press Start to Begin";
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Update()
    {
        if (isRecording)
        {
            float elapsedTime = Time.time - startTime;
            timeText.text = "Time: " + elapsedTime.ToString("F2") + " seconds";
        }
    }

    public void StartRecording()
    {
        startTime = Time.time;
        isRecording = true;
        timeText.text = "Recording...";
    }

    public void StopRecording()
    {
        isRecording = false;
        timeText.text = "Recording Stopped";
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == sceneToStartRecording)
        {
            StartRecording();
        }
        else if (scene.name == sceneToStopRecording)
        {
            StopRecording();
        }
    }
}
