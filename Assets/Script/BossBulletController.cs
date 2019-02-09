using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletController : MonoBehaviour
{
    /// <summary>
    /// キューブの移動速度
    /// </summary>
    public float speed = -0.2f;//16:9.-5_1024:768.-4
    /// <summary>
    /// 消滅位置
    /// </summary>
    public float deadLine = -10;
    /// <summary>
    /// オブジェクトの消滅条件を管理する変数
    /// </summary>
    public int life;
    /// <summary>
    /// 消滅時に生成されるオブジェクト
    /// </summary>
    public GameObject particlePrefab;

    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(this.speed * Time.deltaTime, 0, 0);
        if (transform.position.x < deadLine)
        {
            Destroy(this.gameObject);
        }
    }

    public void Damage(int i)
    {
        //life変数を引数分マイナスさせる
        this.life -= i;
        //life変数が1未満の場合はオブジェクトを破棄する
        if (this.life < 1)
        {
            Instantiate(this.particlePrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
