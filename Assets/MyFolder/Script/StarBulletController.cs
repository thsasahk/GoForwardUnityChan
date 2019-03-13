using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarBulletController : MonoBehaviour
{
    /// <summary>
    /// オブジェクトの移動速度
    /// </summary>
    [SerializeField] private float speed;
    /// <summary>
    /// オブジェクトの持つ攻撃力
    /// </summary>
    [SerializeField] private int attack;
    /// <summary>
    /// HardBlockと接触した際に与えるy方向の力
    /// </summary>
    private float verticalPower;
    /// <summary>
    /// verticalPowerの最小値
    /// </summary>
    [SerializeField] private float vPowerMin;
    /// <summary>
    /// verticalPowerの最大値
    /// </summary>
    [SerializeField] private float vPowerMax;
    /// <summary>
    /// HardBlockと接触した際に与えるx方向の力
    /// </summary>
    private float horizontalPower;
    /// <summary>
    /// horizontalPowerの最小値
    /// </summary>
    [SerializeField] private float hPowerMin;
    /// <summary>
    /// horizontalPowerの最大値
    /// </summary>
    [SerializeField] private float hPowerMax;
    /// <summary>
    /// HardBlockに与える回転速度
    /// </summary>
    private float rollSpeed;
    /// <summary>
    /// rollSpeedの最小値
    /// </summary>
    [SerializeField] private float rsMin;
    /// <summary>
    /// rollSpeedの最大値
    /// </summary>
    [SerializeField] private float rsMax;
    /// <summary>
    /// HardBlockの落下時間
    /// </summary>
    [SerializeField] private float fallTime;
    /// <summary>
    /// オブジェクトが削除される地点
    /// </summary>
    [SerializeField] private float deadLine;
    /// <summary>
    /// パーティクルオブジェクト
    /// </summary>
    [SerializeField] private GameObject particle;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(this.speed * Time.deltaTime, 0.0f, 0.0f);
        //deadLineを越えたら破棄
        if (this.transform.position.x > this.deadLine)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.gameObject.tag)
        {
            //Damege()を作動させる
            case "Block":
                other.gameObject.GetComponent<CubeController>().Damage(this.attack);
                break;
            //吹き飛ばす
            case "HBlock":
                this.verticalPower = Random.Range(this.vPowerMin, this.vPowerMax);
                this.horizontalPower = Random.Range(this.hPowerMin, this.hPowerMax);
                other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(this.horizontalPower
                    * Time.deltaTime, this.verticalPower * Time.deltaTime));
                this.rollSpeed = Random.Range(this.rsMin, this.rsMax);
                iTween.RotateTo(other.gameObject, iTween.Hash("z", this.rollSpeed, "time", this.fallTime));
                other.gameObject.GetComponent<CubeController>().speed = 0;
                other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                break;
            //Damage()作動させる
            case "JumpBall":
                other.gameObject.GetComponent<JumpBallController>().Damage(this.attack);
                break;
            //Damege()を作動させる
            case "Boss":
                other.gameObject.GetComponent<BossController>().Damage(this.attack);
                GameObject obj = Instantiate(this.particle);
                obj.transform.position = this.transform.position;
                obj.transform.transform.Rotate(-90.0f, 0.0f, 0.0f);
                Destroy(gameObject);
                break;
            //Damege()を作動させる
            case "Bullet":
                other.gameObject.GetComponent<BossBulletController>().Damage(this.attack);
                break;

            default:
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Damege()を作動させる
        if (other.gameObject.tag == "Star")
        {
            other.gameObject.GetComponent<StarController>().Damage(this.attack);
        }
    }
}
