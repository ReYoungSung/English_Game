using UnityEngine;
using UnityEngine.UI;

public class ChapterButtonManager : MonoBehaviour 
{
    public Button[] buttons;
    public GameObject popup;

    private Color defaultColor = new Color(1f, 1f, 1f, 0.7f); // Set the alpha value to 0.7 (180/255)

    void Start()
    {
        // �� ��ư�� Ŭ�� �̺�Ʈ �߰�
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // Ŭ���� ������ �ذ��ϱ� ���� ������ ����
            buttons[i].onClick.AddListener(() => OnButtonClick(index));
        }
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

    public void ShowPopup()
    {
        if (SceneOption.Instance.ChapterNum <= PlayerPrefs.GetInt("UnlockedChapterNum"))  
        {
            // �˾� â�� Ȱ��ȭ
            popup.SetActive(true);     
        }
        else
        {
            Debug.Log("���� ������ �� �����ϴ�");   
            ClosePopup();    
        }
    }

    // �˾� â�� �ݱ� ���� �Լ�
    public void ClosePopup()
    {
        // �˾� â�� ��Ȱ��ȭ
        popup.SetActive(false);
    }
}
