using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGenerator : MonoBehaviour
{
    //キューブのPrefab
    public GameObject cubePrefab;
    //ハードキューブのプレファブ
    public GameObject hardPrefab;
    //ジャンプボールのプレファブ
    public GameObject jumpBall;

    //時間計測用の変数
    private  float delta = 0;

    //キューブの生成間隔
    public float span = 1.0f;

    //キューブの生成位置：X座標
    private float genPosX = 12;

    //キューブの生成位置オフセット
    private float offsetY = 0.3f;
    
    //キューブの縦方向の間隔
    private float spaceY = 6.9f;

    //キューブの生成位置オフセット
    private float offsetX = 0.5f;
    
    //キューブの横方向の間隔
    private float spaceX = 0.4f;

    //キューブの生成個数の上限
    public int maxBlockNum = 4;

    //ハードキューブプレファブが生成される確率
    public int hardPercentage;

    //一度に複数のジャンプボールを作らないように管理する
    private bool ballSwitch = false;
    public GameObject canvas;
    private UIController uiController;
    
    void Start()
    {
        this.uiController = this.canvas.GetComponent<UIController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(this.uiController.len >= 147)
        {
            Destroy(gameObject);
        }
        this.delta += Time.deltaTime;

        //span以上の時間がたったかどうかを調べる
        if(this.delta > this.span)
        {
            this.delta = 0;
            //生成するキューブ数をランダムに決定
            int n = Random.Range(1,maxBlockNum+1);

            //指定したキューブ数だけ生成する
            for(int i = 0;i < n;i ++)
            {
                //生成されるキューブの種類をランダムに決定
                int m = Random.Range(1,this.hardPercentage);
                if(m == hardPercentage - 1)
                {
                    //ハードキューブの生成
                    GameObject go = Instantiate(this.hardPrefab)as GameObject;
                    go.transform.position = new Vector2(this.genPosX,this.offsetY+i*spaceY);
                }else if(m == hardPercentage - 2)
                {
                    if(this.uiController.len > 100 || this.ballSwitch)
                        return;
                    GameObject go = Instantiate(this.jumpBall)as GameObject;
                    go.transform.position = new Vector2(this.genPosX-3,this.offsetY);
                    this.ballSwitch = true;
                }else
                {
                    //キューブの生成
                    GameObject go = Instantiate(cubePrefab)as GameObject;
                    go.transform.position = new Vector2(this.genPosX,this.offsetY+i*spaceY);
                }
            }
            //次のキューブまでの生成時間を決める
            this.span = this.offsetX + this.spaceX*n;
            this.ballSwitch = false;
        }
    }
}
