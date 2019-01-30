using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    /// <summary>
    /// ClickToPlayオブジェクト
    /// </summary>
    public GameObject clickToPlay;
    /// <summary>
    /// HighScoreTextオブジェクト
    /// </summary>
    public GameObject highScoreText;
    /// <summary>
    /// ClickToPlayオブジェクトの点滅表示を管理する
    /// </summary>
    private float span = 0;
    /// <summary>
    /// シーンのロード状況を管理する
    /// </summary>
    private bool sceneLoad = false;
    /// <summary>
    /// Playerオブジェクト
    /// </summary>
    public GameObject player;
    /// <summary>
    /// PlayerオブジェクトのAnimator
    /// </summary>
    private Animator animator;
    void Start()
    {
        this.animator = this.player.GetComponent<Animator>();
        this.animator.SetBool("Run", true);
    }

    void Update()
    {
        //ハイスコアの表示
        this.highScoreText.GetComponent<Text>().text = "High Score:" + PlayerPrefs.GetInt("highScore_Key",0) + "pts";

        //clickToPlayを点滅させる
        this.span += Time.deltaTime;
        if(this.span <= 1.0f)
        {
            this.clickToPlay.GetComponent<Text>().text = "Click to Play";
        }
        else if(this.span > 1.0f
        && this.span <= 2.0f)
        {
            this.clickToPlay.GetComponent<Text>().text = " ";
        }
        else if(this.span > 2.0f)
        {
            this.span = 0;
        }

        //シーンのロード中に重ねて読み込むことはしない
        if((Input.GetMouseButton(0)
            ||Input.GetMouseButton(1)
                || Input.GetKeyDown(KeyCode.Space))
                && this.sceneLoad == false)
            {
                LoadScene();
            }
    }
    /// <summary>
    /// GameSceneをロードする
    /// </summary>
    void LoadScene()
    {
        FadeManager.Instance.LoadScene("GameScene", 1.0f);
        this.sceneLoad = true;
    }
}
