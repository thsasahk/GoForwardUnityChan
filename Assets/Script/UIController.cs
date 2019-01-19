﻿using System.Collections;
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

    //スコアの記録、表示
    public GameObject scoreText;
    public int score=0;
    //ハイスコア
    public string highScore_Key;
    public int highScore=0;

    //cubeScoreの獲得
    public int cubeScore=0;

    void Start()
    {
        //シンビューからオブジェクトを検索する
        this.gameOverText=GameObject.Find("GameOver");
        this.runLengthText=GameObject.Find("RunLength");

        PlayerPrefs.GetInt("highScore_Key",0);
        Debug.Log(this.highScore);
    }

    void Update()
    {
        //scoreを計算
        this.score=this.cubeScore+Mathf.FloorToInt(this.len);
        //scoreを表示
        this.scoreText.GetComponent<Text>().text="Score:"+this.score.ToString()+"pts";

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
        this.gameOverText.GetComponent<Text>().text="Game Over";
        this.isGameOver=true;
        //ハイスコアの更新
        if(this.highScore<this.score)
        {
            this.highScore=this.score;
            PlayerPrefs.SetInt("highScore_Key",this.highScore);
            PlayerPrefs.Save();
        }
    }
}
