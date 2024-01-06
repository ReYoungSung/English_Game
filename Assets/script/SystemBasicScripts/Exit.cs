using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public void OnClickExit()
    {
        Debug.Log("Button Click");
        SceneOption.Instance.SaveGameData(); 
        Application.Quit();
    }
}
