using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class ChapterButtonManager : MonoBehaviour 
{
    public Button[] buttons;
    public GameObject popup;
    private LicenseUnlockManager licenseUnlockManager = null;
    private bool lockDeactivated = false;

    private Color defaultColor = new Color(1f, 1f, 1f, 1f); // Set the alpha value to 0.9
    private Color toggleColor = new Color(0.8f, 0.8f, 0.8f, 1f); // Set the alpha value to 1 

    void Start()
    {
        // �� ��ư�� Ŭ�� �̺�Ʈ �߰�
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // Ŭ���� ������ �ذ��ϱ� ���� ������ ����
            buttons[i].onClick.AddListener(() => OnButtonClick(index));
        }

        // DeactivateButtons �Լ��� ȣ���Ͽ� ���ǿ� ���� �� ��° �ڽ��� ��Ȱ��ȭ
        DeactivateButtons();
        InitializeButtons();
        LicenseUnlockManager.Instance.VerifyLicense();
        Invoke("DeactivateButtons",2f);
    }

    private void Update()
    {
        if(!lockDeactivated && LicenseUnlockManager.Instance.LicensesUnlocked)
        {
            DeactivateAllPurchaseLocks();
            lockDeactivated = true;
        }
    }

    public void InitializeButtons()
    {
        int first = 0;
        // ��� ��ư�� ������ �ʱ�ȭ
        foreach (Button button in buttons)
        {
            button.GetComponent<Image>().color = defaultColor;
            RectTransform buttonRectTransform = button.GetComponent<RectTransform>();
            buttonRectTransform.localScale = new Vector3(5.0f, 5.0f, 5.0f);

            if (0 < first)
            {
                button.transform.GetChild(3).gameObject.GetComponent<CodelessIAPButton>().
                    onPurchaseComplete.AddListener(
                        LicenseUnlockManager.Instance.OnUnlockChapterAction
                    );
            }
            else
            {
                button.transform.GetChild(3).gameObject.SetActive(false);
            }
            ++first;
        }
    }

    void OnButtonClick(int clickedButtonIndex)
    {
        // ��� ��ư�� �ʱ�ȭ
        InitializeButtons();

        // Ŭ���� ��ư�� ������ ����
        buttons[clickedButtonIndex].GetComponent<Image>().color = toggleColor;

        // ��ư�� ũ�⸦ 1.1��� Ű���
        RectTransform buttonRectTransform = buttons[clickedButtonIndex].GetComponent<RectTransform>();
        buttonRectTransform.localScale = new Vector3(5.5f, 5.5f, 5.5f);
    }

    public void ShowPopup()
    {   
        if (SceneOption.Instance.ChapterNum <= PlayerPrefs.GetInt("UnlockedChapterNum"))  
        {
            // �˾� â�� Ȱ��ȭ
            popup.SetActive(true);
            SoundManager.instance.PlaySFX("SellectMenuSFX");
        }
        else
        {
            SoundManager.instance.PlaySFX("ErrorSFX");
            ClosePopup();    
        }
    }

    // �˾� â�� �ݱ� ���� �Լ�
    public void ClosePopup()
    {
        // �˾� â�� ��Ȱ��ȭ
        popup.SetActive(false);
    } 

    // ���ǿ� ���� ��ư�� �� ��° �ڽ��� ��Ȱ��ȭ�ϴ� �Լ�
    public void DeactivateButtons()
    {
        int unlockedChapterNum = PlayerPrefs.GetInt("UnlockedChapterNum");

        for (int i = 0; i < buttons.Length; i++)
        {
            if (i + 1 <= unlockedChapterNum)
            {
                buttons[i].transform.GetChild(2).gameObject.SetActive(false);
                buttons[i].GetComponent<Image>().enabled = true; 
            }
        }
    }

    public void DeactivateAllPurchaseLocks()
    {
        for(int i =0; i < buttons.Length; ++i)
            DeactivatePurchaseLock(i);
    }

    public void DeactivatePurchaseLock(int buttonIndex)
    {
        buttons[buttonIndex].transform.GetChild(3).gameObject.SetActive(false);
        buttons[buttonIndex].GetComponent<Image>().enabled = true;
    }
}
