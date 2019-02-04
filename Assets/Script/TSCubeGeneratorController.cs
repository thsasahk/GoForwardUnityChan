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

    void Start()
    {
        this.tutorialSceneManagerController = this.tutorialSceneManager.GetComponent<TutorialSceneManagerController>();
        this.createCube = true;
        CreateCube();
    }

    void Update()
    {
        //delay秒後にcubeを生成する
        if (this.createCube == false)
        {
            this.createCube = true;
            Invoke("CreateCube", this.delay);
        }
    }

    /// <summary>
    /// lesson変数を参照しながら決められたキューブを生成する
    /// </summary>
    void CreateCube()
    {
        switch (this.tutorialSceneManagerController.lesson)
        {
            case 1:
            case 2:
                Instantiate(this.cube[0], new Vector2(5.0f, 0.0f), Quaternion.identity);
                break;

            case 3:
                Instantiate(this.cube[1], new Vector2(5.0f, 0.0f), Quaternion.identity);
                Instantiate(this.cube[0], new Vector2(7.0f, 0.0f), Quaternion.identity);
                GameObject lesson3Cube = GameObject.Find("TutrialSceneHardPrefab");
                break;

            default:
                break;
        }
    }
}
