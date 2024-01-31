using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableOfContentDisplay : MonoBehaviour
{
    
    [SerializeField] private GameObject tableWindow1;  
    private List<Dictionary<string, object>> TableDialog;

    public Text Table1OfChapter; 
    public Text Table2OfChapter;
    [SerializeField] private Text chapterNum;

    public void Awake()
    {
        //CSV 파일에서 데이터 파싱 
        TableDialog = CSVReader.Read("TopicOfChapters");  
    }

    public void popupTableWindow()
    {
        Table1OfChapter.text = "";
        Table2OfChapter.text = "";

        int startRange1 = 0;
        int endRange1 = 10;
        int startRange2 = 11;
        int endRange2 = 20;

        chapterNum.text = SceneOption.Instance.ChapterNum.ToString();

        for (int i = startRange1; i <= endRange1; i++)
        {
            Table1OfChapter.text += TableDialog[i][SceneOption.Instance.ChapterNum.ToString()].ToString() + "\n";
        }

        for (int i = startRange2; i <= endRange2; i++)
        {
            Table2OfChapter.text += TableDialog[i][SceneOption.Instance.ChapterNum.ToString()].ToString() + "\n";
        }

        SoundManager.instance.PlaySFX("SellectMenuSFX");
        tableWindow1.SetActive(true);
    }

    public void unPopupTableWindow()
    {
        SoundManager.instance.PlaySFX("SellectMenuSFX"); 
        tableWindow1.SetActive(false); 
    } 
}
