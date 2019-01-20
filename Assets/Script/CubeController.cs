using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    //キューブの移動速度
    private float speed=-0.2f;

    //消滅位置
    private float deadLine=-10;

    //キューブを破壊した際のスコアを記録
    public int cubeScore;

    Rigidbody2D rigid2D;

    private AudioSource[] SE;
    private GameObject canvas;
    private UIController uiController;
    public GameObject particlePrefab;

    private int hitCount=0;
    public int destroyCount;

    void Start()
    {
        this.SE=GetComponents<AudioSource>();
        this.rigid2D=GetComponent<Rigidbody2D>();
        //isGameOverの獲得
        this.canvas=GameObject.Find("Canvas");
        this.uiController=this.canvas.GetComponent<UIController>();
    }

    void Update()
    {
        //キューブを移動させる
        transform.Translate(this.speed,0,0);
        //画面外に出たら破棄する
        if(transform.position.x<deadLine)
        {
            Destroy(this.gameObject);
        }

        //hitCountがdestroyCountになったらキューブを破壊
        if(this.hitCount==this.destroyCount)Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag=="Stage"||other.gameObject.tag=="Block"||other.gameObject.tag=="HBlock")
        {
            this.SE[0].Play();
        }
    }

    void OnDestroy()
    {
        //スクロールによる破棄やシーンのロードではパーティクルを生成しない
        if(transform.position.x>deadLine&&this.uiController.isGameOver==false&&this.gameObject.tag=="Block")
        {
            Instantiate(this.particlePrefab,transform.position,Quaternion.identity);

            //キューブが破壊された際にスコアに加算
            this.uiController.cubeScore+=10;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch(gameObject.tag)
        {
            case "HBlock":
            this.SE[1].Play();
            break;

            case "Block":
            switch(other.gameObject.GetComponent<BombController>().Lv)
            {
                case 0:
                this.hitCount++;
                break;

                case 1:
                this.hitCount=destroyCount;
                break;

                case 2:
                this.hitCount=this.destroyCount;
                break;
            }
            break;
        }
    }
}