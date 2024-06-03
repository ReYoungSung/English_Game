using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeveloperReset : MonoBehaviour
{
    public void Reset()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("RESETTED");
    }
}
