using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{   
    public Text messageText; // 메시지를 표시할 UI 텍스트
    public Text koreanText; // 한국어 뜻을 표시할 UI 텍스트  
    public Text englishText; // 영어 정답을 표시할 UI 텍스트

    private List<Button> clickedButtons = new List<Button>(); // 클릭한 버튼을 저장할 리스트 

    // 버튼들의 단어를 관리하기 위한 딕셔너리
    private Dictionary<Button, string> buttonWords = new Dictionary<Button, string>();

    private List<Dictionary<string, object>> Korean_Dialog;
    private List<Dictionary<string, object>> English_Dialog;
    private List<Dictionary<string, object>> FakeWord_Dialog;

    public List<string> listOfAnswer = new List<string>();
    public List<string> listOfFake = new List<string>();
    private List<string> combinedList = new List<string>();

    private void Awake()  
    { 
        //CSV 파일에서 데이터 파싱
        Korean_Dialog = CSVReader.Read("Chapter"+ SceneOption.Instance.ChapterNum.ToString() + "_KSP");
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
        //현재 챕터와 유닛을 불러옴
        koreanText.text = Korean_Dialog[SceneOption.Instance.CurrentLevelNumber][SceneOption.Instance.UnitNum.ToString()].ToString();   
        englishText.text = English_Dialog[SceneOption.Instance.CurrentLevelNumber][SceneOption.Instance.UnitNum.ToString()].ToString().Replace("/", " ");  

        //영어 문장을 /로 나누어서 리스트에 삽입
        listOfAnswer = new List<string>(English_Dialog[SceneOption.Instance.CurrentLevelNumber][SceneOption.Instance.UnitNum.ToString()].ToString().Split('/'));   
        listOfFake = new List<string>(FakeWord_Dialog[SceneOption.Instance.CurrentLevelNumber][SceneOption.Instance.UnitNum.ToString()].ToString().Split('&'));  

        // A 리스트의 모든 요소를 먼저 다른 리스트에 추가합니다.
        combinedList = new List<string>(listOfAnswer); 
        // B 리스트의 요소를 추가하면서 combinedList의 크기가 8이 되도록 제한합니다.
        for (int i = 0; combinedList.Count < 8 && i < listOfFake.Count; i++) 
        {
            combinedList.Add(listOfFake[i]);
        }


        SetButtonTexts();
    }

    private void SetButtonTexts()
    {
        // 각 버튼에 텍스트 할당
        for (int i = 0; i < this.GetComponent<ButtonGame>().buttons.Count; i++)
        {
            // i가 combinedList의 인덱스 범위 내에 있는지 확인
            if (i < combinedList.Count)
            {
                // 각 버튼의 텍스트 컴포넌트에 combinedList의 요소 할당
                this.GetComponent<ButtonGame>().buttons[i].GetComponentInChildren<Text>().text = combinedList[i];
            }
        }
    }
}
