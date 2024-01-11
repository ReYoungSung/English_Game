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
    }
    public void BackUIMove(){
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
    }
    public void SelectUIMove(){
        stateUI.SetBool("PlayButtonOnClick",false);
        playUI.SetBool("PlayButtonOnClick",false);
        secUI.SetBool("PlayButtonOnClick",false);
        backUI.SetBool("PlayButtonOnClick",false);

        secUI.SetBool("QuickPlayButtonOnClick",true);

        StartCoroutine(LoadChapterMenu());
    }
    public void LoadOtherScene(string sceneName) 
    {
        SceneManager.LoadScene(sceneName); 
    }

    public void SellectLevel(string sceneName)
    {
        if (SceneOption.Instance.ChapterNum < PlayerPrefs.GetInt("UnlockedChapterNum"))  // 선택한 Chapter가 최대 Chapter 미만일 때 씬을 전환한다 
            SceneManager.LoadScene(sceneName);
        else if (SceneOption.Instance.ChapterNum == PlayerPrefs.GetInt("UnlockedChapterNum") &&
            SceneOption.Instance.UnitNum <= PlayerPrefs.GetInt("UnlockedFinalUnitNum"))  // 선택한 Chapter가 최대 Chapter와 같을 때는 최대 Unit이하 여부를 확인한다
            SceneManager.LoadScene(sceneName);
        else
            Debug.Log("아직 입장할 수 없습니다");
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
}
