using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanController : MonoBehaviour
{
    //アニメーションするためのコンポーネントを入れる
    Animator animator;

    //ユニティちゃんを移動させるコンポーネントを入れる
    Rigidbody2D rigid2D;

    //地面の位置
    private float groundLevel=-3.0f;

    //ジャンプ速度の減衰
    private float dump=0.8f;

    //ジャンプの速度
    float jumpVelocity=20;

    //ゲームオーバーになる位置
    private float deadLine=-9;

    void Start()
    {
        //アニメーターのコンポーネントを取得
        this.animator=GetComponent<Animator>();
        //Rigidbody2Dのコンポーネントを取得
        this.rigid2D=GetComponent<Rigidbody2D>();
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
        GetComponent<AudioSource>().volume=(isGround)?1:0;

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
