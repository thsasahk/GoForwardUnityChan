using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TSCubeGeneratorController : MonoBehaviour
{
    /// <summary>
    /// TutorialSceneManagerオブジェクト
    /// </summary>
    [SerializeField] private GameObject tutorialSceneManager;
    /// <summary>
    /// TutorialSceneManagerオブジェクトのスクリプト
    /// </summary>
    private TutorialSceneManagerController tutorialSceneManagerController;
    /// <summary>
    /// 生成するオブジェクト
    /// </summary>
    [SerializeField] private GameObject[] cube;
    /// <summary>
    /// Cubeの生成を管理する変数
    /// </summary>
    public bool createCube = false;
    /// <summary>
    /// キューブ生成までの間
    /// </summary>
    [SerializeField] private float delay;
    /// <summary>
    /// 次に生成するキューブの種類を決定する変数
    /// </summary>
    private int count = 0;

    void Start()
    {
        this.tutorialSceneManagerController = this.tutorialSceneManager.GetComponent<TutorialSceneManagerController>();
        this.createCube = true;
        CreateCube();
    }

    void Update()
    {
        if (this.tutorialSceneManagerController.loadScene)
        {
            return;
        }
        //delay秒後にcubeを生成する
        if (this.createCube == false)
        {
            this.createCube = true;
            Invoke("CreateCube", this.delay);
        }
    }

    /// <summary>
    /// 通常のBlockとHardBlockを交互に生成する
    /// </summary>
    void CreateCube()
    {
        this.count++;
        if (this.count % 2 == 0)
        {
            int n = Random.Range(0, 4);
            Instantiate(this.cube[n], new Vector2(12.0f, 0.0f), Quaternion.identity);
        }
        else
        {
            Instantiate(this.cube[4], new Vector2(12.0f, 0.0f), Quaternion.identity);
        }
        this.createCube = false;
        /*lesson変数を参照しながら決められたキューブを生成する
        switch (this.tutorialSceneManagerController.lesson)
        {
            case 1:
            case 2:
                int n = Random.Range(0, 4);
                Instantiate(this.cube[n], new Vector2(5.0f, 0.0f), Quaternion.identity);//{16:9 5.0_1028:786 5}
                break;

            case 3:
                int m = Random.Range(0, 4);
                Instantiate(this.cube[4], new Vector2(3.0f, 0.0f), Quaternion.identity);//{16:9 5_1028:786 3}
                Instantiate(this.cube[m], new Vector2(5.0f, 0.0f), Quaternion.identity);//{16:9 7_1028:786 5}
                GameObject lesson3Cube = GameObject.Find("TutrialSceneHardPrefab");
                break;

            default:
                break;
        }
        */
    }
}
