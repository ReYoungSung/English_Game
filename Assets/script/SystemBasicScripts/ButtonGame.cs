using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using InGameScript;
using System.Linq;

public class ButtonGame : MonoBehaviour
{
    private GameManager gameManager;

    public Text outputText; // 단어 출력을 위한 UI 텍스트

    public List<Button> buttons; // 버튼 리스트
    public List<Vector3> buttonPositions; // 버튼 위치 리스트
    public List<Button> answerButtons; // 정답 버튼 리스트
    public List<Button> clickedSet = new List<Button>(); // 클릭한 버튼을 저장한 리스트 
    private List<string> receivedWords = new List<string>(); // 전달받은 단어 저장 리스트 

    private int answerPoint = 0;

    [SerializeField] private GameObject FailImage;
    [SerializeField] private GameObject thunderImage;
    [SerializeField] private GameObject failEnemy;


    private void Start()
    {
        gameManager = this.GetComponent<GameManager>();

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


        for (int i = 0; i < gameManager.listOfAnswer.Count; i++)
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

            // 클릭한 순서 초기화
            clickedSet.Clear();

            //테스트 모드인지 아닌지 구분 후 적용
            if (gameManager.currentGameMode == GameManager.GameMode.test)
            {
                RunningTime.Instance.isHintOpen = false;
                StartCoroutine(LoadNextScene());
            }
            else  //기본 연습 버전일 때는 
            {
                RunningTime.Instance.CheckTurnNum++;

                //세 번 반복 후 다음 씬으로 전환
                if (RunningTime.Instance.CheckTurnNum == 3)
                {
                    StartCoroutine(LoadNextScene());   
                }
                else
                {
                    StartCoroutine(ReloadRepeatScene());
                }
            }

            ReceiveWord(button.GetComponentInChildren<Text>().text);
        }
        else if (IsCorrectSequence() == 3)
        {
            // 잘못된 순서
            SoundManager.instance.PlaySFX("FailSFX");    
            RunningTime.Instance.MissingPoint++;     

            failEnemy.transform.position = button.transform.position;


            if (gameManager.currentGameMode == GameManager.GameMode.test)
            {
                RunningTime.Instance.isHintOpen = true;
            }
            StartCoroutine(FailFeedbackAction());
        }
        else
        {
            //잘 선택하고 있을 때  
            SoundManager.instance.PlaySFX("ClickSFX");

            ReceiveWord(button.GetComponentInChildren<Text>().text);
        }

        buttons.ForEach(button => button.interactable = true);            
    }

    private int IsCorrectSequence()
    {
        //순서대로 올바른 풍선만 눌렀을 때 1로 반환
        for (int i = 0; i < clickedSet.Count; i++)
        {
            if (!IsEnglishAnswerContained())
            {
                answerPoint = 0;
                return 3; //3일 때는 실패로 재시작
            }
            else
            {
                answerPoint++;
                if (answerPoint == answerButtons.Count)
                {
                    return 1; //1일 때는 성공 
                }
            }
        }
        answerPoint = 0;
        return 2; //2일 때는 잘선택하고 있는 중을 표시
    }

    // 클릭된 버튼들의 텍스트를 순서대로 합친 문자열을 생성하는 함수
    private string ConcatenateButtonText(List<Button> buttons)
    {
        return string.Join(" ", buttons.Select(button => button.GetComponentInChildren<Text>().text));
    }

    // gameManager.englishAnswer의 앞부분부터 포함되는지 여부를 확인하는 함수
    private bool IsEnglishAnswerContained()
    {
        string concatenatedButtonText = ConcatenateButtonText(clickedSet);
        return gameManager.englishAnswer.StartsWith(concatenatedButtonText);
    }


    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator LoadNextScene()
    {
        SceneOption.Instance.CurrentLevelNumber++; 

        yield return new WaitForSeconds(1.5f);

        RunningTime.Instance.CheckTurnNum = 0;

        int finalLevelIndex = (gameManager.currentGameMode == GameManager.GameMode.test) ? 9 : 15;

        if (SceneOption.Instance.CurrentLevelNumber <= finalLevelIndex)
        {
            ReloadScene();
        }
        else //최종단계 클리어 시 
        {
            if (gameManager.currentGameMode == GameManager.GameMode.test)
            {
                SceneOption.Instance.SaveGameData();
                SceneManager.LoadScene("clearForTest");
            }
            else
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

        if (gameManager.currentGameMode == GameManager.GameMode.test)
        {
            if (RunningTime.Instance.MissingPoint >= 3)
            {
                SceneManager.LoadScene("gameover"); 
            }
            else
                ReloadScene();
        }
        else
        {
            ReloadScene();
        }
    }

    private IEnumerator ReloadRepeatScene()
    {
        // 대기 시간을 두고 다음 동작을 수행
        yield return new WaitForSeconds(1.5f);

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
        receivedWords.Add(word);
        UpdateOutputText();
    }

    private void UpdateOutputText()
    {
        string output = string.Join(" ", receivedWords.ToArray());    
        outputText.text = output;  

        // Check the length of the concatenated string
        if (gameManager.englishAnswer.Length >= 38) 
        {
            // If it exceeds 30 characters, set the font size to 55
            outputText.fontSize = 55; 
        }
        else
        {
            // Otherwise, set the font size to the default value (you may adjust this as needed)
            outputText.fontSize = 80;// Your default font size here;
        }
    }
}
