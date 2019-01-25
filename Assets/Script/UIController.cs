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
    public float length = 0;

    //走る速度
    public float speed = 0.03f;

    //ゲームオーバーの判定
    public bool isGameOver = false;

    //スコアの記録、表示
    public GameObject scoreText;
    public GameObject clearScore;
    public int score = 0;
    //ハイスコア
    public string highScore_Key;
    public int highScore;

    //CubePrefab破壊時のスコア
    public int cubeScore = 0;
    //CoinPrefab獲得時のスコア
    public int coinScore = 0;
    public int starScore = 0;
    public GameObject unityChan;
    public bool clear = false;
    public GameObject bgmManager;
    private AudioSource[] bgm;
    private bool changeBGM = false;
    private bool sceneLoad = false;
    void Start()
    {
        //シンビューからオブジェクトを検索する
        this.gameOverText = GameObject.Find("GameOver");
        this.runLengthText = GameObject.Find("RunLength");
        this.highScore = PlayerPrefs.GetInt("highScore_Key",0);
        this.bgm = this.bgmManager.GetComponents<AudioSource>();
    }

    void Update()
    {
        if(this.isGameOver == false && length >= 150)
        {
            if(this.unityChan.transform.position.x >= 0)
            {
                GameClear();
            }
        }
        //scoreを計算
        this.score = this.cubeScore + this.coinScore + this.starScore + Mathf.FloorToInt(this.length);
        //scoreを表示
        if(this.clear == false)
        {
            this.scoreText.GetComponent<Text>().text = "Score:" + this.score.ToString() + "pts";
        }
        if(this.isGameOver == false && this.clear == false)
        {
            //走った距離を計測する
            this.length += this.speed;

            //走った距離を表示する
            this.runLengthText.GetComponent<Text>().text = "Distance: " + length.ToString("F2") + "m";
        }

        //ゲームオーバーになった場合
        if((this.isGameOver || this.clear)
            && (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
                && this.sceneLoad == false)
        {
            FadeManager.Instance.LoadScene ("StartScene", 1.0f);
            this.sceneLoad = true;
        }

        //ハイスコアのリセット
        if(Input.GetKey(KeyCode.Space)
        && Input.GetKey(KeyCode.R))
        {
            PlayerPrefs.DeleteKey("highScore_Key");
        }
    }

    public void GameOver()
    {
        this.gameOverText.GetComponent<Text>().text = "Game Over";
        this.isGameOver = true;
        //ハイスコアの更新
        if(this.highScore < this.score)
        {
            this.highScore = this.score;
            PlayerPrefs.SetInt("highScore_Key",this.highScore);
            PlayerPrefs.Save();
        }
        if(this.changeBGM == false)
        {
            this.bgm[0].Stop();
            this.bgm[1].Play();
            this.changeBGM = true;
        }
    }
    void GameClear()
    {
        this.gameOverText.GetComponent<Text>().text = "Game Clear";
        this.clearScore.GetComponent<Text>().text = "Score:" + this.score.ToString() + "pts";
        this.runLengthText.GetComponent<Text>().text = " ";
        this.scoreText.GetComponent<Text>().text = " ";
        this.clear = true;
        //ハイスコアの更新
        if(this.highScore < this.score)
        {
            this.highScore = this.score;
            PlayerPrefs.SetInt("highScore_Key",this.highScore);
            PlayerPrefs.Save();
        }
        if(this.changeBGM == false)
        {
            this.bgm[0].Stop();
            this.bgm[2].Play();
            this.changeBGM = true;
        }
    }
}
