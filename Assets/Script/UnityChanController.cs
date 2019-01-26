using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityChanController : MonoBehaviour
{
    /// <summary>
    /// オブジェクトのAnimator
    /// </summary>
    private Animator animator;
    /// <summary>
    /// オブジェクトのRigidbody2D
    /// </summary>
    Rigidbody2D rigid2D;
    /// <summary>
    /// オブジェクトのAudioSource
    /// </summary>
    public AudioSource[] unitySE;
    /// <summary>
    /// オブジェクトが歩く地面の高さ
    /// </summary>
    private float groundLevel = -3.0f;
    /// <summary>
    /// ジャンプ速度の減衰
    /// </summary>
    public float dump = 0.8f;
    /// <summary>
    /// ジャンプの速度
    /// </summary>
    public float jumpVelocity=20;
    /// <summary>
    /// ゲームオーバーになる位置
    /// </summary>
    public float deadLine=-9;
    /// <summary>
    /// chargeLV0で発射される弾
    /// </summary>
    public GameObject bombLv0;
    /// <summary>
    /// chargeLV1で発射される弾
    /// </summary>
    public GameObject bombLv1;
    /// <summary>
    /// chargeLV2で発射される弾
    /// </summary>
    public GameObject bombLv2;
    /// <summary>
    /// Bombオブジェクトのスクリプト
    /// </summary>
    private BombController bombController;
    /// <summary>
    /// 右クリックを押してからたった時間
    /// </summary>
    public float chargeTime=0;
    /// <summary>
    /// chargeLVが一つ上がるまでの時間
    /// </summary>
    public float maxCharge;
    /// <summary>
    /// 溜めた時間によってchargeのレベルを定義し、発射されるBombの種類を管理する
    /// </summary>
    public int chargeLV=0;
    /// <summary>
    /// chargeTimeを視覚化するオブジェクト
    /// </summary>
    public Slider chargeSlider;
    /// <summary>
    /// Canvasオブジェクト
    /// </summary>
    public GameObject canvas;
    /// <summary>
    /// Canvasオブジェクトのスクリプト
    /// </summary>
    private UIController uiController;
    /// <summary>
    /// オブジェクトの接地状態を入力する
    /// </summary>
    private bool isGround;
    /// <summary>
    /// clear時のオブジェクトの歩行速度
    /// </summary>
    public float clearWalk;
    /// <summary>
    /// clear時に生成されるParticleSystem赤、白
    /// </summary>
    public GameObject clearParticleWR;
    /// <summary>
    /// clear時に生成されるParticleSystem黄、緑
    /// </summary>
    public GameObject clearParticleYG;
    /// <summary>
    /// clear時に生成されるParticleSystem
    /// </summary>
    private bool isGoalParticle = false;

    void Start()
    {
        this.animator = GetComponent<Animator>();
        this.rigid2D = GetComponent<Rigidbody2D>();
        this.unitySE = GetComponents<AudioSource>();
        this.uiController = this.canvas.GetComponent<UIController>();
    }

    void Update()
    {
        //デッドラインを超えた場合ゲームオーバーにする
        if(transform.position.x < deadLine)
        {
            //UIControllerのGameOver関数を呼び出して画面上に「GameOver」と表示する
            GameObject.Find("Canvas").GetComponent<UIController>().GameOver();
            //ユニティちゃんを破棄する
            Destroy(gameObject);
        }
        //走行距離150で終了
        if(this.uiController.length >= 150.0f)
        {
            Clear();
            return;
        }
        //ユニティちゃんが画面右に行くのを禁止
        if(this.transform.position.x > -2.9f)
        {
            this.transform.position = new Vector2(-2.9f,this.transform.position.y);
        }
        //走るアニメーションを再生するために、Animatorのパラメータを調節する
        this.animator.SetFloat("Horizontal", 1);

        //着地しているかどうかを調べる
        this.isGround = (this.transform.position.y > this.groundLevel)?false:true;
        this.animator.SetBool("isGround",isGround);

        //ジャンプ状態のときはボリュームを0にする
        this.unitySE[1].volume = (isGround)?1:0;

        //着地状態でクリックされた場合
        if(isGround && Input.GetMouseButtonDown(0))
        {
            //上方向への力をかける
            this.rigid2D.velocity = new Vector2(0,this.jumpVelocity);
        }
        //クリックをやめたら減速する
        if(Input.GetMouseButton(0) == false)
        {
            if(this.rigid2D.velocity.y > 0)
            {
                this.rigid2D.velocity *= dump;
            }
        }

        //チャージ音を再生する
        if(Input.GetMouseButtonDown(1))
        {
            this.unitySE[0].Play();
        }
        //右クリックを推している間、Charge関数を呼び続ける
        if(Input.GetMouseButton(1))
        {
            Charge();
        }
        //発射
        if (Input.GetMouseButtonUp(1))
        {
            Shot();
        }
        //スライダーの値を変化させる
        this.chargeSlider.value = this.chargeTime;
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

    /// <summary>
    /// 引数として与えた色を下記の変数に代入して、BackGroundとFillの色を変化させる
    /// </summary>
    /// <param name="fillColor">ChargeSlider下FillのColor</param>
    /// <param name="backgroundColor">ChargeSlider下BackGroundのColor</param>
    public void ChangeSliderColor(Color fillColor, Color backgroundColor)
    {
        //Imageクラスの変数ImagesにchargeSliderの子が持つ全てのImageコンポーネントを格納する
        Image[] images = this.chargeSlider.GetComponentsInChildren<Image>();
        //Imageクラスの変数ImageにImagesの要素を順に代入して処理を行う
        foreach (var image in images)
        {
            if (image.name == "Background")
                image.color = backgroundColor;
            else if (image.name == "Fill")
                image.color = fillColor;
        }
    }

    /// <summary>
    /// オブジェクトをx=0まで移動させ、Animatorをidleに移行、SEを停止させParticleSystemを生成する
    /// </summary>
    void Clear()
    {
        if(this.transform.position.x <= 0)
        {
            this.transform.Translate(this.clearWalk,0,0);
        }
        else
        {
            this.animator.SetFloat("Horizontal", 0);
            if(this.isGoalParticle == false)
            {
                Instantiate(this.clearParticleWR,new Vector2(-4.5f,-4.9f),Quaternion.identity);
                Instantiate(this.clearParticleYG,new Vector2(4.5f,-4.9f),Quaternion.identity);
                this.isGoalParticle = true;
            
            }
        }
        this.unitySE[0].Stop();
        this.unitySE[1].Stop();
        this.isGround = (this.transform.position.y > this.groundLevel)?false:true;
        this.animator.SetBool("isGround",isGround);
    }

    /// <summary>
    /// ChargeTime変数で時間を計測し、maxChargeを超えたらchargeLVを変化させる
    /// </summary>
    void Charge()
    {
        this.chargeTime += Time.deltaTime;
        if (chargeTime >= maxCharge)
        {
            switch (this.chargeLV)
            {
                case 0:
                    this.chargeLV = 1;
                    this.chargeTime = 0;
                    break;

                case 1:
                case 2:
                    this.chargeLV = 2;
                    this.chargeTime = maxCharge;
                    break;
            }
        }
    }

    /// <summary>
    /// チャージ中のSEを停止させ、chargeLVによって生成するBombオブジェクトを変化させる
    /// </summary>
    void Shot()
    {
        this.unitySE[0].Stop();
        switch (this.chargeLV)
        {
            case 0:
                Instantiate(bombLv0, new Vector2(transform.position.x + 1.0f, transform.position.y - 0.3f), Quaternion.identity);
                break;

            case 1:
                GameObject BombLv1 = Instantiate(this.bombLv1) as GameObject;
                BombLv1.transform.position = new Vector2(transform.position.x + 1.0f, transform.position.y - 0.3f);
                BombLv1.transform.Rotate(0.0f, 0.0f, 0.0f);
                break;

            case 2:
                GameObject BombLv2 = Instantiate(this.bombLv2) as GameObject;
                BombLv2.transform.position = new Vector2(transform.position.x + 1.0f, transform.position.y - 0.3f);
                BombLv2.transform.Rotate(0.0f, 0.0f, 0.0f);
                break;
        }
        this.chargeTime = 0.0f;
        this.chargeLV = 0;
    }
}