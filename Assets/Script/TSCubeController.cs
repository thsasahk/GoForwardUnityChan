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
    public float speed;//{16:9 -7_1028:786 -5.5}
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
    /// <summary>
    /// ManualTextオブジェクト
    /// </summary>
    [SerializeField] private GameObject manualText;
    /// <summary>
    /// ManualTextオブジェクトのスクリプト
    /// </summary>
    private TSUIController tSUIController;
    /// <summary>
    /// lesson4の開始を記録する
    /// </summary>
    public bool lesson4Start;
    /// <summary>
    /// playerオブジェクト
    /// </summary>
    [SerializeField] private GameObject player;

    [SerializeField] private float delay;

    private float count;

    void Start()
    {
        this.tutorialSceneManager = GameObject.Find("TutorialSceneManager");
        this.cubeGenerator = GameObject.Find("CubeGenerator");
        this.tutorialSceneManagerController = this.tutorialSceneManager.GetComponent<TutorialSceneManagerController>();
        this.tSCubeGeneratorController = this.cubeGenerator.GetComponent<TSCubeGeneratorController>();
        this.manualText = GameObject.Find("ManualText");
        this.tSUIController = this.manualText.GetComponent<TSUIController>();
        this.SE = GetComponents<AudioSource>();
    }

    void Update()
    {
        //lesson変数によってキューブの性質を変化させる
        switch (this.tutorialSceneManagerController.lesson)
        {
            case 2:
                this.life = 3;
                break;

            case 4:
                if (this.lesson4Start == false)
                {
                    transform.position = new Vector2(12.0f, 0.0f);
                    this.lesson4Start = true;
                }

                this.count += Time.deltaTime;

                if (this.delay <= this.count)
                {
                    //キューブを移動させる
                    transform.Translate(this.speed * Time.deltaTime, 0, 0);
                }
                
                
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

    /// <summary>
    /// オブジェクトが破棄されるときにlesson変数を上昇させる
    /// createCubeをfalseにしてCubeの生成を許可する
    /// TextMeshProのアニメを再生する
    /// </summary>
    private void OnDestroy()
    {
        if (this.tutorialSceneManagerController.loadScene)
        {
            return;
        }
        this.tutorialSceneManagerController.lesson++;
        this.tSUIController.TextMoveBotom();
        this.tSCubeGeneratorController.createCube = false;
    }

    //生成され、着地した際にSE[0]を再生する
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Stage"
        || other.gameObject.tag == "Block"
        || other.gameObject.tag == "HBlock")
        {
            this.SE[0].Play();
        }
    }
}
