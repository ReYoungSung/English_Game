using UnityEngine;
using UnityEngine.UI;

public class WordButton : MonoBehaviour
{
    public string word; // 각 버튼의 고유한 단어
    public GameManager manager; // 게임 매니저 스크립트 참조

    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        // 버튼이 클릭되면 게임 매니저에게 자신의 단어를 전달
        manager.ReceiveWord(word);
    }
}
