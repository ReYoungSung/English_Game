using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonGame : MonoBehaviour
{
    public List<Button> buttons; // 버튼 리스트
    public List<Vector3> buttonPositions; // 버튼 위치 리스트
    public List<int> targetSequence; // 지정한 번호 순서 리스트
    private HashSet<int> clickedSet = new HashSet<int>(); // 클릭한 번호를 저장하는 HashSet
    public int nextSceneIndex; // 다음 씬의 인덱스 번호를 설정

    void Start()
    {
        // 이전에 눌렀던 정답을 PlayerPrefs에서 읽어옴
        if (PlayerPrefs.HasKey("CorrectClickCount"))
        {
            PlayerPrefs.DeleteKey("CorrectClickCount");
        }

        // 버튼 클릭 이벤트를 각 버튼에 추가
        for (int i = 0; i < buttons.Count; i++)
        {
            int buttonNumber = i + 1; // 버튼의 번호 (1부터 시작)
            buttons[i].onClick.AddListener(() => OnButtonClick(buttonNumber));
        }

        // 버튼 위치 정보를 초기화
        InitializeButtonPositions();

        // 버튼들을 랜덤하게 섞기
        ShuffleButtons();
    }

    void InitializeButtonPositions()
    {
        buttonPositions.Clear();

        // 각 버튼의 초기 위치를 리스트에 추가
        foreach (Button button in buttons)
        {
            buttonPositions.Add(button.transform.position);
        }
    }

    void ShuffleButtons()
    {
        int buttonCount = buttons.Count;

        // 버튼 위치를 섞기 위해 Fisher-Yates 알고리즘 사용
        for (int i = buttonCount - 1; i > 0; i--)
        {
            int randomIndex = UnityEngine.Random.Range(0, i + 1);

            // 현재 버튼 위치와 랜덤하게 선택된 버튼 위치를 교환
            Vector3 tempPosition = buttonPositions[i];
            buttonPositions[i] = buttonPositions[randomIndex];
            buttonPositions[randomIndex] = tempPosition;
        }

        // 섞인 순서대로 버튼 위치를 배치
        for (int i = 0; i < buttonCount; i++)
        {
            buttons[i].transform.position = buttonPositions[i];
        }
    }

    public void OnButtonClick(int buttonNumber)
    {
        buttons[buttonNumber - 1].interactable = false;
        clickedSet.Add(buttonNumber);

        // 버튼을 누를 때마다 현재까지의 클릭 순서와 정답 순서를 비교
        if (clickedSet.Count == targetSequence.Count)
        {
            if (IsCorrectSequence())
            {
                // 정답인 경우
                PlayerPrefs.DeleteKey("CorrectClickCount");

                Debug.Log("정답입니다.");
                // 다음 씬으로 전환
                LoadNextScene();

                // 클릭한 순서 초기화
                clickedSet.Clear();
            }
            else
            {
                // 잘못된 순서
                Debug.Log("잘못된 순서입니다.");

                // 클릭한 순서 초기화
                clickedSet.Clear();
                // 현재 씬을 다시 로드하여 재시작
                ReloadScene();
            }

            buttons.ForEach(button => button.interactable = true);
        }
    }

    bool IsCorrectSequence()
    {
        if (clickedSet.Count != targetSequence.Count)
            return false;

        for (int i = 0; i < targetSequence.Count; i++)
        {
            if (!clickedSet.Contains(targetSequence[i]))
            {
                return false;
            }
        }
        return true;
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void LoadNextScene()
    {
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("마지막 씬입니다.");
        }
    }
}
