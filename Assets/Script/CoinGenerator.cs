using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    /// <summary>
    /// 次のコインを生成するまでの時間
    /// </summary>
    private int span;
    /// <summary>
    /// 最後にコインを生成してから経った時間
    /// </summary>
    private float waitTime;
    /// <summary>
    /// Coinオブジェクト
    /// </summary>
    public GameObject coinPrefab;
    /// <summary>
    /// GameScene開始時最初にCoinを生成するまでの最短時間
    /// </summary>
    public int startSpan1 = 5;
    /// <summary>
    /// GameScene開始時最初にCoinを生成するまでの最長時間
    /// </summary>
    public int startSpan2 = 8;
    /// <summary>
    /// Coinを生成するまでの最短時間
    /// </summary>
    public int span1 = 3;
    /// <summary>
    /// Coinを生成するまでの最長時間
    /// </summary>
    public int span2 = 6;
    /// <summary>
    /// Canvasオブジェクト
    /// </summary>
    public GameObject canvas;
    /// <summary>
    /// Canvasオブジェクトのスクリプト
    /// </summary>
    private UIController uiController;
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
        //GameScene開始から最初にCoinを生成するまでの時間を決定する
        this.span=Random.Range(this.startSpan1,this.startSpan2);
        this.uiController = this.canvas.GetComponent<UIController>();
        this.cubeGeneratorScript = this.cubeGenerator.GetComponent<CubeGenerator>();
    }

    
    void Update()
    {
        //Boss出現中は他のオブジェクトを生成しない
        if (this.cubeGeneratorScript.isBoss)
        {
            return;
        }
        //UIControllerのlengthが147を超えたら破棄される
        if (this.uiController.length >= 147 || this.uiController.isGameOver)
        {
            Destroy(gameObject);
        }
        this.waitTime += Time.deltaTime;
        if (this.waitTime >= this.span)
        {
            //Coinオブジェクトを生成するY座標を決定する
            float m = Random.Range(-4.5f,1.5f);
            Instantiate(this.coinPrefab,new Vector2(12,m),Quaternion.identity);
            //次にCoinオブジェクトを生成するまでのspanを決定する
            this.span = Random.Range(this.span1,this.span2);
            //Coin生成から経った時間を初期化
            this.waitTime = 0;
        }
    }
}
