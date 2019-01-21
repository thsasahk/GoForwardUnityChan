using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin_Controller : MonoBehaviour
{
    //coinScoreの獲得
    public GameObject canvas;
    private UIController uiController;

    //Coinの移動速度
    public float speed;
    void Start()
    {
        this.uiController=this.canvas.GetComponent<UIController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Coinを移動させる
        this.transform.Translate(this.speed,0,0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag=="Player")
        {
            this.uiController.coinScore+=20;
            Destroy(gameObject);
        }        
    }
}
