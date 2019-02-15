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
    /// <summary>
    /// Stalkerオブジェクト
    /// </summary>
    [SerializeField]private GameObject stalker;
    /// <summary>
    /// Stalkerオブジェクトのスクリプト
    /// </summary>
    private StalkerController stalkerController;
    /// <summary>
    /// 時間計測用の変数
    /// </summary>
    private float count;

    void Start()
    {
        this.uiController = this.canvas.GetComponent<UIController>();
        this.stalkerController = this.stalker.GetComponent<StalkerController>();
    }

    void Update()
    {
        if (this.uiController.isGameOver || this.uiController.clear)
        {
            return;
        }

        if (this.uiController.clearScene)
        {
            this.count += Time.deltaTime;
            if (this.count >= 6.5 && this.count <= 9.0f)
            {
                return;
            }
        }
        /*StalkerControllerから背景のスクロールを制御する
        if (this.stalkerController.scrollStop)
        {
            this.count += Time.deltaTime;
            if (this.count >= 2.5f)
            {
                this.stalkerController.scrollStop = false;
            }
            return;
        }*/

        //lengthが150を超えたら背景のスクロールは停止する
        //if(this.uiController.length >= 150)
        //{
        //    return;
        //}
        //背景を移動する
        transform.Translate(this.scrollSpeed * Time.deltaTime, 0, 0);
        //画面外に出たら画面右端に移動する
        if(this.transform.position.x < this.deadLine)
        {
            this.transform.position=new Vector2(this.startLine,0);
        }
    }
}
