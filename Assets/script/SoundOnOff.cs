using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnOff : MonoBehaviour
{
    // Update is called once per frame
    public void SetSoundVolume(int a)
    {
        SoundManager.instance.SetBGMVolume(a);
        SoundManager.instance.SetSFXVolume(a);
    }
}
