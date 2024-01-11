using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnOff : MonoBehaviour
{
    [SerializeField] private GameObject SoundOffButton;
    [SerializeField] private GameObject SoundOnButton;

    private void Update()
    {
        if(SoundManager.instance.bgmVolume == 0)
        {
            SoundOnButton.SetActive(true);
            SoundOffButton.SetActive(false);
        }
        else
        {
            SoundOnButton.SetActive(false);
            SoundOffButton.SetActive(true);
        }
    }

    // Update is called once per frame
    public void SetSoundVolume(int a)
    {
        SoundManager.instance.SetBGMVolume(a);
        SoundManager.instance.SetSFXVolume(a);
    }
}
