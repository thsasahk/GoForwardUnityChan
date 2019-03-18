using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TSStarController : MonoBehaviour
{
    /// <summary>
    /// オブジェクトに円運動させるために進行方向の角度を変化させる
    /// </summary>
    [SerializeField] private float angleSpeed;
    /// <summary>
    /// オブジェクトの現在の進行方向の角度
    /// </summary>
    private float rollAngle;
    /// <summary>
    /// 角度RollAngleへの移動量を決める
    /// </summary>
    [SerializeField] private float rollSpeed;
    /// <summary>
    /// 直線運動のの実行時間
    /// </summary>
    [SerializeField] private float fallTime;
    /// <summary>
    /// 直線運動での目的X座標
    /// </summary>
    [SerializeField] private float distanceX;
    /// <summary>
    /// 直線運動での目的Y座標
    /// </summary>
    private float distanceY;
    /// <summary>
    /// distanceYの最小値
    /// </summary>
    [SerializeField] private float distanceYmin;
    /// <summary>
    /// distanceYの最大値
    /// </summary>
    [SerializeField] private float distanceYmax;
    /// <summary>
    /// オブジェクトの破棄条件を管理する
    /// </summary>
    [SerializeField] private int life;
    /// <summary>
    /// オブジェクト破棄時に生成されるParticleSystem
    /// </summary>
    public GameObject starParticlePrefab;
    /// <summary>
    /// StarPanelオブジェクト
    /// </summary>
    private GameObject starPanel;
    /// <summary>
    /// StarPanelオブジェクトのスクリプト
    /// </summary>
    private TSStarPanelController tSStarPanelController;
    /// <summary>
    /// TutorialSceneManagerオブジェクト
    /// </summary>
    private GameObject tutorialSceneManager;
    /// <summary>
    /// TutorialSceneManagerオブジェクトのスクリプト
    /// </summary>
    private TutorialSceneManagerController tutorialSceneManagerController;

    void Start()
    {
        this.distanceY = Random.Range(this.distanceYmin, this.distanceYmax);
        //オブジェクトを右上から左下方向へ移動させる
        iTween.MoveAdd(this.gameObject,
            iTween.Hash("x", this.distanceX, "y", this.distanceY, "time", this.fallTime, "easeType", "linear"));
        this.starPanel = GameObject.FindGameObjectWithTag("StarPanel");
        this.tSStarPanelController = this.starPanel.GetComponent<TSStarPanelController>();
        this.tutorialSceneManager = GameObject.Find("TutorialSceneManager");
        this.tutorialSceneManagerController = this.tutorialSceneManager.GetComponent<TutorialSceneManagerController>();
    }
    void Update()
    {
        //オブジェクトを円運動させる為に進行方向を変化させる
        this.rollAngle += this.angleSpeed * Time.deltaTime;
        //オブジェクトを円運動させる
        iTween.MoveUpdate(this.gameObject,
            iTween.Hash("x", this.transform.position.x +
            this.rollSpeed * Mathf.Cos(this.rollAngle * Mathf.Deg2Rad) * Time.deltaTime,
                "y", this.transform.position.y +
                this.rollSpeed * Mathf.Sin(this.rollAngle * Mathf.Deg2Rad) * Time.deltaTime,
                    "easeType", "linear"));
        //画面外に出たら破棄
        if (this.transform.position.y <= -5.5f ||
            this.transform.position.x <= -10.0f || this.tutorialSceneManagerController.loadScene)
        {
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// Starオブジェクトのlife変数を変化させ破棄を管理する
    /// 破棄時にstarParticlePrefabを生成する
    /// 破棄時にUIControllerのstarScoreをプラスする
    /// </summary>
    /// <param name="i"></param>
    public void Damage(int i)
    {
        this.life -= i;
        if (this.life < 1)
        {
            /*
            Instantiate(this.starParticlePrefab, new Vector2(this.transform.position.x, this.transform.position.y)
                ,new Vector2(-90.0f,0.0f));
            */
            GameObject obj = Instantiate(this.starParticlePrefab);
            obj.transform.position = new Vector2(this.transform.position.x, this.transform.position.y);
            obj.transform.Rotate(-90.0f, 0.0f, 0.0f);
            if (this.tSStarPanelController.starCount < 3)
            {
                this.tSStarPanelController.starCount++;
            }
            Destroy(gameObject);
        }
    }

    /*
    /// <summary>
    /// Playerと接触した際にCircleCollider2DのTriggerをオフにする
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            GetComponent<CircleCollider2D>().isTrigger = false;
        }
        //if(other.gameObject.tag == "Player")
        //{
        //    this.rb2D = other.gameObject.GetComponent<Rigidbody2D>();
        //    this.rb2D.AddForce(new Vector3(this.power * Mathf.Cos(this.rollAngle * Mathf.Deg2Rad),
        //        this.power * Mathf.Sin(this.rollAngle * Mathf.Deg2Rad), 0));
        //}
    }
    */

    /*
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            GetComponent<CircleCollider2D>().isTrigger = true;
        }
    }
    */
}
