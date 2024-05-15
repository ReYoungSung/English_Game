using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class ChapterButtonManager : MonoBehaviour 
{
    public Button[] buttons;
    public GameObject popup;
    private LicenseUnlockManager licenseUnlockManager = null;
    private bool lockDeactivated = false;

    private Color defaultColor = new Color(1f, 1f, 1f, 1f); // Set the alpha value to 0.9
    private Color toggleColor = new Color(0.8f, 0.8f, 0.8f, 1f); // Set the alpha value to 1 

    private void Awake()
    {
        licenseUnlockManager = GameObject.Find("LicenseManager").GetComponent<LicenseUnlockManager>();
    }

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
        InitializeButtons();
        Invoke("DeactivateButtons",2f);
    }

    private void Update()
    {
        if(!lockDeactivated && licenseUnlockManager.LicensesUnlocked)
        {
            DeactivateAllChapterLocks();
            lockDeactivated = true;
        }
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
        buttons[clickedButtonIndex].GetComponent<Image>().color = toggleColor;

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

    public void DeactivateAllChapterLocks()
    {
        PlayerPrefs.SetInt("UnlockedChapterNum", buttons.Length);
        for(int i =0; i < buttons.Length; ++i)
            DeactivateChapterLock(i);
    }

    public void DeactivateChapterLock(int buttonIndex)
    {
        buttons[buttonIndex].transform.GetChild(2).gameObject.SetActive(false);
        buttons[buttonIndex].GetComponent<Image>().enabled = true;
    }
}
