using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    private int span;
    private float waitTime;
    public GameObject coinPrefab;
    void Start()
    {
        this.span=Random.Range(5,8);
    }

    
    void Update()
    {
        this.waitTime+=Time.deltaTime;

        if(this.waitTime>=this.span)
        {
            float m=Random.Range(-4.5f,1.5f);
            Instantiate(this.coinPrefab,new Vector2(12,m),Quaternion.identity);
            this.span=Random.Range(3,6);
            this.waitTime=0;
        }
    }
}
