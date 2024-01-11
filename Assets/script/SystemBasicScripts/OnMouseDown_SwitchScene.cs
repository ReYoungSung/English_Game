using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class OnMouseDown_SwitchScene : MonoBehaviour
{
    public Animator playUI;
    public Animator stateUI;
    public Animator secUI;
    public Animator backUI;
    public Animator upUI;
    public Animator scrollUI;

    [SerializeField] private GameObject settingUI;

    private SoundManager soundManager;

    private void Awake()
    {
        soundManager = GameObject.Find("SoudManager").GetComponent<SoundManager>();
    }

    private void Start()
    {

        SceneOption.Instance.LoadGameData(); //버튼 클릭 전에 현재 입장 가능 스테이지 정보 업데이트
    }

    private void Update()
    {
        Debug.Log("챕터 번호:"+SceneOption.Instance.ChapterNum + " | 유닛 번호:"+ SceneOption.Instance.UnitNum);
    }

    public void ChangeChapter(int a)  
    {
        SceneOption.Instance.ChapterNum = a;  
    }

    public void ChangeUnit(int b) 
    {
        SceneOption.Instance.UnitNum = b; 
    }

    public void StartUIMove(){

        soundManager.PlaySFX("SellectMenuSFX");

        stateUI.SetBool("PlayButtonOnClick",true);
        playUI.SetBool("PlayButtonOnClick",true);
        secUI.SetBool("PlayButtonOnClick",true);
        backUI.SetBool("PlayButtonOnClick",true);

        stateUI.SetBool("Back",false);
        playUI.SetBool("Back",false);
        secUI.SetBool("Back",false);
        backUI.SetBool("BackButtonOnClick",false);
        upUI.SetBool("Back",false);
        scrollUI.SetBool("Back",false);

        LoadSettingUI(false);
    }
    public void BackUIMove(){

        soundManager.PlaySFX("SellectMenuSFX");

        stateUI.SetBool("PlayButtonOnClick",false);
        playUI.SetBool("PlayButtonOnClick",false);
        secUI.SetBool("PlayButtonOnClick",false);
        backUI.SetBool("PlayButtonOnClick",false);

        secUI.SetBool("QuickPlayButtonOnClick",false);
        upUI.SetBool("QuickPlayButtonOnClick",false);
        scrollUI.SetBool("QuickPlayButtonOnClick",false);

        upUI.SetBool("Back", true);
        scrollUI.SetBool("Back", true);
        backUI.SetBool("BackButtonOnClick", true); 

        StartCoroutine(ReloadMainMenu());
        LoadSettingUI(false);
    }
    public void SelectUIMove(){

        soundManager.PlaySFX("PlayButtonSFX");

        stateUI.SetBool("PlayButtonOnClick",false);
        playUI.SetBool("PlayButtonOnClick",false);
        secUI.SetBool("PlayButtonOnClick",false);
        backUI.SetBool("PlayButtonOnClick",false);

        secUI.SetBool("QuickPlayButtonOnClick",true);
    
        StartCoroutine(LoadChapterMenu());

        LoadSettingUI(false);
    }
    public void LoadOtherScene(string sceneName) 
    {
        soundManager.PlaySFX("SellectMenuSFX");
        SceneManager.LoadScene(sceneName); 
    }

    public void SellectLevel(string sceneName)
    {
        if (SceneOption.Instance.ChapterNum < PlayerPrefs.GetInt("UnlockedChapterNum"))  // 선택한 Chapter가 최대 Chapter 미만일 때 씬을 전환한다 
        {
            soundManager.PlaySFX("UnitButtonSFX");
            SceneManager.LoadScene(sceneName);
        }
        else if (SceneOption.Instance.ChapterNum == PlayerPrefs.GetInt("UnlockedChapterNum") &&
            SceneOption.Instance.UnitNum <= PlayerPrefs.GetInt("UnlockedFinalUnitNum"))  // 선택한 Chapter가 최대 Chapter와 같을 때는 최대 Unit이하 여부를 확인한다
        {
            soundManager.PlaySFX("UnitButtonSFX");
            SceneManager.LoadScene(sceneName);
        }
        else
            soundManager.PlaySFX("ErrorSFX");

    }

    // 팝업을 표시하기 위한 함수
    public void ShowPopup(GameObject popup)
    {
        // 팝업 창을 활성화
        popup.SetActive(true);
    }

    // 팝업 창을 닫기 위한 함수
    public void ClosePopup(GameObject popup) 
    {
        // 팝업 창을 비활성화
        popup.SetActive(false);
    }

    IEnumerator ReloadMainMenu()
    {
        yield return new WaitForSeconds(1f);

        stateUI.SetBool("Back", true);
        playUI.SetBool("Back", true);
        secUI.SetBool("Back", true);  
    }

    IEnumerator LoadChapterMenu()
    {
        yield return new WaitForSeconds(1f);
        
        upUI.SetBool("QuickPlayButtonOnClick", true);
        scrollUI.SetBool("QuickPlayButtonOnClick", true);       
    }


    public void SettingUIMove()
    {
        LoadSettingUI(true);
    }
    
    void LoadSettingUI(bool isHide = true)
    {
        settingUI.SetActive(isHide);  
    } 
}
