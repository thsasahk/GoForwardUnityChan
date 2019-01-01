using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    //ゲームオーバーテキスト
    private GameObject gameOverText;

    //走行距離テキスト
    private GameObject runLengthText;

    //走った距離
    public float len=0;

    //走る速度
    public float speed=0.03f;

    //ゲームオーバーの判定
    public bool isGameOver=false;

    void Start()
    {
        //シンビューからオブジェクトを検索する
        this.gameOverText=GameObject.Find("GameOver");
        this.runLengthText=GameObject.Find("RunLength");
    }

    void Update()
    {
        if(this.isGameOver==false)
        {
            //走った距離を計測する
            this.len+=this.speed;

            //走った距離を表示する
            this.runLengthText.GetComponent<Text>().text="Distance: "+len.ToString("F2")+"m";
        }

        //ゲームオーバーになった場合
        if(isGameOver)
        {
            if(Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene("GameScene");
            }
        }
    }

    public void GameOver()
    {
        this.gameOverText.GetComponent<Text>().text="GameOver";
        this.isGameOver=true;
    }
}
