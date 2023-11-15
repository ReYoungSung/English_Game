using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 터치하면 지운다
public class OnMouseDown_Hide : MonoBehaviour
{
    public GameObject target;

    private void OnMouseDown()
    {
        target.SetActive(false);
    }
}
