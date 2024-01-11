using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;


public class ButtonGame : MonoBehaviour
{
    public Text outputText; // 단어 출력을 위한 UI 텍스트

    public List<Button> buttons; // 버튼 리스트
    public List<Vector3> buttonPositions; // 버튼 위치 리스트
    public List<Button> answerButtons; // 정답 버튼 리스트
    public List<Button> clickedSet = new List<Button>(); // 클릭한 버튼을 저장한 리스트 
    private List<string> receivedWords = new List<string>(); // 전달받은 단어 저장 리스트 

    public int nextLevel = 0; // 다음 씬의 번호를 설정

    private int answerPoint = 0;

    [SerializeField] private GameObject FailImage;
    [SerializeField] private GameObject thunderImage;
    [SerializeField] private GameObject failEnemy;
    
    private void Start() 
    { 
        // UI 초기화  
        UpdateOutputText(); 

        // 이전에 눌렀던 정답을 PlayerPrefs에서 읽어옴 
        if (PlayerPrefs.HasKey("CorrectClickCount"))
        {
            PlayerPrefs.DeleteKey("CorrectClickCount");
        }

        // 버튼 위치 정보를 초기화
        InitializeButtonPositions(); 

        // 버튼들을 랜덤하게 섞기
        ShuffleButtons();


        for (int i = 0; i < this.GetComponent<GameManager>().listOfAnswer.Count; i++)
        {
            answerButtons.Add(buttons[i]);
        }
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

            // 현재 버튼 위치와 랜덤하게 선택된 버튼 위치를 교환,ll
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

    public void OnButtonClick(Button button)   
    {
        button.interactable = false;
        clickedSet.Add(button);

        // 버튼을 누를 때마다 현재까지의 클릭 순서와 정답 순서를 비교        
        if (IsCorrectSequence() == 1)
        {
            // 정답인 경우
            PlayerPrefs.DeleteKey("CorrectClickCount");
            SoundManager.instance.PlaySFX("ClearSFX");

            // 다음 씬으로 전환
            StartCoroutine(LoadNextScene());

            // 클릭한 순서 초기화
            clickedSet.Clear();
        }
        else if(IsCorrectSequence() == 3)
        {
            // 잘못된 순서
            SoundManager.instance.PlaySFX("FailSFX");

            failEnemy.transform.position = button.transform.position;  

            StartCoroutine(FailFeedbackAction());        
        }
        else
        {
            //잘 선택하고 있을 때  
            SoundManager.instance.PlaySFX("ClickSFX");     
        }

        buttons.ForEach(button => button.interactable = true);    

        ReceiveWord(button.GetComponentInChildren<Text>().text);  
    }   

    private int IsCorrectSequence()
    {   
        //순서대로 올바른 풍선만 눌렀을 때 1로 반환
        for (int i = 0; i < clickedSet.Count; i++)
        {
            if (clickedSet[i] != answerButtons[i]) 
            {
                answerPoint = 0;
                return 3; //3일 때는 실패로 재시작
            }
            else
            {
                answerPoint++;
                if(answerPoint == answerButtons.Count) 
                {
                    return 1; //1일 때는 성공으로 다음 단계로 이동 
                }
            }
        }
        answerPoint = 0;
        return 2; //2일 때는 잘선택하고 있는 중을 표시
    }

    private void ReloadScene() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }

    private IEnumerator LoadNextScene()
    {
        // 대기 시간을 두고 다음 동작을 수행
        yield return new WaitForSeconds(1.5f); 

        if (SceneOption.Instance.CurrentLevelNumber < 14)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            SceneOption.Instance.CurrentLevelNumber++;
            Debug.Log(SceneOption.Instance.CurrentLevelNumber);
        }
        else
        {
            SceneOption.Instance.CurrentLevelNumber = 0;
            SceneOption.Instance.SaveGameData();
            SceneManager.LoadScene("clear");
        }
    }

    private IEnumerator FailFeedbackAction()
    {
        FailImage.SetActive(true);
        StartCoroutine(FadeInCoroutine());
        StartCoroutine(CycleBrightness());

        yield return new WaitForSeconds(2f);

        // 클릭한 순서 초기화
        clickedSet.Clear();
        // 현재 씬을 다시 로드하여 재시작
        ReloadScene();
    }

    private IEnumerator FadeInCoroutine()
    {
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            // 현재 시간에 따라 알파 값을 업데이트
            Color currentColor = FailImage.GetComponent<Image>().color;
            currentColor.a = Mathf.Lerp(0f, 0.8f, elapsedTime / 1f);
            FailImage.GetComponent<Image>().color = currentColor;

            // 경과 시간 업데이트
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // 알파 값이 목표 값으로 도달하도록 보장
        Color finalColor = FailImage.GetComponent<Image>().color;
        finalColor.a = 0.8f;
        FailImage.GetComponent<Image>().color = finalColor; 
    }

    IEnumerator CycleBrightness()
    {
        while (true) // 무한 반복
        {
            float elapsedTime = 0f;

            // 0에서 1까지 명도를 0.2초 간격으로 반복
            while (elapsedTime < 0.2f)
            {
                float currentBrightness = Mathf.PingPong(Time.time / 0.2f, 0.5f) * 2f; // 0에서 1까지 반복
                float brightnessValue = currentBrightness * 100f; // 0에서 100으로 변환
                SetBrightness(brightnessValue);

                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
    }

    void SetBrightness(float brightness)
    {
        Color originalColor = thunderImage.GetComponent<Image>().color;
        Color newColor = new Color(originalColor.r, originalColor.g, originalColor.b, brightness / 100f);
        thunderImage.GetComponent<Image>().color = newColor;
    }

    public void ReceiveWord(string word)
    {
        receivedWords.Add(word); // 단어 저장
        UpdateOutputText(); // 출력 업데이트
    }

    private void UpdateOutputText()
    {
        // 저장된 단어들을 순서대로 출력 텍스트에 표시
        string output = string.Join("  ", receivedWords.ToArray());
        outputText.text = output;
    }
}
