using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TSBombController : MonoBehaviour
{
    /// <summary>
    /// bombの移動速度
    /// </summary>
    public float bombSpeed;
    /// <summary>
    /// bombがオブジェクトに与えるダメージ量
    /// </summary>
    public int attack;
    /// <summary>
    /// Cubeオブジェクトのスクリプト
    /// </summary>
    private TSCubeController tSCubeController;

    void Start() { }

    // Update is called once per frame
    void Update()
    {
        switch (this.attack)
        {
            case 1:
                transform.Translate(this.bombSpeed, 0, 0);
                break;

            default:
                transform.Translate(0, this.bombSpeed, 0);
                break;

        }
        //画面外に出たらbombオブジェクトを破棄する
        if (transform.position.x >= 10.0f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            //オブジェクトのスクリプトを取得し、Damage関数を呼び出す
            case "Block":
            case "HBlock":
                this.tSCubeController = other.gameObject.GetComponent<TSCubeController>();
                this.tSCubeController.Damage(this.attack);
                if (attack <= 3)
                {
                    Destroy(gameObject);
                }
                break;


            //それ以外のオブジェクトには反応しない
            default:
                break;
        }
    }
}