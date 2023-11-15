using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text outputText; // 단어 출력을 위한 UI 텍스트
    private List<string> receivedWords = new List<string>(); // 전달받은 단어 저장 리스트
    public Text messageText; // 메시지를 표시할 UI 텍스트



    public List<Button> answerButtons; // 정답 버튼 리스트
    private List<Button> clickedButtons = new List<Button>(); // 클릭한 버튼을 저장할 리스트

    
    // 버튼들의 단어를 관리하기 위한 딕셔너리
    private Dictionary<Button, string> buttonWords = new Dictionary<Button, string>();

    private void Start()
    {
        // 버튼과 해당 버튼의 단어를 딕셔너리에 추가
        InitializeButtonWords();

        // UI 초기화
        UpdateOutputText();
    }

    // 버튼들의 단어를 딕셔너리에 추가
    private void InitializeButtonWords()
    {
        foreach (Button button in answerButtons)
        {
            WordButton wordButton = button.GetComponent<WordButton>();
            if (wordButton != null)
            {
                buttonWords.Add(button, wordButton.word);
            }
        }
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


    private void RestartScene()
    {
        // 현재 씬을 재시작합니다.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void UpdateMessage(string message)
    {
        messageText.text = message;
    }
}
