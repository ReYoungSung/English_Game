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
        // �� ��ư�� Ŭ�� �̺�Ʈ �߰�
        /*
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // Ŭ���� ������ �ذ��ϱ� ���� ������ ����
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

    // ���ǿ� ���� ��ư�� �� ��° �ڽ��� ��Ȱ��ȭ�ϴ� �Լ�
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
