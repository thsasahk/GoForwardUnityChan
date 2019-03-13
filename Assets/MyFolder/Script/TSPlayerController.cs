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
    private Rigidbody2D rigid2D;
    /// <summary>
    /// オブジェクトのAudioSource
    /// </summary>
    private AudioSource[] unitySE;
    /// <summary>
    /// オブジェクトが歩く地面の高さ
    /// </summary>
    private float groundLevel = -3.8f;
    /// <summary>
    /// ジャンプの速度
    /// </summary>
    [SerializeField] private float jumpPower;
    /// <summary>
    /// ゲームオーバーになる位置
    /// </summary>
    [SerializeField] private  float deadLine;//{16:9 -9_1028:786 -7.4}
    /// <summary>
    /// 右クリックを押してからたった時間
    /// </summary>
    private float chargeTime = 0;
    /// <summary>
    /// chargeLVが一つ上がるまでの時間
    /// </summary>
    [SerializeField] private float maxCharge;
    /// <summary>
    /// 溜めた時間によってchargeのレベルを定義し、発射されるBombの種類を管理する
    /// </summary>
    public int chargeLV = 0;
    /// <summary>
    /// ChargeSliderオブジェクト
    /// </summary>
    private GameObject chargeSliderObject;
    /// <summary>
    /// chargeTimeを視覚化するオブジェクト
    /// </summary>
    private Slider chargeSlider;
    /// <summary>
    /// jumpTimeを視覚化するオブジェクト
    /// </summary>
    public Slider hoverSlider;
    /// <summary>
    /// ジェット噴射のParticleSystem
    /// </summary>
    [SerializeField] private GameObject jet;
    /// <summary>
    /// ParticleSystemの器
    /// </summary>
    private ParticleSystem jetParticle;
    /// <summary>
    /// オブジェクトが徐々にスタート位置に戻る速度
    /// </summary>
    [SerializeField] private float returnSpeed;
    /// <summary>
    /// オブジェクトの最高高度
    /// </summary>
    [SerializeField] private float maxHigh;
    /// <summary>
    /// jumpの継続可能時間
    /// </summary>
    [SerializeField] private float jumpLimit;
    /// <summary>
    /// 現在jumpを続けている時間
    /// </summary>
    private float jumpTime = 0;
    /// <summary>
    /// オブジェクトの接地状態を入力する
    /// </summary>
    private bool isGround;
    /// <summary>
    /// ジェットのSEが再生中かどうかを判別
    /// </summary>
    private bool jetSEPlay;
    /// <summary>
    /// playerが定位置へ帰還中であることを表す
    /// </summary>
    public bool isComeBack = false;
    /// <summary>
    /// ポーズ中であることを示す変数
    /// </summary>
    private bool isPause = false;

    void Start()
    {
        this.chargeSliderObject = GameObject.Find("ChargeSlider");
        this.chargeSlider = this.chargeSliderObject.GetComponent<Slider>();
        this.tutorialSceneManager = GameObject.Find("TutorialSceneManager");
        this.tutorialSceneManagerController = this.tutorialSceneManager.GetComponent<TutorialSceneManagerController>();
        this.unitySE = GetComponents<AudioSource>();
        this.animator = GetComponent<Animator>();
        this.rigid2D = GetComponent<Rigidbody2D>();
        this.jetParticle = this.jet.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        //TimeLineの再生中、Pose中は操作不能
        if (this.tutorialSceneManagerController.loadScene|| Mathf.Approximately(Time.timeScale, 0f))
        {
            for(int n = 0; n <= 2; n++)
            {
                this.unitySE[n].Stop();
            }
            this.isPause = true;
            return;
        }
        if (this.isPause)
        {
            this.unitySE[1].Play();
            this.isPause = false;
        }
        //定位置に戻ったら操作を可能にする
        if (transform.position.x >= -2.9f)
        {
            if (this.isComeBack)
            {
                this.unitySE[2].Stop();
                this.unitySE[1].Play();
                this.jetParticle.Stop();
            }
            this.isComeBack = false;
        }
        //画面外に出たら一度操作不能にして、強制的に定位置へと移動する
        if (transform.position.x < deadLine || this.isComeBack)
        {
            if (this.isComeBack == false)
            {
                ComeBack();
            }
            this.jetParticle.Play();
            this.rigid2D.velocity = Vector2.zero;
            return;
        }
        /*
        //チャージ音を再生する
        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.LeftControl) ||
            Input.GetKeyDown(KeyCode.RightControl))
        {
            this.unitySE[0].Play();
        }
        //右クリックを推している間、Charge関数を呼び続ける
        if (Input.GetMouseButton(1) || Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            Charge();
        }
        if (Input.GetMouseButtonUp(1) || Input.GetKeyUp(KeyCode.LeftControl) ||
            Input.GetKeyUp(KeyCode.RightControl))
        {
            Shot();
        }
        */
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
        //着地しているかどうかを調べる
        this.isGround = (this.transform.position.y > this.groundLevel) ? false : true;
        if (isGround)
        {
            this.animator.SetBool("Run", true);
            //jumpTimeをリセット
            //this.jumpTime = 0;
        }
        else
        {
            this.animator.SetBool("Run", false);
        }

        //ジャンプ状態のときはボリュームを0にする
        this.unitySE[1].volume = (isGround) ? 1 : 0;
        /*
        if ((Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space)) && this.jumpTime < this.jumpLimit)
        {
            Jump();
        }
        */
        //
        if (!Input.GetMouseButton(0) && !Input.GetKey(KeyCode.Space))
        {
            this.jumpTime -= Time.deltaTime * 1.5f;
            if (this.jumpTime < 0.0f)
            {
                this.jumpTime = 0.0f;
            }
        }
        //hoverSliderのvalueをjumpTimeに合わせて変更する
        this.hoverSlider.value = this.jumpTime;
        
        // maxHigh以上には上昇しない
        // ピタッと止まると不自然なので少し揺らす
        this.maxHigh = Random.Range(this.maxHigh - 0.02f, this.maxHigh + 0.02f);
        if (this.transform.position.y >= this.maxHigh)
        {
            this.transform.position = new Vector2(this.transform.position.x, this.maxHigh);
            this.rigid2D.velocity = Vector2.zero;
        }
        //ジェットのSEを停止する
        if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Space) || this.jumpTime > this.jumpLimit)
        {
            this.unitySE[2].Stop();
            this.jetSEPlay = false;
        }

        if (this.transform.position.x != -2.9f || this.transform.position.y >= this.maxHigh)
        {
            Return();
        }

        /*lesson変数を参照してPlayerに許可する行動を決定する
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
                    Charge();
                }
                if (Input.GetMouseButtonUp(1) || Input.GetKeyUp(KeyCode.LeftControl) ||
                    Input.GetKeyUp(KeyCode.RightControl))
                {
                    Shot();
                }
                //スライダーの値を変化させる
                this.chargeSlider.value = this.chargeTime;
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
                    Charge();
                }
                if (Input.GetMouseButtonUp(1) || Input.GetKeyUp(KeyCode.LeftControl) ||
                    Input.GetKeyUp(KeyCode.RightControl))
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
                break;

            case 4:
                this.unitySE[1].Play();
                if (transform.position.x < deadLine)
                {
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
                // maxHigh以上には上昇しない
                // ピタッと止まると不自然なので少し揺らす
                this.maxHigh = Random.Range(this.maxHigh - 0.02f, this.maxHigh + 0.02f);
                if (this.transform.position.y >= this.maxHigh)
                {
                    this.transform.position = new Vector2(this.transform.position.x, this.maxHigh);
                    this.rigid2D.velocity = Vector2.zero;
                }

                if (this.transform.position.x != -2.9f || this.transform.position.y >= this.maxHigh)
                {
                    Return();
                }

                //スライダーの値を変化させる
                this.chargeSlider.value = this.chargeTime;
                break;
        }*/

    }

    private void LateUpdate()
    {
        if (this.tutorialSceneManagerController.loadScene || this.isComeBack ||
            Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }
        //チャージ音を再生する
        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.LeftControl) ||
            Input.GetKeyDown(KeyCode.RightControl))
        {
            this.unitySE[0].Play();
        }
        //右クリックを推している間、Charge関数を呼び続ける
        if (Input.GetMouseButton(1) || Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            Charge();
        }
        if (Input.GetMouseButtonUp(1) || Input.GetKeyUp(KeyCode.LeftControl) ||
            Input.GetKeyUp(KeyCode.RightControl))
        {
            Shot();
        }

        if ((Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space)) && this.jumpTime < this.jumpLimit)
        {
            Jump();
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
        /*
        switch (this.tutorialSceneManagerController.lesson)
        {
            case 2:
                this.chargeTime += Time.deltaTime;

                if (chargeTime >= maxCharge)
                {
                    this.chargeLV = 1;
                    this.chargeTime = maxCharge;
                }
                break;

            case 3:
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
                break;
        }
        */
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
    /// </summary>
    void Jump()
    {
        if (this.jetSEPlay == false)
        {
            this.unitySE[2].Play();
            this.jetSEPlay = true;
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
            this.transform.Translate(this.returnSpeed * Time.deltaTime, 0.0f, 0.0f);
        }
    }

    /// <summary>
    /// オブジェクトが破棄されるときに調整のためにlesson変数を減少させる
    /// </summary>
    /*
    private void OnDestroy()
    {
        this.tutorialSceneManagerController.isPlayer = false;
    }*/


    void ComeBack()
    {
        this.isComeBack = true;
        this.unitySE[2].Play();
        this.unitySE[1].Stop();
        this.animator.SetBool("Run", false);
        iTween.MoveTo(gameObject, iTween.Hash("y", 1.0f));
        iTween.MoveTo(gameObject, iTween.Hash("x", -2.9f, "y", 1.0f, "time", 3.0f, "delay", 1.0f, "easeType", "linear"));
    }
}
