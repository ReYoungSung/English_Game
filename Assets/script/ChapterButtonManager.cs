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

        // DeactivateButtons 함수를 호출하여 조건에 따라 세 번째 자식을 비활성화
        DeactivateButtons();
    }

    public void InitializeButtons()
    {
        // 모든 버튼의 색상을 초기화
        foreach (Button button in buttons)
        {
            button.GetComponent<Image>().color = defaultColor;
            RectTransform buttonRectTransform = button.GetComponent<RectTransform>();
            buttonRectTransform.localScale = new Vector3(5.0f, 5.0f, 5.0f);
        }
    }

    void OnButtonClick(int clickedButtonIndex)
    {
        // 모든 버튼을 초기화
        InitializeButtons();

        // 클릭된 버튼의 색상을 변경
        buttons[clickedButtonIndex].GetComponent<Image>().color = Color.red;

        // 버튼의 크기를 1.1배로 키우기
        RectTransform buttonRectTransform = buttons[clickedButtonIndex].GetComponent<RectTransform>();
        buttonRectTransform.localScale = new Vector3(5.5f, 5.5f, 5.5f);
    }

    public void ShowPopup()
    {   
        if (SceneOption.Instance.ChapterNum <= PlayerPrefs.GetInt("UnlockedChapterNum"))  
        {
            // 팝업 창을 활성화
            popup.SetActive(true);
            SoundManager.instance.PlaySFX("SellectMenuSFX");
        }
        else
        {
            SoundManager.instance.PlaySFX("ErrorSFX");
            ClosePopup();    
        }
    }

    // 팝업 창을 닫기 위한 함수
    public void ClosePopup()
    {
        // 팝업 창을 비활성화
        popup.SetActive(false);
    } 

    // 조건에 따라 버튼의 세 번째 자식을 비활성화하는 함수
    void DeactivateButtons()
    {
        int unlockedChapterNum = PlayerPrefs.GetInt("UnlockedChapterNum");

        for (int i = 0; i < buttons.Length; i++)
        {
            if (i + 1 <= unlockedChapterNum)
            {
                buttons[i].transform.GetChild(2).gameObject.SetActive(false);
            }
        }
    }
}
