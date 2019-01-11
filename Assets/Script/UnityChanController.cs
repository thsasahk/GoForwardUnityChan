using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityChanController : MonoBehaviour
{
    //アニメーションするためのコンポーネントを入れる
    Animator animator;

    //ユニティちゃんを移動させるコンポーネントを入れる
    Rigidbody2D rigid2D;

    //
    public AudioSource[] audio;

    //地面の位置
    private float groundLevel=-3.0f;

    //ジャンプ速度の減衰
    private float dump=0.8f;

    //ジャンプの速度
    float jumpVelocity=20;

    //ゲームオーバーになる位置
    private float deadLine=-9;

    //球
    public GameObject bomb;
    void Start()
    {
        //アニメーターのコンポーネントを取得
        this.animator=GetComponent<Animator>();
        //Rigidbody2Dのコンポーネントを取得
        this.rigid2D=GetComponent<Rigidbody2D>();
        //
        this.audio=GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //走るアニメーションを再生するために、Animatorのパラメータを調節する
        this.animator.SetFloat("Horizontal", 1);

        //着地しているかどうかを調べる
        bool isGround=(this.transform.position.y>this.groundLevel)?false:true;
        this.animator.SetBool("isGround",isGround);

        //ジャンプ状態のときはボリュームを0にする
        this.audio[0].volume=(isGround)?1:0;

        //着地状態でクリックされた場合
        if(isGround&&Input.GetMouseButtonDown(0))
        {
            //上方向への力をかける
            this.rigid2D.velocity=new Vector2(0,this.jumpVelocity);
        }
        //クリックをやめたら減速する
        if(Input.GetMouseButton(0)==false)
        {
            if(this.rigid2D.velocity.y>0)
            {
                this.rigid2D.velocity*=dump;
            }
        }

        //発射する
        if(Input.GetMouseButtonDown(1))
        {
            Instantiate(bomb,new Vector2(transform.position.x+1.0f,transform.position.y-0.3f),Quaternion.identity);
        }

        //デッドラインを超えた場合ゲームオーバーにする
        if(transform.position.x<deadLine)
        {
            //UIControllerのGameOver関数を呼び出して画面上に「GameOver」と表示する
            GameObject.Find("Canvas").GetComponent<UIController>().GameOver();

            //ユニティちゃんを破棄する
            Destroy(gameObject);
        }
    }
}
