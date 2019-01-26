using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    /// <summary>
    ///スクロール速度 
    /// </summary>
    private float scrollSpeed = -0.03f;
    /// <summary>
    /// 背景終了位置
    /// </summary>
    private float deadLine = -16;
    /// <summary>
    ///背景開始位置
    ///</summary>
    private float startLine = 15.8f;
    /// <summary>
    ///Canvasオブジェクト
    /// </summary>
    public GameObject canvas;
    /// <summary>
    /// Canvasオブジェクトのコンポーネントのスクリプト
    /// </summary>
    private UIController uiController;
    void Start()
    {
        this.uiController = this.canvas.GetComponent<UIController>();
    }

    // Update is called once per frame
    void Update()
    {
        //lengthが150を超えたら背景のスクロールは停止する
        if(this.uiController.length >= 150)
        {
            return;
        }
        //背景を移動する
        transform.Translate(this.scrollSpeed,0,0);
        //画面外に出たら画面右端に移動する
        if(this.transform.position.x < this.deadLine)
        {
            this.transform.position=new Vector2(this.startLine,0);
        }
    }
}
