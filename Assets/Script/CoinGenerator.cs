using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    private int span;
    private float waitTime;
    public GameObject coinPrefab;
    public int startSpan1=5;
    public int startSpan2=8;
    public int span1=3;
    public int span2=6;
    public GameObject canvas;
    private UIController uiController;
    void Start()
    {
        this.span=Random.Range(this.startSpan1,this.startSpan2);
        this.uiController = this.canvas.GetComponent<UIController>();
    }

    
    void Update()
    {
        if(this.uiController.len >= 147)
        {
            Destroy(gameObject);
        }
        this.waitTime += Time.deltaTime;

        if(this.waitTime >= this.span)
        {
            float m = Random.Range(-4.5f,1.5f);
            Instantiate(this.coinPrefab,new Vector2(12,m),Quaternion.identity);
            this.span = Random.Range(this.span1,this.span2);
            this.waitTime = 0;
        }
    }
}
