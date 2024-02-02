using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnOff : MonoBehaviour
{
    [SerializeField] private GameObject SoundOffButton;
    [SerializeField] private GameObject SoundOnButton;

    private float OriginBGMVolume = 1.0f;
    private float OriginSFXVolume = 1.0f;

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
    public void ActiveSoundVolume()
    {
        SoundManager.instance.SetBGMVolume(OriginBGMVolume);
        SoundManager.instance.SetSFXVolume(OriginSFXVolume);
    }

    public void DeactiveSoundVolume()
    {
        OriginBGMVolume = SoundManager.instance.bgmVolume;
        OriginSFXVolume = SoundManager.instance.sfxVolume;

        SoundManager.instance.SetBGMVolume(0);
        SoundManager.instance.SetSFXVolume(0);
    }
}
