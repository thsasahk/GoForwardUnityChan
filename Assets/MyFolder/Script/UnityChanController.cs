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
    private Rigidbody2D rigid2D;
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
    public float deadLine;//{16:9 -9_1028:786 -7.4}
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
    /// jumpTimeを視覚化するオブジェクト
    /// </summary>
    public Slider hoverSlider;
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
    //public float returnSpeed;
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
    /// Clear関数を繰り返し呼び出さないようにする
    /// </summary>
    private bool oneTime = false;
    /// <summary>
    /// Stalkerオブジェクト
    /// </summary>
    [SerializeField] private GameObject stalker;
    /// <summary>
    /// Playerオブジェクトが目標にする立ち位置
    /// </summary>
    //private float posX;
    /// <summary>
    /// RunAnimeとSEの終了させる変数
    /// </summary>
    private bool runEND = false;
    /// <summary>
    /// ジェットのSEが再生中かどうかを判別
    /// </summary>
    private bool jetSEPlay;
    /// <summary>
    /// ポーズ中であることを示す変数
    /// </summary>
    private bool isPause = false;
    /// <summary>
    /// StarBulletオブジェクト
    /// </summary>
    [SerializeField] private GameObject starBullet;
    /// <summary>
    /// StarBulletオブジェクトを生成する座標
    /// </summary>
    private Vector2 starBullePosition;
    [SerializeField] private float x1;
    [SerializeField] private float y1;
    /// <summary>
    /// StarShot()を呼び出すためのクリックした際にJump()を呼ぶのを禁止する
    /// </summary>
    public bool starDash;
    /// <summary>
    /// StarPanelオブジェクト
    /// </summary>
    [SerializeField] private GameObject starPanel;
    /// <summary>
    /// StarPanelオブジェクトのスクリプト
    /// </summary>
    private StarPanelController starPanelController;
    /// <summary>
    /// StarDash()の間隔
    /// </summary>
    public float coolTime;
    /// <summary>
    /// StarDash()を最後に呼び出してからの時間を計測
    /// </summary>
    private float time = 0;
    /// <summary>
    /// StarDash()でオブジェクトに与える力
    /// </summary>
    private Vector2 dashPower;
    /// <summary>
    /// dashPowerのx値
    /// </summary>
    public float dashPowerX;
    /// <summary>
    /// Playerオブジェクトのx方向の限界
    /// </summary>
    public float maxPosX;
    /// <summary>
    /// dashParticleオブジェクト
    /// </summary>
    [SerializeField]private ParticleSystem dashParticle;
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
    /// <summary>
    /// StarDash()による短時間の無敵
    /// </summary>
    private bool isStar = false;
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

    void Start()
    {
        this.animator = GetComponent<Animator>();
        this.rigid2D = GetComponent<Rigidbody2D>();
        this.unitySE = GetComponents<AudioSource>();
        this.uiController = this.canvas.GetComponent<UIController>();
        this.jetParticle = this.jet.GetComponent<ParticleSystem>();
        this.starPanelController = this.starPanel.GetComponent<StarPanelController>();
        this.dashPower = new Vector2(this.dashPowerX * Time.deltaTime, 0f);
        this.dashSE = this.dashParticle.GetComponent<AudioSource>();
    }

    void Update()
    {
        //Pause中はキー入力を受け付けない
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            for (int n = 0; n <= 2; n++)
            {
                this.unitySE[n].Stop();
            }
            this.dashSE.Stop();
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
        //無敵状態解除
        if(this.time >= this.starTime)
        {
            this.dashParticle.Stop();
            this.isStar = false;
        }
        //maxPosX以上の位置への移動は禁止する
        if (this.transform.position.x >= this.maxPosX)
        {
            this.transform.position = new Vector2(this.maxPosX, this.transform.position.y);
        }
        //着地しているかどうかを調べる
        this.isGround = (this.transform.position.y > this.groundLevel) ? false : true;
        /*
        if (transform.localEulerAngles.y <= 0.0f)
        {
            isGround = false;
        }*/
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

        if (this.uiController.clear)
        {
            transform.position = Vector2.zero;
            transform.Rotate(Vector2.zero);
        }
        //ClearSceneに入ったら操作不能
        if (this.uiController.clearScene)
        {
            Invoke("Clear", 1.0f);
            this.unitySE[0].Stop();
            this.unitySE[2].Stop();
            return;
        }
        /*走行距離150で終了
        if (this.uiController.length >= 150.0f)
        {
            Clear();
            return;
        }
        */

        /*
        //オブジェクトの定位置をstalkerオブジェクトの位置を参考にしながら決定
        //ずれている場合はそのポイントへ徐々に戻る
        this.posX = this.stalker.transform.position.x + 6.7f;
        if(this.transform.position.x != this.posX|| this.transform.position.y >= this.maxHigh)
        {
            Return();
        }
        */
        if((Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space)) && this.jumpTime < this.jumpLimit
            && !this.starDash)
        {
            Jump();
        }
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
        //SEを停止させる
        if(Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Space) || this.jumpTime > this.jumpLimit)
        {
            this.unitySE[2].Stop();
            this.jetSEPlay = false;
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
        if (this.oneTime)
        {
            return;
        }

        this.oneTime = true;
        /*
        if (this.oneTime)
        {
            return;
        }
        this.unitySE[0].Stop();
        iTween.MoveTo(gameObject, iTween.Hash("x", 9.0f, "y", -3.9f, "time", 1.0f, "delay",1.0f,"easeType", "linear"));
        iTween.RotateTo(gameObject, iTween.Hash("y", 0.0f, "delay",2.0f));
        iTween.MoveTo(gameObject, iTween.Hash("x", 7.0f, "time", 2.0f, "delay", 3.0f,"easeType","linear"));
        iTween.MoveTo(gameObject, iTween.Hash("x", 0.0f, "time", 4.0f, "delay", 8.0f, "easeType", "linear"));
        Invoke("GameClearCall", 12.4f);
        this.oneTime = true;
        */
        /*
        if(this.transform.position.x <= 0)
        {
            this.transform.Translate(this.clearWalk * Time.deltaTime, 0, 0);
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
        }*/
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
    /// Stalkerオブジェクトと一定の距離を置ける位置に徐々に移動していく
    /// </summary>
    void Return()
    {
        transform.Translate((this.posX - transform.position.x) * this.returnSpeed * Time.deltaTime, 0.0f, 0.0f);
    }
    */

    void OnDestroy()
    {
        //UIControllerのGameOver関数を呼び出して画面上に「GameOver」と表示する
        if (this.canvas == null)
        {
            return;
        }
        else
        {
            this.uiController.GameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Canon")
        {
            this.unitySE[1].Stop();
            this.animator.SetBool("Run", false);
        }
    }

    /*
    void GameClearCall()
    {
        this.uiController.GameClear();
    }*/

    /// <summary>
    /// x正方向への力を与える
    /// </summary>
    public void StarDash()
    {
        if (this.time < this.coolTime || this.starPanelController.starCount < 1 || 
            this.uiController.clearScene || this.isPause)
        {
            return;
        }
        this.dashParticle.Play();
        this.dashSE.Play();
        this.isStar = true;
        this.starPanelController.starCount--;
        this.starDash = true;
        //this.rigid2D.AddForce(this.dashPower);
        iTween.MoveTo(gameObject, iTween.Hash("x", this.maxPosX, "time", this.starTime - this.t1, 
            /*"delay", 0.2f,*/ "easeType", "linear"));
        this.time = 0;
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

    /// <summary>
    /// Jump()を呼び出すことを許可する
    /// </summary>
    public void StarDashEnd()
    {
        this.starDash = false;
    }

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
                    other.gameObject.GetComponent<CubeController>().speed = 0;
                    other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    break;

                case "JumpBall":
                    this.verticalPower = Random.Range(this.vPowerMin, this.vPowerMax);
                    this.horizontalPower = Random.Range(this.hPowerMin, this.hPowerMax);
                    other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(this.horizontalPower * Time.deltaTime,
                        this.verticalPower * Time.deltaTime));
                    this.rollSpeed = Random.Range(this.rsMin, this.rsMax);
                    iTween.RotateTo(other.gameObject, iTween.Hash("z", this.rollSpeed, "time", this.fallTime));
                    other.gameObject.GetComponent<JumpBallController>().speed = 0;
                    other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    other.gameObject.GetComponent<CircleCollider2D>().enabled = false;
                    break;

                case "Bullet":
                    this.verticalPower = Random.Range(this.vPowerMin, this.vPowerMax);
                    this.horizontalPower = Random.Range(this.hPowerMin, this.hPowerMax);
                    other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(this.horizontalPower * Time.deltaTime,
                        this.verticalPower * Time.deltaTime));
                    other.gameObject.GetComponent<CircleCollider2D>().enabled = false;
                    other.gameObject.GetComponent<BossBulletController>().speed = 0;
                    break;

                default:
                    break;
            }
        }
    }
}