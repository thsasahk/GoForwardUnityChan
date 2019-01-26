﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGenerator : MonoBehaviour
{
    /// <summary>
    /// Cubeオブジェクト
    /// </summary>
    public GameObject cubePrefab;
    /// <summary>
    /// HardCubeオブジェクト
    /// </summary>
    public GameObject hardPrefab;
    /// <summary>
    /// JumpBallオブジェクト
    /// </summary>
    public GameObject jumpBall;
    /// <summary>
    /// 時間計測用の変数
    /// </summary>
    private  float delta = 0;
    /// <summary>
    /// Cubeの生成間隔
    /// </summary>
    public float span = 1.0f;
    /// <summary>
    /// Cubeの生成位置：X座標
    /// </summary>
    private float genPosX = 12;
    /// <summary>
    /// Cubeの生成位置オフセット
    /// </summary>
    private float offsetY = 0.3f;
    /// <summary>
    /// Cubeの生成位置：Y座標
    /// </summary>
    private float spaceY = 6.9f;
    /// <summary>
    /// Cubeの生成位置オフセット
    /// </summary>
    private float offsetX = 0.5f;
    /// <summary>
    /// Cubeの横方向の間隔
    /// </summary>
    private float spaceX = 0.4f;
    /// <summary>
    /// Cubeの生成個数の上限
    /// </summary>
    public int maxBlockNum = 4;
    /// <summary>
    /// HardCubeが生成される確率
    /// </summary>
    public int hardPercentage;
    /// <summary>
    /// 一度に複数のJumpBallを作らないように管理する変数
    /// </summary>
    private bool ballSwitch = false;
    /// <summary>
    /// Canvasオブジェクト
    /// </summary>
    public GameObject canvas;
    /// <summary>
    /// Canvasオブジェクトのスクリプト
    /// </summary>
    private UIController uiController;
    
    void Start()
    {
        this.uiController = this.canvas.GetComponent<UIController>();
    }

    void Update()
    {
        //UIControllerの変数lengthが147以上になると破棄する
        if(this.uiController.length >= 147)
        {
            Destroy(gameObject);
        }
        //最後にCubeを生成してからの時間を計測する
        this.delta += Time.deltaTime;
        if(this.delta > this.span)
        {
            CreateCube();
        }
    }
    /// <summary>
    /// Cube,HardCube,JumpBallの生成を管理する
    /// </summary>
    void CreateCube()
    {
        this.delta = 0;
        //生成するキューブ数をランダムに決定
        int n = Random.Range(1, maxBlockNum + 1);
        //指定したキューブ数だけ生成する
        for (int i = 0; i < n; i++)
        {
            //生成されるキューブの種類をランダムに決定
            int m = Random.Range(1, this.hardPercentage);
            if (m == hardPercentage - 1)
            {
                //ハードキューブの生成
                GameObject go = Instantiate(this.hardPrefab) as GameObject;
                go.transform.position = new Vector2(this.genPosX, this.offsetY + i * spaceY);
            }
            else if (m == hardPercentage - 2)
            {
                //lengthが100以下のとき、すでにJumpBallを生成している場合はJumpBallを生成しない
                if (this.uiController.length > 100 || this.ballSwitch)
                    return;
                GameObject go = Instantiate(this.jumpBall) as GameObject;
                go.transform.position = new Vector2(this.genPosX - 3, this.offsetY);
                //JumpBallを生成したことを記録
                this.ballSwitch = true;
            }
            else
            {
                //キューブの生成
                GameObject go = Instantiate(cubePrefab) as GameObject;
                go.transform.position = new Vector2(this.genPosX, this.offsetY + i * spaceY);
            }
        }
        //次のキューブまでの生成時間を決める
        this.span = this.offsetX + this.spaceX * n;
        //JumpBallの生成禁止スイッチを解除
        this.ballSwitch = false;
    }
}
