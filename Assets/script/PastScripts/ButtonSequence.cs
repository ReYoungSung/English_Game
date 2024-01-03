using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonSequence : MonoBehaviour
{
    public List<Button> buttons; // 버튼 리스트
    public List<Vector3> buttonPositions; // 버튼 위치 리스트
    public List<int> targetSequence; // 지정한 번호 순서 리스트
    private List<int> clickedSequence = new List<int>(); // 클릭한 번호 순서 리스트
    private int correctClickCount = 0; // 올바른 클릭 횟수
    private int currentTargetIndex = 0; // 현재 비교 중인 정답 순서 인덱스
    private HashSet<int> clickedSet = new HashSet<int>(); // 클릭한 번호를 저장하는 HashSet

    // 다음 씬의 인덱스 번호를 설정
    public int nextSceneIndex;

    void Start()
    {
        // 이전에 눌렀던 정답을 PlayerPrefs에서 읽어옴
        if (PlayerPrefs.HasKey("CorrectClickCount"))
        {
            correctClickCount = PlayerPrefs.GetInt("CorrectClickCount");
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

    void OnButtonClick(int buttonNumber)
    {
        // 이미 정답을 모두 맞췄을 때는 동작하지 않음
        if (correctClickCount >= targetSequence.Count)
        {
            return;
        }

        buttons[buttonNumber - 1].interactable = false;

        // 현재 클릭한 버튼과 targetSequence의 현재 인덱스 위치를 비교
        if (buttonNumber == targetSequence[currentTargetIndex])
        {
            clickedSequence.Add(buttonNumber); // 클릭한 버튼의 번호를 리스트에 추가
            currentTargetIndex++; // 다음 인덱스로 이동

            if (currentTargetIndex == targetSequence.Count)
            {
                // 현재까지 클릭한 버튼이 모두 일치할 때
                correctClickCount++;
                // 이번 클릭을 PlayerPrefs에 저장
                PlayerPrefs.SetInt("CorrectClickCount", correctClickCount);

                if (correctClickCount == 1)
                {
                    // 첫 번째  정답인 경우 씬을 재로드
                    ReloadScene();
                    Debug.Log("첫 번째정답입니다.");
                }
                else if (correctClickCount == 2)
                {
                    // 두 번째 정답인 경우 씬을 재로드
                    ReloadScene();
                    Debug.Log("두 번째정답입니다.");
                }
                else if (correctClickCount == 3)
                {
                    // 세 번째 정답인 경우 다음 씬으로 전환 (다음 씬의 인덱스 번호로 전환)
                    if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
                    {
                        SceneManager.LoadScene(nextSceneIndex);
                        // 기억한 정답을 PlayerPrefs에서 삭제
                        PlayerPrefs.DeleteKey("CorrectClickCount");
                    }
                    else
                    {
                        Debug.Log("마지막 씬입니다.");
                    }
                }

                // 클릭한 순서 초기화
                clickedSequence.Clear();
                // 다음 타겟 인덱스를 0으로 초기화
                currentTargetIndex = 0;
            }
        }
        else
        {
            Debug.Log("잘못된 순서입니다.");
            // 클릭한 순서 초기화
            clickedSequence.Clear();
            // 현재 씬을 다시 로드하여 재시작
            ReloadScene();
        }

        buttons[buttonNumber - 1].interactable = true;
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}