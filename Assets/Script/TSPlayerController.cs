using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TSPlayerController : MonoBehaviour
{
    /// <summary>
    /// TutorialSceneManagerオブジェクト
    /// </summary>
    [SerializeField] private GameObject tutorialSceneManager;
    /// <summary>
    /// TutorialSceneManagerオブジェクトのスクリプト
    /// </summary>
    private TutorialSceneManagerController tutorialSceneManagerController;
    /// <summary>
    /// Bombオブジェクト
    /// </summary>
    [SerializeField] private GameObject[] bomb;
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
    public float deadLine = -9;
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
    /// ジェット噴射のParticleSystem
    /// </summary>
    public GameObject jet;
    /// <summary>
    /// ParticleSystemの器
    /// </summary>
    private ParticleSystem jetParticle;
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
    /// <summary>
    /// オブジェクトの接地状態を入力する
    /// </summary>
    private bool isGround;

    void Start()
    {
        this.tutorialSceneManagerController = this.tutorialSceneManager.GetComponent<TutorialSceneManagerController>();
    }

    void Update()
    {
        switch (this.tutorialSceneManagerController.lesson)
        {
            case 1:
                if (Input.GetMouseButtonUp(1) || Input.GetKeyUp(KeyCode.LeftControl) ||
                    Input.GetKeyUp(KeyCode.RightControl))
                {
                    Shot();
                }
                break;

            case 2:
                //チャージ音を再生する
                if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.LeftControl) ||
                    Input.GetKeyDown(KeyCode.RightControl))
                {
                    this.unitySE[0].Play();
                }
                //右クリックを推している間、Charge関数を呼び続ける
                if (Input.GetMouseButton(1) || Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
                {
                    Charge(1);
                }
                if (Input.GetMouseButtonUp(1) || Input.GetKeyUp(KeyCode.LeftControl) ||
                    Input.GetKeyUp(KeyCode.RightControl))
                {
                    Shot();
                }
                break;

            case 3:
                //チャージ音を再生する
                if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.LeftControl) ||
                    Input.GetKeyDown(KeyCode.RightControl))
                {
                    this.unitySE[0].Play();
                }
                //右クリックを推している間、Charge関数を呼び続ける
                if (Input.GetMouseButton(1) || Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
                {
                    Charge(2);
                }
                if (Input.GetMouseButtonUp(1) || Input.GetKeyUp(KeyCode.LeftControl) ||
                    Input.GetKeyUp(KeyCode.RightControl))
                {
                    Shot();
                }
                break;

            case 4:
                //デッドラインを超えた場合ゲームオーバーにする
                if (transform.position.x < deadLine)
                {
                    //UIControllerのGameOver関数を呼び出して画面上に「GameOver」と表示する
                    GameObject.Find("Canvas").GetComponent<UIController>().GameOver();
                    //ユニティちゃんを破棄する
                    Destroy(gameObject);
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

                if ((Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space)) && this.jumpTime < this.jumpLimit)
                {
                    Jump();
                }
                break;
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
                Instantiate(bomb[0], new Vector2(transform.position.x + 1.0f, transform.position.y - 0.3f),
                    Quaternion.identity);
                break;

            case 1:
                GameObject BombLv1 = Instantiate(this.bomb[1]) as GameObject;
                BombLv1.transform.position = new Vector2(transform.position.x + 1.0f, transform.position.y - 0.3f);
                BombLv1.transform.Rotate(0.0f, 0.0f, 0.0f);
                break;

            case 2:
                GameObject BombLv2 = Instantiate(this.bomb[2]) as GameObject;
                BombLv2.transform.position = new Vector2(transform.position.x + 1.0f, transform.position.y - 0.3f);
                BombLv2.transform.Rotate(0.0f, 0.0f, 0.0f);
                break;
        }
        this.chargeTime = 0.0f;
        this.chargeLV = 0;
    }

    /// <summary>
    /// ChargeTime変数で時間を計測し、maxChargeを超えたらchargeLVを変化させる
    /// </summary>
    void Charge(int i)
    {
        this.chargeTime += Time.deltaTime;
        //スライダーの値を変化させる
        this.chargeSlider.value = this.chargeTime;

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
                    this.chargeLV = i;
                    this.chargeTime = maxCharge;
                    break;
            }
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
    /// オブジェクトにy正方向の力を与える
    /// jetParticleを再生する
    /// Jumpしている時間を計測する
    /// maxHigh以上には上昇しない
    /// ピタッと止まると不自然なので少し揺らす
    /// </summary>
    void Jump()
    {
        this.maxHigh = Random.Range(this.maxHigh - 0.02f, this.maxHigh + 0.02f);
        if (this.transform.position.y >= this.maxHigh)
        {
            this.transform.position = new Vector2(this.transform.position.x, this.maxHigh);
            this.rigid2D.velocity = Vector2.zero;
        }
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

        if (this.transform.position.x < -2.9f)
        {
            this.transform.Translate(this.returnSpeed, 0.0f, 0.0f);
        }
    }
}
