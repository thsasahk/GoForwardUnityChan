using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{
    /// <summary>
    /// GameOverTextオブジェクト
    /// </summary>
    private GameObject gameOverText;
    /// <summary>
    /// GameOverTextオブジェクトのTextMeshProUGUI
    /// </summary>
    private TextMeshProUGUI gameOverTextUGUI;
    /// <summary>
    /// gameOverTextUGUIオブジェクトのfontMaterial
    /// </summary>
    private Material gameOverTextMaterial;
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
    /// <summary>
    /// DangerTextオブジェクト
    /// </summary>
    public GameObject DangerText;
    /// <summary>
    /// DangerTextオブジェクトのTextUGUI
    /// </summary>
    public TextMeshProUGUI DangerTextUGUI;
    /// <summary>
    /// DangerTextオブジェクトのAnimator
    /// </summary>
    private Animator DangerTextAnimator;
    /// <summary>
    /// DangerTextオブジェクトのAudioSource;
    /// </summary>
    private AudioSource DangerTextAudioSource;
    /// <summary>
    /// DangerTextのAudioSourceの再生状態を表す
    /// </summary>
    public bool oneplay = false;
    /// <summary>
    /// CubeGeneratorオブジェクト
    /// </summary>
    public GameObject cubeGenerator;
    /// <summary>
    /// CubeGeneratorオブジェクトのスクリプト
    /// </summary>
    private CubeGenerator cubeGeneratorController;
    /// <summary>
    /// DangerTextを表示する時間
    /// </summary>
    public float alertTimeMax;
    /// <summary>
    /// DangerTextが表示されている時間を計測する
    /// </summary>
    private float alertTime;
    /// <summary>
    /// DistanceToBossTextオブジェクト
    /// </summary>
    public GameObject distanceToBossText;
    /// <summary>
    /// 現在位置からボス出現位置までの距離
    /// </summary>
    private float distanceToBoss;


    void Start()
    {
        this.gameOverText = GameObject.Find("GameOver");
        this.gameOverTextUGUI = this.gameOverText.GetComponent<TextMeshProUGUI>();
        this.gameOverTextMaterial = this.gameOverTextUGUI.fontMaterial;
        this.runLengthText = GameObject.Find("RunLength");
        this.bgm = this.bgmManager.GetComponents<AudioSource>();
        this.highScore = PlayerPrefs.GetInt("highScore_Key", 0);
        this.DangerTextUGUI = this.DangerText.GetComponent<TextMeshProUGUI>();
        this.cubeGeneratorController = this.cubeGenerator.GetComponent<CubeGenerator>();
        this.DangerTextAnimator = this.DangerText.GetComponent<Animator>();
        this.DangerTextAudioSource = this.DangerText.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (this.cubeGeneratorController.isBoss)
        {
            DangerTextPlay();
        }
        else
        {
            this.alertTime = 0;
            this.oneplay = false;
        }

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
            && (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Space))
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

        DistanceToBoss();
    }

    /// <summary>
    /// テキストにGame Overを表示し、isGameOverをtrueに変更、BGMも変更する
    /// 獲得スコアがハイスコアより高い場合ハイスコアを変更して記録する
    /// </summary>
    public void GameOver()
    {
        this.gameOverTextUGUI.text = "Game Over";
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
        this.gameOverTextMaterial.SetColor("_OutlineColor", new Color32(255, 255, 0, 255));
        this.gameOverTextUGUI.text = "Game Clear";
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

    /// <summary>
    /// DangerTextにDANGERと表示する
    /// DangerTextのAudioSource、Animatorを再生する
    /// </summary>
    void DangerTextPlay()
    {
        if (this.oneplay == false)
        {
            this.oneplay = true;
            this.DangerTextAudioSource.Play();
        }
        this.DangerTextAnimator.enabled = true;
        this.DangerTextUGUI.text = "DANGER";
        this.alertTime += Time.deltaTime;
        if (alertTime >= alertTimeMax)
        {
            this.DangerTextUGUI.text = " ";
            this.DangerTextAnimator.enabled = false;
        }
    }

    /// <summary>
    /// 現在位置からBoss出現位置までの距離を計算して表示する
    /// Boss出現中はDangerZoneと表示し、以降Boss出現がない時は何も表示しない
    /// </summary>
    void DistanceToBoss()
    {
        if (this.cubeGeneratorController.isBoss)
        {
            this.distanceToBossText.GetComponent<Text>().text = "Danger Zone";
            return;
        }

        if (this.length >= 105)
        {
            this.distanceToBossText.GetComponent<Text>().text = " ";
            return;
        }

        if (this.length < 50)
        {
            this.distanceToBoss = 50 - this.length;
            this.distanceToBossText.GetComponent<Text>().text 
                = "Danger Zone: " + this.distanceToBoss.ToString("F2") + " m";
        }
        else
        {
            this.distanceToBoss = 105 - this.length;
            this.distanceToBossText.GetComponent<Text>().text 
                = "Danger Zone: " + this.distanceToBoss.ToString("F2") + " m";
        }
    }
}