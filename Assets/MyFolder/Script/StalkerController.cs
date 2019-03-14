using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StalkerController : MonoBehaviour
{
    /// <summary>
    /// オブジェクトのX座標
    /// </summary>
    private float posX;
    /// <summary>
    /// posXのとりうる最大値
    /// </summary>
    [SerializeField]private float posMax;
    /// <summary>
    /// posXのとりうる最小値
    /// </summary>
    [SerializeField]private float posMin;
    /// <summary>
    /// オブジェクトの速度
    /// </summary>
    [SerializeField]private float speed;
    /// <summary>
    /// 最後にposXを変更してからの経過時間
    /// </summary>
    private float count;
    /// <summary>
    /// posXを変更する間隔
    /// </summary>
    [SerializeField] private float span;
    /// <summary>
    /// オブジェクトのAnimator
    /// </summary>
    private Animator animator;
    /// <summary>
    /// canvasオブジェクト
    /// </summary>
    private GameObject canvas;
    /// <summary>
    /// canvasオブジェクトのスクリプト
    /// </summary>
    private UIController uIController;
    /// <summary>
    /// オブジェクトのAudioSource
    /// </summary>
    private AudioSource[] se;
    /// <summary>
    /// 接触したオブジェクトに与えるy方向の力
    /// </summary>
    private float verticalPower;
    /// <summary>
    /// verticalPowerの最小値
    /// </summary>
    [SerializeField] private float vPowerMin;
    /// <summary>
    /// verticalPowerの最大値
    /// </summary>
    [SerializeField] private float vPowerMax;
    /// <summary>
    /// 接触したオブジェクトに与えるx方向の力
    /// </summary>
    private float horizontalPower;
    /// <summary>
    /// horizontalPowerの最小値
    /// </summary>
    [SerializeField] private float hPowerMin;
    /// <summary>
    /// horizontalPowerの最大値
    /// </summary>
    [SerializeField] private float hPowerMax;
    /// <summary>
    /// 接触したオブジェクトに与える回転速度
    /// </summary>
    private float rollSpeed;
    /// <summary>
    /// rollSpeedの最小値
    /// </summary>
    [SerializeField] private float rsMin;
    /// <summary>
    /// rollSpeedの最大値
    /// </summary>
    [SerializeField] private float rsMax;
    /// <summary>
    /// 接触したオブジェクトが落下を続ける時間
    /// </summary>
    [SerializeField] private float fallTime;
    /// <summary>
    /// GameOverTimeLineオブジェクト
    /// </summary>
    [SerializeField] private GameObject gameOverTimeLine;
    /// <summary>
    /// GameOverTimeLineのPlayableDirector
    /// </summary>
    private PlayableDirector gameOverTimeLineDirector;
    /// <summary>
    /// CubeGeneratorオブジェクトのスクリプト
    /// </summary>
    [SerializeField] private GameObject cubeGeneratorObject;
    /// <summary>
    /// CubeGeneratorオブジェクト
    /// </summary>
    private CubeGenerator cubeGenerator;

    void Start()
    {
        this.posX = this.posMin;
        this.animator = GetComponent<Animator>();
        this.canvas = GameObject.Find("Canvas");
        this.uIController = this.canvas.GetComponent<UIController>();
        this.se = GetComponents<AudioSource>();
        this.gameOverTimeLineDirector = this.gameOverTimeLine.GetComponent<PlayableDirector>();
        this.cubeGenerator = this.cubeGeneratorObject.GetComponent<CubeGenerator>();
    }

    void Update()
    {
        if (transform.position.x <= -13)
        {
            Destroy(gameObject);
        }

        if (this.uIController.isGameOver || this.uIController.clearScene/* || this.oneTime*/)
        {
            return;
        }

        /*
        if (this.uIController.length >= 150 && this.oneTime == false)
        {
            iTween.MoveTo(gameObject, iTween.Hash("x", -6.0f, "time", 2.0f, "easeType", "linear"));
            this.oneTime = true;
        }
        */

        /*
        //カウントを数える
        this.count += Time.deltaTime;
        //posXの値をposMin～posMaxの間で決定する
        if (this.count >= this.span)
        {
            this.posX = Random.Range(this.posMin, this.posMax);
            this.count = 0;
        }
        //length140を超えるかボス出現中は後方に下がる
        if (this.uIController.length >= 140 || this.cubeGenerator.isBoss)
        {
            this.posX = this.posMin;
        }
        // 目標地点に徐々に近づいていく、目標地点が遠いほど速度は速くなる
        transform.Translate((this.posX - transform.position.x) * this.speed * Time.deltaTime, 0.0f, 0.0f);
        */
    }

    /// <summary>
    /// 接触したオブジェクトに力を与えて吹き飛ばす
    /// </summary>
    /// <param name="other">接触したオブジェクト</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Player":
                Destroy(other.gameObject);
                this.gameOverTimeLineDirector.Play();
                /*TimeLineを使用しない場合のGameOverSceneを演出するスクリプト
                Destroy(other.gameObject);
                this.animator.SetTrigger("Eat");
                this.se[1].Play();
                iTween.PunchScale(gameObject, iTween.Hash("x", 3.0f, "time", 2.5f, "delay", 0.1f));
                iTween.PunchScale(gameObject, iTween.Hash("x", 2.9f, "time", 2.5f, "delay", 1.0f));
                iTween.PunchScale(gameObject, iTween.Hash("x", 2.8f, "time", 2.5f, "delay", 1.0f));
                iTween.PunchScale(gameObject, iTween.Hash("x", 2.7f, "time", 2.5f, "delay", 1.0f));
                iTween.PunchScale(gameObject, iTween.Hash("x", 2.6f, "time", 2.5f, "delay", 1.0f));
                iTween.PunchScale(gameObject, iTween.Hash("x", 2.5f, "time", 2.5f, "delay", 1.0f));
                iTween.PunchScale(gameObject, iTween.Hash("x", 2.4f, "time", 2.5f, "delay", 1.0f));
                Invoke("GameEnd", 4.5f);
                */
                break;
            
            /*
            case "Plazm":
                this.se[0].Play();
                this.animator.SetTrigger("Death");
                this.scrollStop = true;
                iTween.MoveTo(gameObject, iTween.Hash("x", -13.0f, "time", 4.0f, "delay", 2.5f, "easeType", "linear"));
                break;
            */

            case "Block":
            case "HBlock":
                this.verticalPower = Random.Range(this.vPowerMin, this.vPowerMax);
                this.horizontalPower = Random.Range(this.hPowerMin, this.hPowerMax);
                other.GetComponent<Rigidbody2D>().AddForce(new Vector2(this.horizontalPower * Time.deltaTime,
                    this.verticalPower * Time.deltaTime));
                this.rollSpeed = Random.Range(this.rsMin, this.rsMax);
                iTween.RotateTo(other.gameObject, iTween.Hash("z", this.rollSpeed, "time", this.fallTime));
                other.GetComponent<CubeController>().speed = 0;
                other.GetComponent<BoxCollider2D>().enabled = false;
                break;

            case "JumpBall":
                this.verticalPower = Random.Range(this.vPowerMin, this.vPowerMax);
                this.horizontalPower = Random.Range(this.hPowerMin, this.hPowerMax);
                other.GetComponent<Rigidbody2D>().AddForce(new Vector2(this.horizontalPower * Time.deltaTime,
                    this.verticalPower * Time.deltaTime));
                this.rollSpeed = Random.Range(this.rsMin, this.rsMax);
                iTween.RotateTo(other.gameObject, iTween.Hash("z", this.rollSpeed, "time", this.fallTime));
                other.GetComponent<JumpBallController>().speed = 0;
                other.GetComponent<BoxCollider2D>().enabled = false;
                other.GetComponent<CircleCollider2D>().enabled = false;
                break;

            default:
                break;
        }
    }

    /*TimeLineを使用しない場合のGameOverSceneを演出するスクリプト
    private void GameEnd()
    {
        this.se[1].Stop();
        this.animator.SetTrigger("End");
        iTween.RotateTo(gameObject, iTween.Hash("y", 0.0f));
        iTween.MoveTo(gameObject, iTween.Hash("x", -12.0f, "time", 3.0f, "delay", 0.1f, "easeType", "linear"));
        Destroy(gameObject, 3.1f);
    }*/
}
