using UnityEngine;
using UnityEngine.UI;

public class ChapterButtonManager : MonoBehaviour 
{
    public Button[] buttons;
    public GameObject popup;

    private Color defaultColor = new Color(1f, 1f, 1f, 0.7f); // Set the alpha value to 0.7 (180/255)

    void Start()
    {
        // 각 버튼에 클릭 이벤트 추가
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // 클로저 문제를 해결하기 위해 변수를 복사
            buttons[i].onClick.AddListener(() => OnButtonClick(index));
        }
    }

    public void InitializeButtons()
    {
        // 모든 버튼의 색상을 초기화
        foreach (Button button in buttons)
        {
            button.GetComponent<Image>().color = defaultColor;
        }
    }

    void OnButtonClick(int clickedButtonIndex)
    {
        // 모든 버튼을 초기화
        InitializeButtons();

        // 클릭된 버튼의 색상을 변경
        buttons[clickedButtonIndex].GetComponent<Image>().color = Color.red;
    }

    public void ShowPopup()
    {
        if (SceneOption.Instance.ChapterNum <= PlayerPrefs.GetInt("UnlockedChapterNum"))  
        {
            // 팝업 창을 활성화
            popup.SetActive(true);     
        }
        else
        {
            Debug.Log("아직 입장할 수 없습니다");   
            ClosePopup();    
        }
    }

    // 팝업 창을 닫기 위한 함수
    public void ClosePopup()
    {
        // 팝업 창을 비활성화
        popup.SetActive(false);
    }
}
