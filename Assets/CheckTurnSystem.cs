using InGameScript;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckTurnSystem : MonoBehaviour
{
    [SerializeField] private GameObject Blank1;
    [SerializeField] private GameObject Blank2;
    [SerializeField] private GameObject Blank3;

    [SerializeField] private Slider progressBar;
    [SerializeField] private Text chText;
    [SerializeField] private Text unitText;

    [SerializeField] private GameManager gameManager;
     
    private void Start()
    {
        chText.text = "CH" + SceneOption.Instance.ChapterNum.ToString();
        unitText.text = "UNIT" + SceneOption.Instance.UnitNum.ToString();
    }

    private void Update()
    {
        if (gameManager.currentGameMode == GameManager.GameMode.test)
        {
            updateHeartNum();
        }
        else
        {
            updateTurnNum();
        }
    }

    private void updateTurnNum()
    {
        if (RunningTime.Instance.CheckTurnNum == 0)
        {
            Blank1.SetActive(false);
            Blank2.SetActive(false);
            Blank3.SetActive(false);
        }
        else if (RunningTime.Instance.CheckTurnNum == 1)
        {
            Blank1.SetActive(true);
        }
        else if (RunningTime.Instance.CheckTurnNum == 2)
        {
            Blank1.SetActive(true);
            Blank2.SetActive(true);
        }
        else if (RunningTime.Instance.CheckTurnNum == 3)
        {
            Blank1.SetActive(true);
            Blank2.SetActive(true);
            Blank3.SetActive(true);
        }

        if (SceneOption.Instance.CurrentLevelNumber != 1)
            progressBar.value = (float)(SceneOption.Instance.CurrentLevelNumber - 1) / 15.0f;
    }

    private void updateHeartNum()
    {
        if (RunningTime.Instance.MissingPoint == 0)
        {
            Blank1.SetActive(true);
            Blank2.SetActive(true);
            Blank3.SetActive(true);
        }
        else if (RunningTime.Instance.MissingPoint == 1)
        {
            Blank3.SetActive(false);
        }
        else if (RunningTime.Instance.MissingPoint == 2)
        {
            Blank2.SetActive(false);
            Blank3.SetActive(false);
        }
        else if (RunningTime.Instance.MissingPoint == 3)
        {
            Blank1.SetActive(false);
            Blank2.SetActive(false);
            Blank3.SetActive(false);
        }

        if (SceneOption.Instance.CurrentLevelNumber != 1)
            progressBar.value = (float)(SceneOption.Instance.CurrentLevelNumber - 1) / 9.0f;
    }
}

