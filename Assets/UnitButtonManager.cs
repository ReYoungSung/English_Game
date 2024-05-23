using UnityEngine;
using UnityEngine.UI;

public class UnitButtonManager : MonoBehaviour
{
    public Button[] buttons;
    private Color defaultColor = new Color(1f, 1f, 1f, 1f); // Set the alpha value to 0.7 (180/255)
    private Color toggleColor = new Color(0.8f, 0.8f, 0.8f, 1f); // Set the alpha value to 1 

    private void Awake()
    {
        // �� ��ư�� Ŭ�� �̺�Ʈ �߰�
        /*
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // Ŭ���� ������ �ذ��ϱ� ���� ������ ����
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
        // ��� ��ư�� ������ �ʱ�ȭ
        foreach (Button button in buttons)
        {
            button.GetComponent<Image>().color = defaultColor;
        }
    }

    void OnButtonClick(int clickedButtonIndex)
    {
        // ��� ��ư�� �ʱ�ȭ
        InitializeButtons();

        // Ŭ���� ��ư�� ������ ����
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

    // ���ǿ� ���� ��ư�� �� ��° �ڽ��� ��Ȱ��ȭ�ϴ� �Լ�
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
