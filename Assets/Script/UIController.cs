using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    /// <summary>
    /// GameOverTextオブジェクト
    /// </summary>
    private GameObject gameOverText;
    /// <summary>
    /// runLengthTextオブジェクト
    /// </summary>
    private GameObject runLengthText;
    /// <summary>
    /// 走行距離
    /// </summary>
    public float length = 0;
    /// <summary>
    /// Playerが走る速度
    /// </summary>
    public float speed = 0.03f;
    /// <summary>
    /// ゲームオーバーを判定する変数
    /// </summary>
    public bool isGameOver = false;
    /// <summary>
    /// ScoreTextオブジェクト
    /// </summary>
    public GameObject scoreText;
    /// <summary>
    /// ClearSoreオブジェクト
    /// </summary>
    public GameObject clearScore;
    public int score = 0;
    /// <summary>
    /// HighScoreを記録するためのキー
    /// </summary>
    public string highScore_Key;
    /// <summary>
    /// ハイスコアを表す変数
    /// </summary>
    public int highScore;
    /// <summary>
    /// Cubeオブジェクト破壊によって得たScore
    /// </summary>
    public int cubeScore = 0;
    /// <summary>
    /// Coinオブジェクト獲得によって得たScore
    /// </summary>
    public int coinScore = 0;
    /// <summary>
    /// Starオブジェクト破壊によって得たScore
    /// </summary>
    public int starScore = 0;
    /// <summary>
    /// Bossオブジェクト破壊によって得たScore
    /// </summary>
    public int bossScore = 0;
    /// <summary>
    /// unitychanオブジェクト
    /// </summary>
    public GameObject unityChan;
    /// <summary>
    /// ゲームのclear状態を表す変数
    /// </summary>
    public bool clear = false;
    /// <summary>
    /// BGMオブジェクト
    /// </summary>
    public GameObject bgmManager;
    /// <summary>
    /// BGMオブジェクトのAudioSource
    /// </summary>
    private AudioSource[] bgm;
    /// <summary>
    /// BGM変更済みで表すことを示す
    /// </summary>
    private bool changeBGM = false;
    /// <summary>
    /// シーンのロード状況を管理する
    /// </summary>
    private bool sceneLoad = false;
    void Start()
    {
        this.gameOverText = GameObject.Find("GameOver");
        this.runLengthText = GameObject.Find("RunLength");
        this.bgm = this.bgmManager.GetComponents<AudioSource>();
        this.highScore = PlayerPrefs.GetInt("highScore_Key", 0);
    }

    void Update()
    {
        //Gameover状態でなくlengthが150以上のときにunityChanオブジェクトのposition.xを参照する
        if(this.isGameOver == false && length >= 150)
        {
            //unityChanオブジェクトがx=0に移動してGameClearとなる
            if(this.unityChan.transform.position.x >= 0)
            {
                GameClear();
            }
        }
        //scoreを計算
        this.score = this.cubeScore + this.coinScore + this.starScore + this.bossScore + Mathf.FloorToInt(this.length);
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
    /// <summary>
    /// テキストにGame Overを表示し、isGameOverをtrueに変更、BGMも変更する
    /// 獲得スコアがハイスコアより高い場合ハイスコアを変更して記録する
    /// </summary>
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
    /// <summary>
    /// テキストにGame Clearを表示し、clearをtrueに変更、BGMも変更する
    /// 獲得スコアがハイスコアより高い場合ハイスコアを変更して記録する
    /// </summary>
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
