﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    //キューブの移動速度
    private float speed=-0.2f;

    //消滅位置
    private float deadLine=-10;

    Rigidbody2D rigid2D;

    private AudioSource[] SE;
    private AudioSource destroySE;
    private GameObject cubeGenerator;
    private GameObject canvas;
    private UIController uiController;
    public GameObject particlePrefab;
    void Start()
    {
        this.SE=GetComponents<AudioSource>();
        this.rigid2D=GetComponent<Rigidbody2D>();
        //破棄時のSEを鳴らすために、キューブジェネレイターからオーディオソースを取得
        this.cubeGenerator=GameObject.Find("CubeGenerator");
        this.destroySE=this.cubeGenerator.GetComponent<AudioSource>();
        //isGameOverの獲得
        this.canvas=GameObject.Find("Canvas");
        this.uiController=this.canvas.GetComponent<UIController>();
    }

    void Update()
    {
        //キューブを移動させる
        transform.Translate(this.speed,0,0);
        //画面外に出たら破棄する
        if(transform.position.x<deadLine)
        {
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag=="Stage"||other.gameObject.tag=="Block"||other.gameObject.tag=="HBlock")
        {
            this.SE[0].Play();
        }        
    }

    void OnDestroy()
    {
        //スクロールによる破棄やシーンのロードではSEは鳴らない
        if(transform.position.x>deadLine&&this.uiController.isGameOver==false&&this.gameObject.tag=="Block")
        {
            this.destroySE.Play();
            Instantiate(this.particlePrefab,transform.position,Quaternion.identity);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag=="Bomb"&&this.gameObject.tag=="HBlock")
        {
            this.SE[1].Play();
        }
    }
}