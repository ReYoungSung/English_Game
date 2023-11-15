using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SceneController : MonoBehaviour
{
    [Header("Settings")]
    public int maxCompletionCount = 2; // 두 번 완료 후 이동할 횟수
    public string otherSceneName = "OtherScene"; // 다른 씬의 이름

    private int completionCount = 0; // 완료 횟수

    private ButtonSequence buttonSequence; // ButtonSequence 스크립트 참조

    void Start()
    {
        // PlayerPrefs에서 완료 횟수를 불러옵니다.
        if (PlayerPrefs.HasKey("CompletionCount"))
        {
            completionCount = PlayerPrefs.GetInt("CompletionCount");
        }

        // ButtonSequence 스크립트 컴포넌트를 참조합니다.
        buttonSequence = FindObjectOfType<ButtonSequence>();

        // ButtonSequence 스크립트를 비활성화합니다.
        if (buttonSequence != null && completionCount == maxCompletionCount)
        {
            buttonSequence.enabled = false;
        }
    }

    public void CompleteScene()
    {
        completionCount++;

        if (completionCount >= maxCompletionCount)
        {
            // 정해진 횟수만큼 완료되면 다른 씬으로 이동
            PlayerPrefs.DeleteKey("CompletionCount"); // 완료 횟수 초기화
            SceneManager.LoadScene(otherSceneName); // 다른 씬으로 이동
        }
        else
        {
            // 완료 횟수를 PlayerPrefs에 저장
            PlayerPrefs.SetInt("CompletionCount", completionCount);
        }
    }
}
