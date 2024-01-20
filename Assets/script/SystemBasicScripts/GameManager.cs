using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public enum GameMode  
    {
        practice,
        test
    }

    // 현재 계절 변수
    public GameMode currentGameMode;  

    public Text messageText; // 메시지를 표시할 UI 텍스트
    public Text koreanText; // 한국어 뜻을 표시할 UI 텍스트  
    public Text englishText; // 영어 정답을 표시할 UI 텍스트

    private List<Button> clickedButtons = new List<Button>(); // 클릭한 버튼을 저장할 리스트 

    // 버튼들의 단어를 관리하기 위한 딕셔너리
    private Dictionary<Button, string> buttonWords = new Dictionary<Button, string>();

    private List<Dictionary<string, object>> Korean_Dialog;
    [HideInInspector] public List<Dictionary<string, object>> English_Dialog;
    private List<Dictionary<string, object>> FakeWord_Dialog;

    public List<string> listOfAnswer = new List<string>();
    public List<string> listOfFake = new List<string>();
    private List<string> combinedList = new List<string>();

    [HideInInspector] public string englishAnswer;
    [HideInInspector] public string koreanOutput;


    private void Awake()
    {
        //CSV 파일에서 데이터 파싱
        Korean_Dialog = CSVReader.Read("Chapter" + SceneOption.Instance.ChapterNum.ToString() + "_KSP");
        English_Dialog = CSVReader.Read("Chapter" + SceneOption.Instance.ChapterNum.ToString() + "_ESP");
        FakeWord_Dialog = CSVReader.Read("Chapter" + SceneOption.Instance.ChapterNum.ToString() + "_FSP");

        //현재 챕터 및 UNIT의 스테이지에 맞게 게임 내용 변경
        UpdateStage(); 
    }

    private void RestartScene()
    {
        // 현재 씬을 재시작합니다.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void UpdateMessage(string message)
    {
        messageText.text = message;
    }

    private void UpdateStage()
    {
        // currentGameMode에 따라 LevelNumber 수정
        int adjustedLevelNumber = (currentGameMode == GameMode.test) ? SceneOption.Instance.CurrentLevelNumber + 18 : SceneOption.Instance.CurrentLevelNumber - 1;

        koreanOutput = Korean_Dialog[adjustedLevelNumber][SceneOption.Instance.UnitNum.ToString()].ToString();
        englishAnswer = English_Dialog[adjustedLevelNumber][SceneOption.Instance.UnitNum.ToString()].ToString().Replace("/", " ");

        //현재 챕터와 유닛을 불러옴
        if (koreanOutput.Length >= 25)
        {
            koreanText.fontSize = 50; 
        } 

        if (englishAnswer.Length >= 38)
        {
            englishText.fontSize = 65;
        }

        koreanText.text = koreanOutput;
        englishText.text = englishAnswer;

        //영어 문장을 /로 나누어서 리스트에 삽입    
        string inputString = English_Dialog[adjustedLevelNumber][SceneOption.Instance.UnitNum.ToString()].ToString();

        if (inputString.Contains("/"))
            listOfAnswer = new List<string>(inputString.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries));
        else
            listOfAnswer = new List<string>(inputString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));


        listOfFake = new List<string>(FakeWord_Dialog[adjustedLevelNumber][SceneOption.Instance.UnitNum.ToString()].ToString().Replace(" ", "").Split('&'));

        // 만약 리스트가 비어 있거나 2개 이하라면
        if (listOfFake.Count <= 2)
        {
            // 기본 값들을 추가합니다
            listOfFake.Add("JGL");
            listOfFake.Add("Songalak");
            listOfFake.Add("YSR");
            listOfFake.Add("SYH");
        } 

        // A 리스트의 모든 요소를 먼저 다른 리스트에 추가합니다.      
        combinedList = new List<string>(listOfAnswer);

        // B 리스트의 요소를 추가하면서 combinedList의 크기가 8이 되도록 제한합니다.       
        for (int i = 0; combinedList.Count < 8 && i < listOfFake.Count; i++)
        {
            combinedList.Add(listOfFake[i]);
        }

        SetButtonTexts();
    }

    // 영어나 기호가 아닌지 확인하는 메서드
    bool IsEnglishOrSymbol(string s)
    {
        foreach (char c in s)
        {
            if (!(char.IsLetter(c) || char.IsSymbol(c)))
            {
                return false;
            }
        }
        return true;
    }

    private void SetButtonTexts()
    {
        // 각 버튼에 텍스트 할당
        for (int i = 0; i < this.GetComponent<ButtonGame>().buttons.Count; i++)
        {
            // i가 combinedList의 인덱스 범위 내에 있는지 확인
            if (i < combinedList.Count)
            {
                if (combinedList[i].Length >= 13)
                {
                    englishText.fontSize = 50;  
                }
                // 각 버튼의 텍스트 컴포넌트에 combinedList의 요소 할당
                this.GetComponent<ButtonGame>().buttons[i].GetComponentInChildren<Text>().text = combinedList[i];
            }
        }
    }

    public void SetFinalLevel()
    {
        int SceneNumDvelope = (currentGameMode == GameMode.test) ? 9 : 15;
        

        if(SceneOption.Instance.CurrentLevelNumber < SceneNumDvelope)
        {
            SceneOption.Instance.CurrentLevelNumber++;
        }

    }
}

