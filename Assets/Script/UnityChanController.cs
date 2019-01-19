﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityChanController : MonoBehaviour
{
    //アニメーションするためのコンポーネントを入れる
    Animator animator;

    //ユニティちゃんを移動させるコンポーネントを入れる
    Rigidbody2D rigid2D;

    //
    public AudioSource[] unitySE;

    //地面の位置
    private float groundLevel=-3.0f;

    //ジャンプ速度の減衰
    private float dump=0.8f;

    //ジャンプの速度
    float jumpVelocity=20;

    //ゲームオーバーになる位置
    private float deadLine=-9;

    //球
    public GameObject bomb;
    public GameObject bombLv2;
    private BombController bombController;
    
    //球のチャージ時間
    public float chargeTime=0;

    //チャージの完了時間
    public float maxCharge;

    //チャージのレベルを設定
    public int chargeLV=0;

    //スライダーを用意
    public Slider chargeSlider;

    void Start()
    {
        //アニメーターのコンポーネントを取得
        this.animator=GetComponent<Animator>();
        //Rigidbody2Dのコンポーネントを取得
        this.rigid2D=GetComponent<Rigidbody2D>();
        //
        this.unitySE=GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //ユニティちゃんが画面右に行くのを禁止
        if(this.gameObject.transform.position.x>-2.9f)
        {
            this.gameObject.transform.position=new Vector2(-2.9f,this.gameObject.transform.position.y);
        }
        //走るアニメーションを再生するために、Animatorのパラメータを調節する
        this.animator.SetFloat("Horizontal", 1);

        //着地しているかどうかを調べる
        bool isGround=(this.transform.position.y>this.groundLevel)?false:true;
        this.animator.SetBool("isGround",isGround);

        //ジャンプ状態のときはボリュームを0にする
        this.unitySE[1].volume=(isGround)?1:0;

        //着地状態でクリックされた場合
        if(isGround&&Input.GetMouseButtonDown(0))
        {
            //上方向への力をかける
            this.rigid2D.velocity=new Vector2(0,this.jumpVelocity);
        }
        //クリックをやめたら減速する
        if(Input.GetMouseButton(0)==false)
        {
            if(this.rigid2D.velocity.y>0)
            {
                this.rigid2D.velocity*=dump;
            }
        }

        //チャージ音を再生する
        if(Input.GetMouseButtonDown(1))this.unitySE[0].Play();
        //チャージする
        if(Input.GetMouseButton(1))
        {
            this.chargeTime+=Time.deltaTime;
            if(chargeTime>=maxCharge)
            {
                switch(this.chargeLV)
                {
                    case 0:
                    this.chargeLV=1;
                    this.chargeTime=0;
                    break;

                    case 1:
                    case 2:
                    this.chargeLV=2;
                    this.chargeTime=maxCharge;
                    break;
                }
            }
        }
        //発射
        if(Input.GetMouseButtonUp(1))
        {
            this.unitySE[0].Stop();
            if(chargeLV==1)
            {
                //bombのBombControllerを取得
                this.bombController=this.bomb.GetComponent<BombController>();
                this.bombController.Lv=1;
                Instantiate(bomb,new Vector2(transform.position.x+1.0f,transform.position.y-0.3f),Quaternion.identity);
            }else if(chargeLV==2)
            {
                //bombLv2のBombControllerを取得
                this.bombController=this.bombLv2.GetComponent<BombController>();
                this.bombController.Lv=2;
                GameObject BombLv2=Instantiate(this.bombLv2)as GameObject;
                BombLv2.transform.position=new Vector2(transform.position.x+1.0f,transform.position.y-0.3f);
                BombLv2.transform.Rotate(0.0f,0.0f,0.0f);
            }
            this.chargeTime=0.0f;
            this.chargeLV=0;
        }
        //スライダーの値を変化させる
        this.chargeSlider.value=this.chargeTime;


        //デッドラインを超えた場合ゲームオーバーにする
        if(transform.position.x<deadLine)
        {
            //UIControllerのGameOver関数を呼び出して画面上に「GameOver」と表示する
            GameObject.Find("Canvas").GetComponent<UIController>().GameOver();

            //ユニティちゃんを破棄する
            Destroy(gameObject);
        }

        //ChargeSliderの色を変化させる
        switch(this.chargeLV)
        {
            case 0:
            ChangeSliderColor(new Color32(95,255,187,255),new Color32(0,0,0,255));
            break;

            case 1:
            case 2:
            ChangeSliderColor(new Color32(59,255,72,255),new Color32(95,255,187,255));
            break;
        }
    }

    public void ChangeSliderColor(Color fillColor, Color backgroundColor)
    {
        Image[] images = this.chargeSlider.GetComponentsInChildren<Image>();
        foreach (var image in images)
        {
            if (image.name == "Background")
                image.color = backgroundColor;
            else if (image.name == "Fill")
                image.color = fillColor;
        }
    }
}