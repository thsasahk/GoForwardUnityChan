using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialSceneManagerController : MonoBehaviour
{
    /// <summary>
    /// チュートリアルの進行を管理する変数
    /// </summary>
    public int lesson = 1;
    /// <summary>
    /// Playerオブジェクトの有無を表す
    /// </summary>
    public bool isPlayer = true;
    /// <summary>
    /// Playerオブジェクト
    /// </summary>
    [SerializeField] private GameObject player;
    /// <summary>
    /// LoadSceneを実行した際にtrueにする
    /// </summary>
    private bool loadScene = false;
    /// <summary>
    /// HardPrefabオブジェクト
    /// </summary>
    private GameObject hCube;
    /// <summary>
    /// HardPrefabオブジェクトのスクリプト
    /// </summary>
    private TSCubeController tSCubeController;

    void Start()
    {
        
    }

    void Update()
    {
        if ((this.lesson == 5 || Input.GetKeyDown(KeyCode.RightShift)|| Input.GetKeyDown(KeyCode.LeftShift)) 
            && this.loadScene == false)
        {
            LoadScene();
        }

        //Playerオブジェクトが破棄されたとき、新しいPlayerオブジェクトを生成する
        //lesson4Start変数を切り替えてlesson4を仕切り直す
        if (this.isPlayer == false)
        {
            this.isPlayer = true;
            GameObject clone = Instantiate(this.player) as GameObject;
            clone.transform.position = new Vector2(-2.9f, 7.0f);
            clone.transform.Rotate(new Vector2(0.0f, 0.0f));
            this.hCube = GameObject.Find("TutrialSceneHardPrefab(Clone)");
            this.tSCubeController = this.hCube.GetComponent<TSCubeController>();
            this.tSCubeController.lesson4Start = false;
        }
    }

    void LoadScene()
    {
        loadScene = true;
        FadeManager.Instance.LoadScene("GameScene", 1.0f);
    }
}
