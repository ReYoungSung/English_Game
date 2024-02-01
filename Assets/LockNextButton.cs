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

    // ���ǿ� ���� ��ư�� �� ��° �ڽ��� ��Ȱ��ȭ�ϴ� �Լ�
    private void DeactivateButtons()
    {
        if (SceneOption.Instance.ChapterNum < PlayerPrefs.GetInt("UnlockedChapterNum"))  
        {
            this.transform.GetChild(0).gameObject.SetActive(false);
        }
        else if (SceneOption.Instance.ChapterNum == PlayerPrefs.GetInt("UnlockedChapterNum") &&
            SceneOption.Instance.UnitNum < PlayerPrefs.GetInt("UnlockedFinalUnitNum"))    //���� �ܰ谡 �������� �� ��ư ��ݵ� Ǯ����
        {
            this.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
