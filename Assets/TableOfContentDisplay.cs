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

    [HideInInspector] public int TableChapterNum;

    public void Awake()
    {
        //CSV ���Ͽ��� ������ �Ľ� 
        TableDialog = CSVReader.Read("TopicOfChapters");  
    }

    public void popupTableWindow(int tableNum)
    {
        TableChapterNum = tableNum;

        Table1OfChapter.text = "";
        Table2OfChapter.text = "";

        int startRange1 = 0;
        int endRange1 = 10;
        int startRange2 = 11;
        int endRange2 = 20; 

        chapterNum.text = "Chater."+ TableChapterNum.ToString();  

        for (int i = startRange1; i <= endRange1; i++)
        {
            Table1OfChapter.text += TableDialog[i][TableChapterNum.ToString()].ToString() + "\n";  
        }

        for (int i = startRange2; i <= endRange2; i++)
        {
            Table2OfChapter.text += TableDialog[i][TableChapterNum.ToString()].ToString() + "\n";  
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
