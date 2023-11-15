using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sometimesmove : MonoBehaviour
{
    public float moveMax;
    public float speed;

    Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dirPos = pos;
        dirPos.y = dirPos.y + moveMax * Mathf.Sin(Time.time * speed);
        transform.position = dirPos;
    }
}
