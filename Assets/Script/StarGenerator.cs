using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarGenerator : MonoBehaviour
{
    public GameObject star;
    public GameObject canvas;
    private UIController uiController;
    private float span;
    public float coolTime;
    private float time = 0;
    void Start()
    {
        this.uiController = this.canvas.GetComponent<UIController>();
        this.span = Random.Range(this.coolTime,this.coolTime + 5.0f);
    }
    void Update()
    {
        if(this.uiController.len >= 140)
        {
            Destroy(gameObject);
        }
        this.time += Time.deltaTime;
        if(this.uiController.len >= 100 && this.time >= this.span)
        {
            Instantiate(this.star,new Vector2(13,5),Quaternion.identity);
            this.span = Random.Range(this.coolTime,this.coolTime + 5.0f);
            this.time = 0;
        }
    }
}
