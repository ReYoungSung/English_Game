using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionController : MonoBehaviour
{
    private void Start()
    {
        if(this.gameObject.name == "BGMSlider")
            this.GetComponent<Slider>().value = SoundManager.instance.bgmVolume; 
        else if(this.gameObject.name == "SFXSlider")
            this.GetComponent<Slider>().value = SoundManager.instance.sfxVolume; 
    }

    public void controllingBGM()
    {
        SoundManager.instance.SetBGMVolume(this.GetComponent<Slider>().value); 
    }

    public void controllingSFX() 
    {
        SoundManager.instance.SetSFXVolume(this.GetComponent<Slider>().value);
    }
}
