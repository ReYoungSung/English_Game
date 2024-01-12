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

    private void Update()
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
        else if(RunningTime.Instance.CheckTurnNum == 2)
        {
            Blank1.SetActive(true);
            Blank2.SetActive(true);
        }
        else if(RunningTime.Instance.CheckTurnNum == 3)
        {
            Blank1.SetActive(true);
            Blank2.SetActive(true);
            Blank3.SetActive(true);
        }

        if(SceneOption.Instance.CurrentLevelNumber != 1)
            progressBar.value = (float)(SceneOption.Instance.CurrentLevelNumber-1)/15.0f; 

    }
}
