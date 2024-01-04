using UnityEngine;
using UnityEngine.UI;

public class WordButton : MonoBehaviour
{
    public string word; // 각 버튼의 고유한 단어
    private GameManager manager; // 게임 매니저 스크립트 참조

    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();

        // 버튼 클릭 이벤트에 버튼 사라지기 함수 연결
        button.onClick.AddListener(CarryWordToManager);  
    }

    // 버튼이 클릭되면 게임 매니저에게 자신의 단어를 전달
    private void CarryWordToManager()
    {
        manager.ReceiveWord(word);
        HideButton();
    }

    // 버튼을 비활성화하여 사라지게 만듭니다.
    public void HideButton()
    {
        button.gameObject.SetActive(false);
    }
}
