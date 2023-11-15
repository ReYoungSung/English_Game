using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextBtn : MonoBehaviour
{
    public Button btn;
    private static NextBtn _instance;
    public static NextBtn Instance
    {
        get
        {
            return _instance;
        }
    }

    void Start()
    {
        btn.onClick.AddListener(btn1print);
    }

    public GameObject button; // 인스펙터에서 넣어줄 예정

    // 시작하자마자 호출됨
    private void Awake()
    {
        button.SetActive(false); // 팝업은 처음에 꺼져있어야함
        _instance = this; // PopUpManager가 NULL이 되지않도록

    }
    private void OnMouseDown()
    {
        button.SetActive(true);
    }

    void btn1print()
    {
        Debug.Log("Stage Clear");
    }
    public GameObject a;
    public GameObject b;
    public GameObject c;

}
