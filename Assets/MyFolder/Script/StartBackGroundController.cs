using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBackGroundController : MonoBehaviour
{
    /// <summary>
    /// スクロール速度
    /// </summary>
    public float scrollSpeed;
    /// <summary>
    /// 背景終了位置
    /// </summary>
    private float deadLine = -18;
    /// <summary>
    /// 背景移動開始位置
    /// </summary>
    private float startLine = 18f;
    /// <summary>
    /// Canvasオブジェクト
    /// </summary>
    public GameObject canvas;
    /// <summary>
    /// Canvasオブジェクトのスクリプト
    /// </summary>
    private UIController uiController;
    void Start()
    {
    }

    void Update()
    {
        //背景を移動する
        transform.Translate(this.scrollSpeed * Time.deltaTime, 0, 0);
        //画面外に出たら画面右端に移動する
        if(transform.position.x < deadLine)
        {
            transform.position=new Vector2(this.startLine,0);
        }
    }
}
