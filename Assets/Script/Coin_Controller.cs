using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin_Controller : MonoBehaviour
{
    //coinScoreの獲得
    private GameObject canvas;
    private UIController uiController;

    public GameObject coinSound;

    //Coinの移動速度
    public float speed;

    private int deadLine=-10;

    void Start()
    {
        this.canvas=GameObject.Find("Canvas");
        this.uiController=this.canvas.GetComponent<UIController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Coinを移動させる
        this.transform.Translate(this.speed,0,0);

        //deadLineを超えたら破棄
        if(transform.position.x<this.deadLine)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag=="Player")
        {
            Instantiate(this.coinSound,new Vector2(0,0),Quaternion.identity);
            this.uiController.coinScore+=20;
            Destroy(gameObject);
        }        
    }
}
