using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public class OnMouseDown_SwitchScene : MonoBehaviour
{
    public Animator playUI;
    public Animator stateUI;
    public Animator secUI;
    public Animator backUI;
    public Animator scrollUI;
    public bool animating = false;

    public GameObject Character;
    private float appearanceTime = 4f; // 등장 시간 (초)
    private float minScale = 0.2f; // 최소 크기
    private float maxScale = 1f; // 최대 크기

    [SerializeField] private GameObject settingUI;  

    private void Start() 
    {
        SceneOption.Instance.LoadGameData(); //버튼 클릭 전에 현재 입장 가능 스테이지 정보 업데이트
    }

    public void ChangeChapter(int a)  
    { 
        SceneOption.Instance.ChapterNum = a;
    } 

    public void ChangeUnit(int b) 
    {
        SceneOption.Instance.UnitNum = b; 
    }
    
    public void StartUIMove()
    {
        if(animating == false)
        {
            animating = true;    
            SoundManager.instance.PlaySFX("SellectMenuSFX");   

            stateUI.SetBool("PlayButtonOnClick",true); 
            playUI.SetBool("PlayButtonOnClick",true);  
            secUI.SetBool("PlayButtonOnClick",true);   
            backUI.SetBool("PlayButtonOnClick",true);

            StartCoroutine(Animating());   
        }     
    }    
    public void BackUIMove()
    {
        if(animating == false)
        {
            animating = true;
            SoundManager.instance.PlaySFX("SellectMenuSFX");

            stateUI.SetBool("PlayButtonOnClick",false);
            playUI.SetBool("PlayButtonOnClick",false);

            secUI.SetBool("PlayButtonOnClick",false);
            backUI.SetBool("PlayButtonOnClick",false);

            StartCoroutine(ReloadMainMenu());

            scrollUI.SetBool("QuickPlayButtonOnClick",false);
            StartCoroutine(Animating());
        }
    }
    public void SelectUIMove(){
        if(animating==false)
        {
            animating=true; 
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
        
        //upUI.SetBool("QuickPlayButtonOnClick", true);
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
        RunningTime.Instance.ResetGame();

        LoadOtherScene("stage_unit"); 
    }

    public void ReStartGame()
    {
        SceneManager.LoadScene(SceneOption.Instance.previousModeName);
        RunningTime.Instance.ResetGame();
    }

    public void LoadNextLevel()
    {
        int nextChapterNum = SceneOption.Instance.ChapterNum;
        int nextUnitNum = SceneOption.Instance.UnitNum;

        //챕터의 마지막 Unit인지 구분
        if (SceneOption.Instance.UnitNum < 21)  
        {
            nextUnitNum++;
        }
        else
        {
            nextUnitNum = 1;
            nextChapterNum++; 
        }

        if (PlayerPrefs.GetInt("UnlockedChapterNum") <= 12)
        {
            if (nextChapterNum < PlayerPrefs.GetInt("UnlockedChapterNum"))  // 현재 Chapter가 최대 Chapter 미만일 때 씬을 전환한다 
            {
                SceneOption.Instance.UnitNum++;

                SoundManager.instance.PlaySFX("UnitButtonSFX");
                SceneManager.LoadScene("LoadingScene");
            }
            else if (nextChapterNum == PlayerPrefs.GetInt("UnlockedChapterNum") &&
                nextUnitNum <= PlayerPrefs.GetInt("UnlockedFinalUnitNum"))  // 다음 Chapter가 최대 Chapter와 같을 때는 최대 Unit이하 여부를 확인한다
            {
                SceneOption.Instance.UnitNum = 1;
                SceneOption.Instance.ChapterNum++;

                SoundManager.instance.PlaySFX("UnitButtonSFX");
                SceneManager.LoadScene("LoadingScene");
            }
            else
            {
                SoundManager.instance.PlaySFX("ErrorSFX");

            }
        }
        else
        {
            SceneManager.LoadScene("main");
        }
    }

    public void LoadRandomTest()
    {
        if (PlayerPrefs.GetInt("UnlockedChapterNum") != 1)
        {
            // 무작위로 Chapter 선택
            int randomChapterNum = UnityEngine.Random.Range(1, PlayerPrefs.GetInt("UnlockedChapterNum"));

            int randomUnitNum = UnityEngine.Random.Range(1, 22);

            // SceneOption.Instance에 값을 설정
            SceneOption.Instance.ChapterNum = randomChapterNum;
            SceneOption.Instance.UnitNum = randomUnitNum;

            SellectLevel("LoadingSceneForTest");
        }
        else
        {
            SoundManager.instance.PlaySFX("ErrorSFX");
        }
    }
}
