using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
//テキストを取得
public GameObject clickToPlay;
public GameObject highScoreText;

private float span=0;

    void Start()
    {
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

        //GameSceneへの遷移
        if(Input.GetMouseButton(0)
        ||Input.GetMouseButton(1))
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
