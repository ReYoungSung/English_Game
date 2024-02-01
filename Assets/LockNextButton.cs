using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LockNextButton : MonoBehaviour
{
    private void Start()
    {
        DeactivateButtons();
    }

    // 조건에 따라 버튼의 세 번째 자식을 비활성화하는 함수
    private void DeactivateButtons()
    {
        if (SceneOption.Instance.ChapterNum < PlayerPrefs.GetInt("UnlockedChapterNum"))  
        {
            this.transform.GetChild(0).gameObject.SetActive(false);
        }
        else if (SceneOption.Instance.ChapterNum == PlayerPrefs.GetInt("UnlockedChapterNum") &&
            SceneOption.Instance.UnitNum < PlayerPrefs.GetInt("UnlockedFinalUnitNum"))    //다음 단계가 열려야지 이 버튼 잠금도 풀린다
        {
            this.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
