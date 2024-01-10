using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour
{
    public GameObject popup;
    public GameObject backgroundUI; // 백그라운드 UI 요소

    // 팝업을 표시하기 위한 함수
    public void ShowPopup()
    {
        // 백그라운드 UI 요소를 비활성화
        backgroundUI.SetActive(false);   

        // 팝업 창을 활성화
        popup.SetActive(true);   
    }

    // 팝업 창을 닫기 위한 함수
    public void ClosePopup()
    {
        // 팝업 창을 비활성화
        popup.SetActive(false);   

        // 백그라운드 UI 요소를 다시 활성화
        backgroundUI.SetActive(true);   
    }

    // 다음 씬으로 이동하기 위한 함수
    public void LoadNextScene()
    {
        SceneManager.LoadScene("다음 씬의 이름");   
    }
}
