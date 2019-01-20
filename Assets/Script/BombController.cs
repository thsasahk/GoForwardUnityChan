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
        switch(this.Lv)
        {
            case 0:
            transform.Translate(this.bombSpeed,0,0);
            break;

            case 1:
            case 2:
            transform.Translate(0,this.bombSpeed,0);
            break;

        }

        if(transform.position.x>=10.0f)Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if((this.Lv==0||this.Lv==1)&&other.gameObject.tag!="Player"&&other.gameObject.tag!="Bomb")Destroy(gameObject);
    }

    void OnDestroy()
    {
        this.Lv=0;
    }
}
