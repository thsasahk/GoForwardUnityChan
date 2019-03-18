using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TSBombController : MonoBehaviour
{
    /// <summary>
    /// bombの移動速度
    /// </summary>
    public float bombSpeed;//16:9LV0.9LV1.8LV2.7_1024:768LV0.7LV1.6LV2.5
    /// <summary>
    /// bombがオブジェクトに与えるダメージ量
    /// </summary>
    public int attack;
    /// <summary>
    /// Cubeオブジェクトのスクリプト
    /// </summary>
    private TSCubeController tSCubeController;
    /// <summary>
    /// オブジェクトを破棄するx座標
    /// </summary>
    [SerializeField] private float deadLine;//{16:9 10_1028:786 8}

    void Start() { }

    // Update is called once per frame
    void Update()
    {
        switch (this.attack)
        {
            case 1:
                transform.Translate(this.bombSpeed * Time.deltaTime, 0, 0);
                break;

            default:
                transform.Translate(0, this.bombSpeed * Time.deltaTime, 0);
                break;

        }
        //画面外に出たらbombオブジェクトを破棄する
        if (transform.position.x >= this.deadLine)
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

            case "Star":
                other.gameObject.GetComponent<TSStarController>().Damage(this.attack);
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