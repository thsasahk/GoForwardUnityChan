using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBackGroundController : MonoBehaviour
{
    //スクロール速度
    private float scrollSpeed = -0.03f;
    //背景終了位置
    private float deadLine = -16;
    //背景開始位置
    private float startLine = 15.8f;
    public GameObject canvas;
    private UIController uiController;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //背景を移動する
        transform.Translate(scrollSpeed,0,0);
        //画面外に出たら画面右端に移動する
        if(transform.position.x < deadLine)
        {
            transform.position=new Vector2(this.startLine,0);
        }
    }
}
