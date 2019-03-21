using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class StartSceneManager : MonoBehaviour
{
    /// <summary>
    /// ClickToPlayオブジェクト
    /// </summary>
    public GameObject clickToPlay;
    /// <summary>
    /// HighScoreTextオブジェクト
    /// </summary>
    public TextMeshProUGUI highScoreText;
    /// <summary>
    /// ClickToPlayオブジェクトの点滅表示を管理する
    /// </summary>
    private float span = 0;
    /// <summary>
    /// シーンのロード状況を管理する
    /// </summary>
    private bool stopLoad = false;
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
        //ハイスコアの表示
        this.highScoreText.GetComponent<TextMeshProUGUI>().text = "あなたのハイスコア" +
            PlayerPrefs.GetInt("highScore_Key_ver0.71", 0) + "pts";
    }

    void Update()
    {
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
        /*
        if ((Input.GetMouseButton(1)|| Input.GetKeyDown(KeyCode.Space))&&this.stopLoad == false)
        {
            this.stopLoad = true;
            FadeManager.Instance.LoadScene("TutorialScene", 1.0f);
        }
        */
    }

    /*
    private void LateUpdate()
    {
        if ((Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetKeyDown(KeyCode.Space)) 
            && this.stopLoad == false)
        {
            this.stopLoad = true;
            FadeManager.Instance.LoadScene("TutorialScene", 1.0f);
        }
    }
    */

    /// <summary>
    /// TutorialSceneをロードする
    /// イベントトリガーで使用
    /// </summary>
    public void LoadScene()
    {
        //シーンのロード中に重ねて読み込むことはしない
        if (this.stopLoad == false)
        {
            this.stopLoad = true;
            FadeManager.Instance.LoadScene("TutorialScene", 1.0f);
        }
    }

    /// <summary>
    /// イベントトリガーで使用
    /// </summary>
    public void startScroll()
    {
        this.stopLoad = true;
        //Debug.Log("scroll");
    }

    /// <summary>
    /// イベントトリガーで使用
    /// </summary>
    public void endScroll()
    {
        this.stopLoad = false;
    }
}
