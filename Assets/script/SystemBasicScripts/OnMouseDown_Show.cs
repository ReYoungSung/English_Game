using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnMouseDown_Show : MonoBehaviour
{

    private static OnMouseDown_Show _instance;
    public static OnMouseDown_Show Instance
    {
        get
        {
            return _instance;
        }
    }
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    public GameObject _popup; // 인스펙터에서 넣어줄 예정

    // 시작하자마자 호출됨
    private void Awake()
    {
        _popup.SetActive(false); // 팝업은 처음에 꺼져있어야함
        _instance = this; // PopUpManager가 NULL이 되지않도록

    }

    private void OnMouseDown()
    {
        _popup.SetActive(true);
    }
}