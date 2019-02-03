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
    private float groundLevel = -3.8f;
    /// <summary>
    /// ジャンプの速度
    /// </summary>
    public float jumpPower;
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
    /// 右クリックを押してからたった時間
    /// </summary>
    public float chargeTime = 0;
    /// <summary>
    /// chargeLVが一つ上がるまでの時間
    /// </summary>
    public float maxCharge;
    /// <summary>
    /// 溜めた時間によってchargeのレベルを定義し、発射されるBombの種類を管理する
    /// </summary>
    public int chargeLV = 0;
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
    /// <summary>
    /// ジェット噴射のParticleSystem
    /// </summary>
    public GameObject jet;
    /// <summary>
    /// ParticleSystemの器
    /// </summary>
    private ParticleSystem jetParticle;
    /// <summary>
    /// オブジェクトが徐々にスタート位置に戻る速度
    /// </summary>
    public float returnSpeed;
    /// <summary>
    /// オブジェクトの最高高度
    /// </summary>
    public float maxHigh;
    /// <summary>
    /// jumpの継続可能時間
    /// </summary>
    public float jumpLimit;
    /// <summary>
    /// 現在jumpを続けている時間
    /// </summary>
    private float jumpTime = 0;

    void Start()
    {
        this.animator = GetComponent<Animator>();
        this.rigid2D = GetComponent<Rigidbody2D>();
        this.unitySE = GetComponents<AudioSource>();
        this.uiController = this.canvas.GetComponent<UIController>();
        this.jetParticle = this.jet.GetComponent<ParticleSystem>();
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

        if(this.transform.position.x != -2.9f|| this.transform.position.y >= this.maxHigh)
        {
            Return();
        }

        //着地しているかどうかを調べる
        this.isGround = (this.transform.position.y > this.groundLevel) ? false : true;
        if (isGround)
        {
            this.animator.SetBool("Run", true);
            //jumpTimeをリセット
            this.jumpTime = 0;
        }
        else
        {
            this.animator.SetBool("Run", false);
        }

        //ジャンプ状態のときはボリュームを0にする
        this.unitySE[1].volume = (isGround) ? 1 : 0;

        if((Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space)) && this.jumpTime < this.jumpLimit)
        {
            Jump();
        }
        // maxHigh以上には上昇しない
        // ピタッと止まると不自然なので少し揺らす
        this.maxHigh = Random.Range(this.maxHigh - 0.02f, this.maxHigh + 0.02f);
        if (this.transform.position.y >= this.maxHigh)
        {
            this.transform.position = new Vector2(this.transform.position.x, this.maxHigh);
            this.rigid2D.velocity = Vector2.zero;
        }

        //チャージ音を再生する
        if (Input.GetMouseButtonDown(1)|| Input.GetKeyDown(KeyCode.LeftControl)|| Input.GetKeyDown(KeyCode.RightControl))
        {
            this.unitySE[0].Play();
        }
        //右クリックを推している間、Charge関数を呼び続ける
        if(Input.GetMouseButton(1)|| Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            Charge();
        }

        //発射
        if (Input.GetMouseButtonUp(1)|| Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.RightControl))
        {
            Shot();
        }
        //ChargeSliderの色を変化させる
        switch (this.chargeLV)
        {
            case 0:
                ChangeSliderColor(new Color32(95, 255, 187, 255), new Color32(84, 84, 84, 255));
                break;

            case 1:
            case 2:
                ChangeSliderColor(new Color32(59, 255, 72, 255), new Color32(95, 255, 187, 255));
                break;
        }
        //スライダーの値を変化させる
        this.chargeSlider.value = this.chargeTime;
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
            this.isGround = (this.transform.position.y > this.groundLevel) ? false : true;
            if (isGround)
            {
                this.animator.SetBool("Run", true);
            }
        }
        else
        {
            this.animator.SetBool("Run", false);
            if(this.isGoalParticle == false)
            {
                Instantiate(this.clearParticleWR,new Vector2(-4.5f,-4.9f),Quaternion.identity);
                Instantiate(this.clearParticleYG,new Vector2(4.5f,-4.9f),Quaternion.identity);
                this.isGoalParticle = true;
            
            }
        }
        this.unitySE[0].Stop();
        this.unitySE[1].Stop();
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
                Instantiate(bombLv0, new Vector2(transform.position.x + 1.0f, transform.position.y - 0.3f), 
                    Quaternion.identity);
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

    /// <summary>
    /// オブジェクトにy正方向の力を与える
    /// jetParticleを再生する
    /// Jumpしている時間を計測する
    /// </summary>
    void Jump()
    {
        this.rigid2D.AddForce(new Vector2(0f, this.jumpPower * Time.deltaTime));
        this.jetParticle.Play();
        this.jumpTime += Time.deltaTime;
    }

    /// <summary>
    /// オブジェクトが画面右に行くのを禁止
    /// オブジェクトが画面左に押し込まれても、徐々にスタート位置に帰ってくる
    /// </summary>
    void Return()
    {
        if (this.transform.position.x > -2.9f)
        {
            this.transform.position = new Vector2(-2.9f, this.transform.position.y);
        }

        if(this.transform.position.x < -2.9f)
        {
            this.transform.Translate(this.returnSpeed, 0.0f, 0.0f);
        }
    }
}