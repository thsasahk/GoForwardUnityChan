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
    [SerializeField] private bool isGround;
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
    [SerializeField] private bool isPause = false;
    /// <summary>
    /// ジャンプゲージの回復速度に影響
    /// </summary>
    [SerializeField] private float recovery;
    /// <summary>
    /// StarDash()の間隔
    /// </summary>
    public float coolTime;
    /// <summary>
    /// StarDash()を最後に呼び出してからの時間を計測
    /// </summary>
    [SerializeField] private float time = 0;
    /// <summary>
    /// StarPanelオブジェクト
    /// </summary>
    [SerializeField] private GameObject TSStarPanel;
    /// <summary>
    /// StarPanelControllerオブジェクト
    /// </summary>
    private TSStarPanelController tSStarPanelController;
    /// <summary>
    /// Playerオブジェクトのx方向の移動制限
    /// </summary>
    public float maxPosX;
    /// <summary>
    /// Playerオブジェクトのx方向の移動制限
    /// </summary>
    [SerializeField] private float minPosX;
    /// <summary>
    /// dashParticleオブジェクト
    /// </summary>
    [SerializeField] private ParticleSystem dashParticle;
    /// <summary>
    /// StarDash()による短時間の無敵
    /// </summary>
    public bool isStar = false;
    /// <summary>
    /// 無敵の継続時間
    /// </summary>
    [SerializeField] private float starTime;
    /// <summary>
    /// StarDash()内のiTween.MoveToの継続時間に影響
    /// </summary>
    [SerializeField] private float t1;
    /// <summary>
    /// dashParticleオブジェクトのAudioSource
    /// </summary>
    private AudioSource dashSE;
    /// <summary>
    /// StarShot()を呼び出すためのクリックした際にJump()を呼ぶのを禁止する
    /// </summary>
    //public bool starDash;
    /// <summary>
    /// 連射機対策のショットする際に必要な間隔
    /// </summary>
    [SerializeField] private float interval;
    /// <summary>
    /// 前回ショットしてからの時間を計測
    /// </summary>
    private float shotTime = 0;
    /// <summary>
    /// 極めて規則的な入力をされていないかを確認
    /// </summary>
    private float shotTime2;
    /// <summary>
    /// 接触したオブジェクトに与えるy方向の力
    /// </summary>
    private float verticalPower;
    /// <summary>
    /// verticalPowerの最小値
    /// </summary>
    [SerializeField] private float vPowerMin;
    /// <summary>
    /// verticalPowerの最大値
    /// </summary>
    [SerializeField] private float vPowerMax;
    /// <summary>
    /// 接触したオブジェクトに与えるx方向の力
    /// </summary>
    private float horizontalPower;
    /// <summary>
    /// horizontalPowerの最小値
    /// </summary>
    [SerializeField] private float hPowerMin;
    /// <summary>
    /// horizontalPowerの最大値
    /// </summary>
    [SerializeField] private float hPowerMax;
    /// <summary>
    /// 接触したオブジェクトに与える回転速度
    /// </summary>
    private float rollSpeed;
    /// <summary>
    /// rollSpeedの最小値
    /// </summary>
    [SerializeField] private float rsMin;
    /// <summary>
    /// rollSpeedの最大値
    /// </summary>
    [SerializeField] private float rsMax;
    /// <summary>
    /// 接触したオブジェクトが落下を続ける時間
    /// </summary>
    [SerializeField] private float fallTime;

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
        //this.TSStarPanel = GameObject.FindGameObjectWithTag("StarPanel");
        this.tSStarPanelController = this.TSStarPanel.GetComponent<TSStarPanelController>();
        this.dashSE = this.dashParticle.GetComponent<AudioSource>();
    }

    void Update()
    {
        //TimeLineの再生中、Pose中は操作不能
        if (this.tutorialSceneManagerController.loadScene|| Mathf.Approximately(Time.timeScale, 0f))
        {
            //位置を調整する
            if (this.tutorialSceneManagerController.loadScene && this.isPause == false)
            {
                iTween.MoveTo(gameObject, iTween.Hash("x", -2.9f, "y", -3.9f, "time", 1.0f));
            }
            for (int n = 0; n <= 2; n++)
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
        //最後にStarDash()を呼び出してからの時間を計測
        this.time += Time.deltaTime;
        //最後に射撃してからの時間を計測
        this.shotTime += Time.deltaTime;
        //無敵状態解除
        if (this.time >= this.starTime)
        {
            this.dashParticle.Stop();
            this.isStar = false;
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
        //maxPosX以上の位置への移動は禁止する
        Mathf.Clamp(transform.position.x, this.minPosX, this.maxPosX);
        //最後にStarDash()を呼び出してからの時間を計測
        this.time += Time.deltaTime;
        //無敵状態解除
        if (this.time >= this.starTime)
        {
            this.dashParticle.Stop();
            this.isStar = false;
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
            this.jumpTime -= Time.deltaTime * this.recovery;
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

        /*
        if (this.transform.position.x != -2.9f || this.transform.position.y >= this.maxHigh)
        {
            Return();
        }
        */

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
            Mathf.Approximately(Time.timeScale, 0f)|| this.isStar)
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
        if (Input.GetMouseButtonUp(1) || Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.RightControl))
        {
            Shot();
        }

        if ((Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space)) && this.jumpTime < this.jumpLimit)
        {
            Jump();
        }
        if (!Input.GetMouseButton(0) && !Input.GetKey(KeyCode.Space))
        {
            this.jumpTime -= this.recovery * Time.deltaTime;
            if (this.jumpTime < 0.0f)
            {
                this.jumpTime = 0.0f;
            }
        }
    }

    /// <summary>
    /// チャージ中のSEを停止させ、chargeLVによって生成するBombオブジェクトを変化させる
    /// </summary>
    void Shot()
    {
        if (this.shotTime >= interval && this.shotTime != this.shotTime2)
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
            this.shotTime2 = this.shotTime;
            this.shotTime = 0;
        }
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

    /*
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
    */
    /*
    /// <summary>
    /// オブジェクトが破棄されるときに調整のためにlesson変数を減少させる
    /// </summary>
    private void OnDestroy()
    {
        this.tutorialSceneManagerController.isPlayer = false;
    }
    */
    /// <summary>
    /// Playerオブジェクトをスタート地点に移動させる
    /// スライダーに関する値は0に戻す
    /// </summary>
    void ComeBack()
    {
        this.isComeBack = true;
        this.chargeLV = 0;
        this.chargeTime = 0;
        this.jumpTime = 0;
        this.unitySE[2].Play();
        this.unitySE[1].Stop();
        this.animator.SetBool("Run", false);
        iTween.MoveTo(gameObject, iTween.Hash("y", 1.0f));
        iTween.MoveTo(gameObject, iTween.Hash("x", -2.9f, "y", 1.0f, "time", 3.0f, "delay", 1.0f, "easeType", "linear"));
    }

    /// <summary>
    /// x正方向への力を与える
    /// </summary>
    public void StarDash()
    {
        /*
        Debug.Log("ok2");
        if (this.time < this.coolTime)
        {
            Debug.Log(1);
            return;
        }
        if (this.tSStarPanelController.starCount < 1)
        {
            Debug.Log(2);
            return;
        }
        if (this.isPause)
        {
            Debug.Log(3);
            return;
        }
        if (!isGround)
        {
            Debug.Log(4);
            return;
        }
        if (this.isComeBack)
        {
            Debug.Log(5);
            return;
        }
        */
        if (this.time < this.coolTime || this.tSStarPanelController.starCount < 1 
            || this.isPause || !isGround|| this.isComeBack)
        {
            Debug.Log("no");
            return;
        }
        this.dashParticle.Play();
        this.dashSE.Play();
        this.isStar = true;
        this.tSStarPanelController.starCount--;
        //this.starDash = true;
        //this.isStar = true;
        //this.rigid2D.AddForce(this.dashPower);
        iTween.MoveTo(gameObject, iTween.Hash("x", this.maxPosX, "time", this.starTime - this.t1,
            /*"delay", 0.2f,*/ "easeType", "linear"));
        this.time = 0;
        this.chargeTime = 0.0f;
        this.chargeLV = 0;
        /*
        //残談がない時やクリアシーンに入った時は発射不可
        if (this.starPanelController.starBullet < 1|| this.uiController.clearScene)
        {
            return;
        }
        //残談を減らす
        this.starPanelController.starBullet--;
        this.starShot = true;
        this.starBullePosition = new Vector2(this.transform.position.x + this.x1, this.transform.position.y + this.y1);
        Instantiate(this.starBullet, this.starBullePosition, Quaternion.identity);
        */
    }

    /*
    /// <summary>
    /// Jump()を呼び出すことを許可する
    /// </summary>
    public void StarDashEnd()
    {
        //this.starDash = false;
        this.isStar = false;
    }
    */

    /// <summary>
    /// 接触したオブジェクトに力を与えて吹き飛ばす
    /// </summary>
    /// <param name="other">接触したオブジェクト</param>
    private void OnCollisionStay2D(Collision2D other)
    {
        if (this.isStar)
        {
            switch (other.gameObject.tag)
            {
                case "Block":
                case "HBlock":
                    this.verticalPower = Random.Range(this.vPowerMin, this.vPowerMax);
                    this.horizontalPower = Random.Range(this.hPowerMin, this.hPowerMax);
                    other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(this.horizontalPower * Time.deltaTime,
                        this.verticalPower * Time.deltaTime));
                    this.rollSpeed = Random.Range(this.rsMin, this.rsMax);
                    iTween.RotateTo(other.gameObject, iTween.Hash("z", this.rollSpeed, "time", this.fallTime));
                    other.gameObject.GetComponent<TSCubeController>().speed = 0;
                    other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    break;

                default:
                    break;
            }
        }
    }
}
