using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevelFeedback : MonoBehaviour
{
    public Text ChapterNum;
    public Text UnitNum;

    void Start()
    {
        if (PlayerPrefs.GetInt("UnlockedChapterNum") <= 12)
            ChapterNum.text = PlayerPrefs.GetInt("UnlockedChapterNum").ToString();
        else
            ChapterNum.text = "Master";

        if (PlayerPrefs.GetInt("UnlockedChapterNum") > 12)
            UnitNum.text = "100%";
        else if (PlayerPrefs.GetInt("UnlockedFinalUnitNum") < 21)
            UnitNum.text = ((PlayerPrefs.GetInt("UnlockedFinalUnitNum") * 100) / 21).ToString()+"%";
        else 
            UnitNum.text = ((1 * 100) / 21).ToString() + "%";  
    }
}
