using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintFunction : MonoBehaviour
{
    [SerializeField] private GameObject HintOffButton;
    [SerializeField] private GameObject HintOnButton;
    [SerializeField] private GameObject HintText;

    private void Awake() 
    {
        if (RunningTime.Instance.isHintOpen == false) 
        {
            HintOnButton.SetActive(true);
            HintOffButton.SetActive(false);
            HintText.SetActive(false); 
        }
        else
        {
            HintOnButton.SetActive(false);
            HintOffButton.SetActive(true);
            HintText.SetActive(true);
        }
    }

    private void Update()
    {
        if (RunningTime.Instance.isHintOpen == false) 
        {
            HintOnButton.SetActive(true); 
            HintOffButton.SetActive(false); 
            HintText.SetActive(false); 
        }
        else
        {
            HintOnButton.SetActive(false); 
            HintOffButton.SetActive(true); 
            HintText.SetActive(true); 
        }
    }

    public void ActiveHint(bool isActive)
    {
        RunningTime.Instance.isHintOpen = isActive;
    }
}
