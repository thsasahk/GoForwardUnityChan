using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    /// <summary>
    ///スクロール速度 
    /// </summary>
    public float scrollSpeed;
    /// <summary>
    /// 背景終了位置
    /// </summary>
    private float deadLine = -18f;
    /// <summary>
    ///背景開始位置
    ///</summary>
    private float startLine = 18f;
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
        if (this.uiController.isGameOver)
        {
            return;
        }
        //lengthが150を超えたら背景のスクロールは停止する
        if(this.uiController.length >= 150)
        {
            return;
        }
        //背景を移動する
        transform.Translate(this.scrollSpeed * Time.deltaTime, 0, 0);
        //画面外に出たら画面右端に移動する
        if(this.transform.position.x < this.deadLine)
        {
            this.transform.position=new Vector2(this.startLine,0);
        }
    }
}
