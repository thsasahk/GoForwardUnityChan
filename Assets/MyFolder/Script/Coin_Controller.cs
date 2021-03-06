﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin_Controller : MonoBehaviour
{
    /// <summary>
    /// Canvasオブジェクト
    /// </summary>
    private GameObject canvas;
    /// <summary>
    /// Canvasオブジェクトのスクリプト
    /// </summary>
    private UIController uiController;
    /// <summary>
    /// Coinオブジェクト消滅時のAoudio Sourceを再生するオブジェクト
    /// </summary>
    public GameObject coinSound;
    /// <summary>
    /// Coinオブジェクトの移動速度
    /// </summary>
    public float speed;//{16:9 -9_1028:786 -7}
    /// <summary>
    /// 移動終了位置
    /// </summary>
    public int deadLine = -10;
    /// <summary>
    /// オブジェクトのSpriteRenderer
    /// </summary>
    private SpriteRenderer sprite;
    /// <summary>
    /// オブジェクトにGradientをつける
    /// </summary>
    public Gradient gradient;
    /// <summary>
    /// ColorGradient()の返し値
    /// </summary>
    private Color color;
    /// <summary>
    /// オブジェクトの色を個体ごとに変化させるための変数
    /// </summary>
    private float i;

    void Start()
    {
        this.canvas = GameObject.Find("Canvas");
        this.uiController = this.canvas.GetComponent<UIController>();
        this.sprite = GetComponent<SpriteRenderer>();
        this.i = Random.Range(0, 3.13f);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.uiController.isGameOver)
        {
            return;
        }
        //Coinを移動させる
        this.transform.Translate(this.speed * Time.deltaTime, 0, 0);
        //deadLineを超えたら破棄
        if(transform.position.y < this.deadLine)
        {
            Destroy(this.gameObject);
        }
        //
        this.sprite.color = ColorGradient();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Playerに接触するとCoinSoundオブジェクトを生成してUIControllerのcoinScoreを加算し、消滅する
        if(other.gameObject.tag == "Player" && !other.GetComponent<UnityChanController>().isStar)
        {
            Instantiate(this.coinSound, transform.position, Quaternion.identity);
            this.uiController.coinScore += 20;
            Destroy(gameObject);
        }        
    }

    private Color ColorGradient()
    {
        this.i += Time.deltaTime;
        this.color = this.gradient.Evaluate(Mathf.Abs(Mathf.Sin(this.i)));
        return this.color;
    }
}
