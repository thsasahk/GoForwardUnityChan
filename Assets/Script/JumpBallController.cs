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
    private AudioSource jSE;
    /// <summary>
    /// JumpBallが破棄されたときに生成されるParticleSystem
    /// </summary>
    public GameObject particlePrefab;

    void Start()
    {
        this.jSE = GetComponent<AudioSource>();
        this.canvas = GameObject.Find("Canvas");
        this.uiController = this.canvas.GetComponent<UIController>();
    }

    void Update()
    {
        transform.Translate(this.speed * Time.deltaTime, 0, 0);
        //画面外に出たら破棄する
        if(transform.position.x < deadLine)
        {
            Destroy(this.gameObject);
        }
    }

    void OnDestroy()
    {
        //スクロールによる破棄やシーンのロードではParticleSystemを生成しない
        if(transform.position.x > deadLine
        && this.uiController.isGameOver == false)
        {
            Instantiate(this.particlePrefab,transform.position,Quaternion.identity);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            this.jSE.Play();
        }
    }
}
