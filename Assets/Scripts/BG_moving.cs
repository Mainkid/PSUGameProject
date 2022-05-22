using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BG_moving : MonoBehaviour
{
    public float ScrollX = 0.5f;
    public float ScrollY = 0.5f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float OffsetX = Time.time * ScrollX;
        float OffsetY = Time.time * ScrollY;
        gameObject.GetComponent<RawImage>().material.mainTextureOffset= new Vector2(OffsetX, OffsetY);
    }
}
