using UnityEngine;
using UnityEngine.UI;

public class UnitButtonManager : MonoBehaviour
{
    public Button[] buttons;
    private int unlockedChapterNum; 
    private int unlockedUnitNum;    
    private Color defaultColor = new Color(1f, 1f, 1f, 0.7f); // Set the alpha value to 0.7 (180/255)


    private void Awake()
    {
        // 각 버튼에 클릭 이벤트 추가
        /*
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // 클로저 문제를 해결하기 위해 변수를 복사
            buttons[i].onClick.AddListener(() => OnButtonClick(index));
        }
        */

        unlockedChapterNum = PlayerPrefs.GetInt("UnlockedChapterNum");
        unlockedUnitNum = PlayerPrefs.GetInt("UnlockedFinalUnitNum");
    }

    private void Update()
    {
        SetUnitButton();
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

    public void SetUnitButton()
    {
        if (SceneOption.Instance.ChapterNum == unlockedChapterNum)
        {
            DeactivateButtons();
        }
        else if (SceneOption.Instance.ChapterNum < unlockedChapterNum)
        {
            activateAllButtons();
        }
    }

    // 조건에 따라 버튼의 두 번째 자식을 비활성화하는 함수
    private void DeactivateButtons()
    {
        foreach (Button button in buttons)
        {
            button.transform.GetChild(1).gameObject.SetActive(true);
        }

        for (int i = 0; i < buttons.Length; i++)
        {
            if (i + 1 <= unlockedUnitNum) 
            {
                buttons[i].transform.GetChild(1).gameObject.SetActive(false); 
            }
        }
    }

    private void activateAllButtons()
    {
        foreach (Button button in buttons)
        {
            button.transform.GetChild(1).gameObject.SetActive(false);
        }
    }
}
