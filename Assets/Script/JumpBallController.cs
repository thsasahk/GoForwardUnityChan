using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBallController : MonoBehaviour
{
    public float speed;
    //消滅位置
    private float deadLine=-10;
    private GameObject canvas;
    private UIController uiController;
    private AudioSource jSE;
    public GameObject particlePrefab;

    void Start()
    {
        this.jSE=GetComponent<AudioSource>();
        //isGameOverの獲得
        this.canvas=GameObject.Find("Canvas");
        this.uiController=this.canvas.GetComponent<UIController>();
    }

    void Update()
    {
        transform.Translate(this.speed,0,0);
        //画面外に出たら破棄する
        if(transform.position.x<deadLine)
        {
            Destroy(this.gameObject);
        }
    }

    void OnDestroy()
    {
        //スクロールによる破棄やシーンのロードではSEは鳴らない
        if(transform.position.x>deadLine&&this.uiController.isGameOver==false)
        {
            Instantiate(this.particlePrefab,transform.position,Quaternion.identity);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag=="Player")this.jSE.Play();
    }
}
