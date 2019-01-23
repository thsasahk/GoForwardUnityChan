using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public float bombSpeed;
    //bombの種類
    public int attack;
    //接触したオブジェクトのスクリプトの器
    private CubeController cubeController;
    void Start(){}

    // Update is called once per frame
    void Update()
    {
        switch(this.attack)
        {
            case 1:
            transform.Translate(this.bombSpeed,0,0);
            break;

            default:
            transform.Translate(0,this.bombSpeed,0);
            break;

        }

        if(transform.position.x >= 10.0f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch(other.gameObject.tag)
        {
            case "JumpBall":
            Destroy(other.gameObject);
            if(attack <= 3)
            {
                Destroy(gameObject);
            }
            break;

            case "Block":
            case "HBlock":
            this.cubeController = other.gameObject.GetComponent<CubeController>();
            this.cubeController.Damage(this.attack);
            if(attack <= 3)
            {
                Destroy(gameObject);
            }
            break;

            default:
            break;
        }
    }
}