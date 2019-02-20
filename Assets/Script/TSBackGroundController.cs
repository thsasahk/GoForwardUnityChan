using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TSBackGroundController : MonoBehaviour
{
    /// <summary>
    ///スクロール速度 
    /// </summary>
    public float scrollSpeed;
    /// <summary>
    /// 背景終了位置
    /// </summary>
    private float deadLine = -18f;
    /// <summary>
    ///背景開始位置
    ///</summary>
    private float startLine = 18f;
    /// <summary>
    /// TutorialSceneManagerオブジェクト
    /// </summary>
    [SerializeField] private GameObject tutorialSceneManager;
    /// <summary>
    /// TutorialSceneManagerオブジェクトのスクリプト
    /// </summary>
    private TutorialSceneManagerController tutorialSceneManagerController;

    void Start()
    {
        this.tutorialSceneManagerController = this.tutorialSceneManager.GetComponent<TutorialSceneManagerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.tutorialSceneManagerController.loadScene)
        {
            return;
        }
        //背景を移動する
        transform.Translate(this.scrollSpeed * Time.deltaTime, 0, 0);
        //画面外に出たら画面右端に移動する
        if (this.transform.position.x < this.deadLine)
        {
            this.transform.position = new Vector2(this.startLine, 0);
        }
        /*
        if (this.tutorialSceneManagerController.loadScene)
        {
            return;
        }

        if (this.tutorialSceneManagerController.lesson == 4)
        {
            //背景を移動する
            transform.Translate(this.scrollSpeed * Time.deltaTime, 0, 0);
            //画面外に出たら画面右端に移動する
            if (this.transform.position.x < this.deadLine)
            {
                this.transform.position = new Vector2(this.startLine, 0);
            }
        
        }*/
    }
}
