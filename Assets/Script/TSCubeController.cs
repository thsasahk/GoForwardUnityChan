using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TSCubeController : MonoBehaviour
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
    /// CubeGeneratorオブジェクト
    /// </summary>
    [SerializeField] private GameObject cubeGenerator;
    /// <summary>
    /// TutorialSceneManagerオブジェクトのスクリプト
    /// </summary>
    private TSCubeGeneratorController tSCubeGeneratorController;
    /// <summary>
    /// オブジェクトの破棄条件を管理する変数
    /// </summary>
    [SerializeField] private int life = 3;
    /// <summary>
    /// キューブの移動速度
    /// </summary>
    public float speed = -0.2f;
    /// <summary>
    /// 消滅位置
    /// </summary>
    public float deadLine = -10;
    /// <summary>
    /// オブジェクトのAudioSource
    /// </summary>
    private AudioSource[] SE;
    /// <summary>
    /// オブジェクト破棄時に生成されるParticleSystem
    /// </summary>
    public GameObject particlePrefab;


    void Start()
    {
        this.tutorialSceneManager = GameObject.Find("TutorialSceneManager");
        this.cubeGenerator = GameObject.Find("CubeGenerator");
        this.tutorialSceneManagerController = this.tutorialSceneManager.GetComponent<TutorialSceneManagerController>();
        this.tSCubeGeneratorController = this.cubeGenerator.GetComponent<TSCubeGeneratorController>();
    }

    void Update()
    {
        switch (this.tutorialSceneManagerController.lesson)
        {
            case 2:
                this.life = 3;
                break;

            case 4:
                //キューブを移動させる
                transform.Translate(this.speed, 0, 0);
                //画面外に出たら破棄する
                if (transform.position.x < deadLine)
                {
                    Destroy(this.gameObject);
                }
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// オブジェクトのlife変数を変化させオブジェクトの破棄を管理する
    /// </summary>
    /// <param name="i">bombPrefabの攻撃力</param>
    public void Damage(int i)
    {
        //HardBlockオブジェクトの場合はlife変数は一定値
        if (gameObject.tag == "HBlock")
        {
            this.SE[1].Play();
            return;
        }
        //life変数を引数分マイナスさせる
        this.life -= i;
        //life変数が1未満の場合はオブジェクトを破棄する
        if (this.life < 1)
        {
            Instantiate(this.particlePrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        this.tutorialSceneManagerController.lesson++;
        this.tSCubeGeneratorController.createCube = false;
    }
}
