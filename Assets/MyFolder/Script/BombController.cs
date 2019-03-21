using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
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
    private CubeController cubeController;
    /// <summary>
    /// Starオブジェクトのスクリプト
    /// </summary>
    private StarController starController;
    /// <summary>
    /// Bossオブジェクトのスクリプト
    /// </summary>
    private BossController bossController;
    /// <summary>
    /// Bulletオブジェクトのスクリプト
    /// </summary>
    private BossBulletController bossBulletController;
    /// <summary>
    /// オブジェクトを破棄するx座標
    /// </summary>
    [SerializeField] private float deadLine;//{16:9 10_1028:786 8}
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private float knockBack;

    void Start(){}

    // Update is called once per frame
    void Update()
    {
        switch(this.attack)
        {
            case 1:
                transform.Translate(this.bombSpeed * Time.deltaTime, 0, 0);
            break;

            default:
                transform.Translate(0, this.bombSpeed * Time.deltaTime, 0);
            break;

        }
        //画面外に出たらbombオブジェクトを破棄する
        if(transform.position.x >= this.deadLine)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //接触したオブジェクトのtagで処理を変化させる
        switch(other.gameObject.tag)
        {
            //attackに関係なくJumpBallを破壊
            case "JumpBall":
                other.GetComponent<JumpBallController>().Damage(this.attack);
                if(attack <= 3)
                {
                    Destroy(gameObject);
                }
                break;

            //オブジェクトのスクリプトを取得し、Damage関数を呼び出す
            case "Block":
            case "HBlock":
                this.cubeController = other.gameObject.GetComponent<CubeController>();
                this.cubeController.Damage(this.attack);
                //other.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right * this.knockBack);
                other.gameObject.GetComponent<CubeController>().time = this.knockBack;
                if (attack <= 3)
                {
                    Destroy(gameObject);
                }
                break;

            case "Star":
                this.starController = other.gameObject.GetComponent<StarController>();
                this.starController.Damage(this.attack);
                if(attack <= 3)
                {
                    Destroy(gameObject);
                }
                break;

            case "Boss":
                this.bossController = other.gameObject.GetComponent<BossController>();
                this.bossController.Damage(this.attack);
                Destroy(gameObject);
                break;

            case "Bullet":
                this.bossBulletController = other.gameObject.GetComponent<BossBulletController>();
                this.bossBulletController.Damage(this.attack);
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