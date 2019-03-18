using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarPanelController : MonoBehaviour
{
    /// <summary>
    /// Playerオブジェクト
    /// </summary>
    [SerializeField] private GameObject player;
    /// <summary>
    /// Playerオブジェクトのスクリプト
    /// </summary>
    private UnityChanController unityChanController;
    /// <summary>
    /// 子オブジェクト
    /// </summary>
    [SerializeField] private Image[] image;
    /// <summary>
    /// StarDash()を行える回数
    /// </summary>
    public int starCount = 0;
    //private int starBullet2 = 0;
    /// <summary>
    /// StarBulletを発射可能であることを示す色
    /// </summary>
    private Color color;
    /// <summary>
    /// StarBullet発射不可を示す色
    /// </summary>
    private Color color2;

    void Start()
    {
        this.unityChanController = this.player.GetComponent<UnityChanController>();
        this.color = new Color(255, 255, 255, 255);
        this.color2 = new Color(0, 0, 0, 255);
    }

    
    void Update()
    {
        //StarBulletの値によって何個目のイメージまで点灯させるかを決定する
        for (int i = 1; i <= this.starCount; i++)
        {
            this.image[i - 1].color = this.color;
        }
        for (int m = this.image.Length; m > this.starCount; m--)
        {
            this.image[m - 1].color = this.color2;
        }
        /*
        if (this.starBullet != this.starBullet2)
        {
            for(int i = 1; i <= this.starBullet; i++)
            {
                this.image[i - 1].color = this.color;
            }
            for(int m = this.image.Length; m > this.starBullet; m--)
            {
                this.image[m - 1].color = this.color2;
            }
        }
        */
        //Zキーで前進する
        if (Input.GetKeyDown(KeyCode.Z))
        {
            this.unityChanController.StarDash();
            //this.unityChanController.isStar = false;
        }
        //this.starBullet2 = this.starBullet;
    }
}
