using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public float bombSpeed;
    //bombの種類
    public int Lv=0;
    void Start(){}

    // Update is called once per frame
    void Update()
    {
        if(this.Lv==1)
        {
            transform.Translate(this.bombSpeed,0,0);
        }else if(this.Lv==2)
        {
            transform.Translate(0,this.bombSpeed,0);
        }
        if(transform.position.x>=10.0f)Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag=="Block"||other.gameObject.tag=="JumpBall")
        {
            Destroy(other.gameObject);
            if(this.Lv==1)Destroy(gameObject);
        }
        if(other.gameObject.tag=="HBlock")
        {
            if(this.Lv==1)Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        this.Lv=0;
    }
}