using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBallController : MonoBehaviour
{
    /// <summary>
    /// JumpBallの移動速度
    /// </summary>
    public float speed;
    /// <summary>
    /// 消滅位置
    /// </summary>
    public float deadLine = -10;
    /// <summary>
    /// Canvasオブジェクト
    /// </summary>
    private GameObject canvas;
    /// <summary>
    /// Canvasオブジェクトのスクリプト
    /// </summary>
    private UIController uiController;
    /// <summary>
    /// JumpBallのAudio Source
    /// </summary>
    private AudioSource[] jSE;
    /// <summary>
    /// JumpBallが破棄されたときに生成されるParticleSystem
    /// </summary>
    public GameObject particlePrefab;
    /// <summary>
    /// オブジェクトの体力
    /// </summary>
    public int life;
    /// <summary>
    /// オブジェクトのRigidbody2D
    /// </summary>
    private Rigidbody2D rb2D;
    /// <summary>
    /// オブジェクトのジャンプ力
    /// </summary>
    [SerializeField] private float power;
    /// <summary>
    /// AddForceの引数
    /// </summary>
    private Vector2 powerVector;

    void Start()
    {
        this.jSE = GetComponents<AudioSource>();
        this.canvas = GameObject.Find("Canvas");
        this.uiController = this.canvas.GetComponent<UIController>();
        this.rb2D = GetComponent<Rigidbody2D>();
        this.powerVector = new Vector2(0.0f, this.power);
    }

    void Update()
    {
        if (this.uiController.isGameOver)
        {
            return;
        }
        transform.Translate(this.speed * Time.deltaTime, 0, 0);//{16:9 -7_1028:786 -5.5}B{16:9 -6_1028:786 -5}
        //画面外に出たら破棄する
        if (transform.position.y < deadLine)
        {
            Destroy(this.gameObject);
        }
    }

    /*
    void OnDestroy()
    {
        //スクロールによる破棄やシーンのロードではParticleSystemを生成しない
        if(transform.position.y > deadLine
        && this.uiController.isGameOver == false)
        {
            this.uiController.score += 20;
            Instantiate(this.particlePrefab,transform.position,Quaternion.identity);
        }
    }
    */

    void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Block":
            case "HBlock":
                this.rb2D.AddForce(this.powerVector);
                break;

            case "Player":
                if (other.gameObject.GetComponent<UnityChanController>().isStar)
                {
                    this.jSE[1].Play();
                    break;
                }

                this.jSE[0].Play();
                break;

            case "Stage":
                this.rb2D.AddForce(this.powerVector);
                break;

            default:  
                break;
        }
        /*
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<UnityChanController>().isStar)
            {
                this.jSE[1].Play();
                return;
            }
            this.jSE[0].Play();
        }
        */
    }

    /// <summary>
    /// Stalkerオブジェクト接触した際にAudioSorceを再生する
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Stalker")
        {
            this.jSE[1].Play();
        }
    }

    public void Damage(int i)
    {
        this.life -= i;
        if (this.life < 1)
        {
            this.uiController.score += 20;
            Instantiate(this.particlePrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
