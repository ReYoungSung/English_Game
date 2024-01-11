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
        ChapterNum.text = PlayerPrefs.GetInt("UnlockedChapterNum").ToString();
        UnitNum.text = ((PlayerPrefs.GetInt("UnlockedFinalUnitNum") * 100) / 21).ToString()+"%";
    }
}
