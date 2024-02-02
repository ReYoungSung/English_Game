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
            ChapterNum.text = "CH."+PlayerPrefs.GetInt("UnlockedChapterNum").ToString();
        else
            ChapterNum.text = "Master";

        if (PlayerPrefs.GetInt("UnlockedChapterNum") > 21)
            UnitNum.text = "Finish"; 
        else if (PlayerPrefs.GetInt("UnlockedFinalUnitNum") < 21)
            UnitNum.text = PlayerPrefs.GetInt("UnlockedFinalUnitNum").ToString();
        else 
            UnitNum.text = PlayerPrefs.GetInt("UnlockedFinalUnitNum").ToString();
    }
}
