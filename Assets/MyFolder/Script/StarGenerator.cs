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
    /// span変数の最低値
    /// </summary>
    public float minSpan;
    /// <summary>
    /// span変数の最大値
    /// </summary>
    public float maxSpan;
    /// <summary>
    /// 最後にStarを生成してからの時間を管理する
    /// </summary>
    public float time = 0;
    /// <summary>
    /// CubeGeneratorオブジェクト
    /// </summary>
    public GameObject cubeGenerator;
    /// <summary>
    /// CubeGeneratorオブジェクトのスクリプト
    /// </summary>
    private CubeGenerator cubeGeneratorScript;

    void Start()
    {
        this.uiController = this.canvas.GetComponent<UIController>();
        //最初のStarを生成する時間を決定する
        this.span = Random.Range(this.minSpan, this.maxSpan);
        this.cubeGeneratorScript = this.cubeGenerator.GetComponent<CubeGenerator>();
    }

    void Update()
    {
        //Boss出現中は他のオブジェクトを生成しない
        if (this.cubeGeneratorScript.isBoss)
        {
            return;
        }
        //lengthが140を超えたら破棄する
        if (this.uiController.length >= 140 || this.uiController.isGameOver)
        {
            Destroy(gameObject);
        }
        this.time += Time.deltaTime;
        if(/*this.uiController.length >= 75 && */this.time >= this.span)
        {
            Instantiate(this.star,new Vector2(13,5),Quaternion.identity);
            //次にStarを生成する時間を決定する
            this.span = Random.Range(this.minSpan, this.maxSpan);
            this.time = 0;
        }
    }
}
