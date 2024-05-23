using UnityEngine;
using UnityEngine.UI;

public class UnitButtonManager : MonoBehaviour
{
    public Button[] buttons;
    private Color defaultColor = new Color(1f, 1f, 1f, 1f); // Set the alpha value to 0.7 (180/255)
    private Color toggleColor = new Color(0.8f, 0.8f, 0.8f, 1f); // Set the alpha value to 1 

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

        InitializeButtons();
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
        buttons[clickedButtonIndex].GetComponent<Image>().color = toggleColor;
    }

    public void SetUnitButton()
    {
        int unlockedChapterNum = PlayerPrefs.GetInt("UnlockedChapterNum");
        if (SceneOption.Instance.ChapterNum == unlockedChapterNum)
            DeactivateButtons();
        else
            activateAllButtons();
    }

    // 조건에 따라 버튼의 두 번째 자식을 비활성화하는 함수
    private void DeactivateButtons()
    {
        int unlockedChapterNum = PlayerPrefs.GetInt("UnlockedChapterNum");
        int unlockedUnitNum = PlayerPrefs.GetInt("UnlockedFinalUnitNum");
        foreach (Button button in buttons)
        {
            button.transform.GetChild(1).gameObject.SetActive(true);
        }

        for (int i = 0; i < buttons.Length; i++)
        {
            if (i + 1 <= unlockedUnitNum) 
            {
                buttons[i].transform.GetChild(1).gameObject.SetActive(false);
                buttons[i].GetComponent<Image>().enabled = true;
            }
        }
    }

    private void activateAllButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].transform.GetChild(1).gameObject.SetActive(false);
            buttons[i].GetComponent<Image>().enabled = true;
        }
    }

    //private void ActivateButtonsUpTo(int unitNum)
    //{
    //    for(int i = 0; i < unitNum; ++i)
    //    {
    //        GameObject subButton = buttons[i].transform.GetChild(1).gameObject;
    //        subButton.transform.GetChild(0).gameObject.SetActive(false);
    //    }
    //}
}
