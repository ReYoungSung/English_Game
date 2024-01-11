using System.Collections;
using System.Collections.Generic;
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
    public bool animating = false;

    [SerializeField] private GameObject settingUI;

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
        if(animating==false){
            animating=true;
            SoundManager.instance.PlaySFX("SellectMenuSFX");

            stateUI.SetBool("PlayButtonOnClick",true);
            playUI.SetBool("PlayButtonOnClick",true);
            secUI.SetBool("PlayButtonOnClick",true);
            backUI.SetBool("PlayButtonOnClick",true);

            StartCoroutine(Animating());
        }
    }
    public void BackUIMove(){
        if(animating==false){
            animating=true;
            SoundManager.instance.PlaySFX("SellectMenuSFX");

            stateUI.SetBool("PlayButtonOnClick",false);
            playUI.SetBool("PlayButtonOnClick",false);

            secUI.SetBool("PlayButtonOnClick",false);
            backUI.SetBool("PlayButtonOnClick",false);

            StartCoroutine(ReloadMainMenu());

            upUI.SetBool("QuickPlayButtonOnClick",false);
            scrollUI.SetBool("QuickPlayButtonOnClick",false);
            StartCoroutine(Animating());
        }
    }
    public void SelectUIMove(){
        if(animating==false){
            animating=true; 
            upUI.SetBool("QuickPlayButtonOnClick",true);
            scrollUI.SetBool("QuickPlayButtonOnClick",true);
        
            SoundManager.instance.PlaySFX("PlayButtonSFX");

            secUI.SetBool("PlayButtonOnClick",false);

            StartCoroutine(LoadChapterMenu());

            StartCoroutine(Animating());
        }
    }
    public void LoadOtherScene(string sceneName) 
    {
        SoundManager.instance.PlaySFX("SellectMenuSFX");
        SceneManager.LoadScene(sceneName); 
    }

    public void SellectLevel(string sceneName)
    {
        if (SceneOption.Instance.ChapterNum < PlayerPrefs.GetInt("UnlockedChapterNum"))  // 선택한 Chapter가 최대 Chapter 미만일 때 씬을 전환한다 
        {
            SoundManager.instance.PlaySFX("UnitButtonSFX");
            SceneManager.LoadScene(sceneName);
        }
        else if (SceneOption.Instance.ChapterNum == PlayerPrefs.GetInt("UnlockedChapterNum") &&
            SceneOption.Instance.UnitNum <= PlayerPrefs.GetInt("UnlockedFinalUnitNum"))  // 선택한 Chapter가 최대 Chapter와 같을 때는 최대 Unit이하 여부를 확인한다
        {
            SoundManager.instance.PlaySFX("UnitButtonSFX");
            SceneManager.LoadScene(sceneName);
        }
        else
            SoundManager.instance.PlaySFX("ErrorSFX");

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

        stateUI.SetBool("PlayButtonOnClick",false);
        secUI.SetBool("PlayButtonOnClick",false);  
    }

    IEnumerator LoadChapterMenu()
    {
        yield return new WaitForSeconds(1f);
        
        upUI.SetBool("QuickPlayButtonOnClick", true);
        scrollUI.SetBool("QuickPlayButtonOnClick", true);       
    }

    IEnumerator Animating()
    {
        yield return new WaitForSeconds(1f);
        animating = false;    
    }

    public void quickStart()
    {
        SoundManager.instance.PlaySFX("UnitButtonSFX");

        ChangeChapter(PlayerPrefs.GetInt("UnlockedChapterNum"));
        ChangeUnit(PlayerPrefs.GetInt("UnlockedFinalUnitNum"));

        LoadOtherScene("stage_unit");
    }
}
