using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    //キューブの移動速度
    private float speed=-0.2f;

    //消滅位置
    private float deadLine=-10;

    Rigidbody2D rigid2D;

    private AudioSource SE;
    void Start()
    {
        this.SE=GetComponent<AudioSource>();
        this.rigid2D=GetComponent<Rigidbody2D>();
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
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag=="Stage"||other.gameObject.tag=="Block")
        {
            this.SE.Play();
        }
    }
}
