using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    /// <summary>
    /// キューブの移動速度
    /// </summary>
    public float speed;//{16:9 -7_1028:786 -5.5}
    /// <summary>
    /// 消滅位置
    /// </summary>
    public float deadLine = -10;
    /// <summary>
    /// オブジェクトのAudioSource
    /// </summary>
    private AudioSource[] SE;
    /// <summary>
    /// Canvasオブジェクト
    /// </summary>
    private GameObject canvas;
    /// <summary>
    /// Canvasオブジェクトのスクリプト
    /// </summary>
    private UIController uiController;
    /// <summary>
    /// オブジェクト破棄時に生成されるParticleSystem
    /// </summary>
    public GameObject particlePrefab;
    /// <summary>
    /// オブジェクトの破棄条件を計測する変数
    /// </summary>
    public int life;

    void Start()
    {
        this.SE = GetComponents<AudioSource>();
        this.canvas = GameObject.Find("Canvas");
        this.uiController = this.canvas.GetComponent<UIController>();
    }

    void Update()
    {
        if (this.uiController.isGameOver)
        {
            return;
        }
        //キューブを移動させる
        transform.Translate(this.speed * Time.deltaTime, 0, 0);
        //画面外に出たら破棄する
        if(transform.position.y < deadLine)
        {
            Destroy(this.gameObject);
        }
    }

    //生成され、着地した際にSE[0]を再生する
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Stage"
        ||other.gameObject.tag == "Block"
        ||other.gameObject.tag == "HBlock")
        {
            this.SE[0].Play();
        }
    }
    /// <summary>
    /// Cubeオブジェクトのlife変数を変化させオブジェクトの破棄を管理する
    /// </summary>
    /// <param name="i"></param>
    public void Damage(int i)
    {
        //HardBlockオブジェクトの場合はlife変数は一定値
        if(gameObject.tag == "HBlock")
        {
            this.SE[1].Play();
            return;
        }
        //life変数を引数分マイナスさせる
        this.life -= i;
        //life変数が1未満の場合はオブジェクトを破棄する
        if(this.life < 1)
        {
            Instantiate(this.particlePrefab,transform.position,Quaternion.identity);
            //キューブが破壊された際にスコアに加算する
            this.uiController.cubeScore += 10;
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Stalkerオブジェクト接触した際にAudioSorceを再生する
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Stalker")
        {
            switch (gameObject.tag)
            {
                case "Block":
                    this.SE[1].Play();
                    break;

                case "HBlock":
                    this.SE[2].Play();
                    break;

                default:
                    break;
            }
        }
    }
}