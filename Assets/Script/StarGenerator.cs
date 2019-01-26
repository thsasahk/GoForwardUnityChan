using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarGenerator : MonoBehaviour
{
    /// <summary>
    /// Starオブジェクト
    /// </summary>
    public GameObject star;
    /// <summary>
    /// Canvasオブジェクト
    /// </summary>
    public GameObject canvas;
    /// <summary>
    /// Canvasオブジェクトのスクリプト
    /// </summary>
    private UIController uiController;
    /// <summary>
    /// Starの生成間隔を決定する
    /// </summary>
    public float span;
    /// <summary>
    /// span変数にランダム性を持たせるための変数
    /// </summary>
    public float coolTime;
    /// <summary>
    /// 最後にStarを生成してからの時間を管理する
    /// </summary>
    private float time = 0;
    void Start()
    {
        this.uiController = this.canvas.GetComponent<UIController>();
        //最初のStarを生成する時間を決定する
        this.span = Random.Range(this.coolTime,this.coolTime + 5.0f);
    }
    void Update()
    {
        //lengthが140を超えたら破棄する
        if(this.uiController.length >= 140)
        {
            Destroy(gameObject);
        }
        this.time += Time.deltaTime;
        if(this.uiController.length >= 100 && this.time >= this.span)
        {
            Instantiate(this.star,new Vector2(13,5),Quaternion.identity);
            //次にStarを生成する時間を決定する
            this.span = Random.Range(this.coolTime,this.coolTime + 5.0f);
            this.time = 0;
        }
    }
}
