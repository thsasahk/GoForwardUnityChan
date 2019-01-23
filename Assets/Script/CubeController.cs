﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    //キューブの移動速度
    public float speed = -0.2f;
    //消滅位置
    public float deadLine = -10;
    Rigidbody2D rigid2D;
    private AudioSource[] SE;
    private GameObject canvas;
    private UIController uiController;
    public GameObject particlePrefab;
    public int life;
    void Start()
    {
        this.SE = GetComponents<AudioSource>();
        this.rigid2D = GetComponent<Rigidbody2D>();
        //isGameOverの獲得
        this.canvas = GameObject.Find("Canvas");
        this.uiController = this.canvas.GetComponent<UIController>();
    }

    void Update()
    {
        //キューブを移動させる
        transform.Translate(this.speed,0,0);
        //画面外に出たら破棄する
        if(transform.position.x < deadLine)
        {
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Stage"
        ||other.gameObject.tag == "Block"
        ||other.gameObject.tag == "HBlock")
        {
            this.SE[0].Play();
        }
    }

    void OnDestroy()
    {
        //スクロールによる破棄やシーンのロードではパーティクルを生成しない
        if(transform.position.x > deadLine
        &&this.uiController.isGameOver == false)
        {
            Instantiate(this.particlePrefab,transform.position,Quaternion.identity);

            //キューブが破壊された際にスコアに加算
            this.uiController.cubeScore += 10;
        }
    }
    
    public void Damage(int i)
    {
        if(gameObject.tag == "HBlock")
        {
            this.SE[1].Play();
            return;
        }
        this.life -= i;
        if(this.life < 1)
        {
            Destroy(gameObject);
        }
    }
}