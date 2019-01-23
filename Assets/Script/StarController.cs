using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour
{
    public float angleSpeed;
    private float rollAngle;
    public float rollSpeed;
    public float fallTime;
    public float distanceX;
    public float distanceY;
    public int life;
    public GameObject starParticlePrefab;
    public GameObject canvas;
    private UIController uiController;
    void Start()
    {
        this.distanceY=Random.Range(-10.0f,-25.0f);
        iTween.MoveAdd(this.gameObject,
            iTween.Hash("x", this.distanceX, "y", this.distanceY, "time", this.fallTime, "easeType", "linear"));
        //UIControllerの取得
        this.canvas = GameObject.Find("Canvas");
        this.uiController=this.canvas.GetComponent<UIController>();
    }
    void Update()
    {
        this.rollAngle += this.angleSpeed;
        iTween.MoveUpdate(this.gameObject,
            iTween.Hash("x", this.transform.position.x + this.rollSpeed * Mathf.Cos(this.rollAngle * Mathf.Deg2Rad),
                "y", this.transform.position.y + this.rollSpeed * Mathf.Sin(this.rollAngle * Mathf.Deg2Rad),
                    "easeType", "linear"));
        //画面外に出たら破棄
        if(this.transform.position.y <= -5.5f ||
            this.transform.position.x <= -10.0f)
        {
            Destroy(gameObject);
        }
    }
    public void Damage(int i)
    {
        this.life -= i;
        if(this.life < 1)
        {
            //スクロールによる破棄やシーンのロードではパーティクルを生成しない
            if(transform.position.y > -5.5f
                &&this.uiController.isGameOver == false)
            {
                Instantiate(this.starParticlePrefab,transform.position,Quaternion.identity);
                //キューブが破壊された際にスコアに加算
                this.uiController.cubeScore += 20;
            }
            Destroy(gameObject);
        }
    }
}