using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public float bombSpeed;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(this.bombSpeed,0,0);
        if(transform.position.x>=10.0f)Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag=="Block"||other.gameObject.tag=="JumpBall")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        if(other.gameObject.tag=="HBlock")
        {
            Destroy(gameObject);
        }
    }
}