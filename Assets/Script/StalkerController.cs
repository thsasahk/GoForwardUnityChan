using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private GameObject canvas;

    private UIController uIController;

    private bool oneTime = false;

    public bool scrollStop = false;

    private AudioSource[] se;

    void Start()
    {
        this.posX = this.posMin;
        this.animator = GetComponent<Animator>();
        this.canvas = GameObject.Find("Canvas");
        this.uIController = this.canvas.GetComponent<UIController>();
        this.se = GetComponents<AudioSource>();
    }

    void Update()
    {
        if (this.uIController.isGameOver || this.oneTime)
        {
            return;
        }

        if (this.uIController.length >= 150 && this.oneTime == false)
        {
            iTween.MoveTo(gameObject, iTween.Hash("x", -6.0f, "time", 2.0f, "easeType", "linear"));
            this.oneTime = true;
        }

        //カウントを数える
        this.count += Time.deltaTime;
        //posXの値をposMin～posMaxの間で決定する
        if (this.count >= this.span)
        {
            this.posX = Random.Range(this.posMin, this.posMax);
            this.count = 0;
        }

        // 目標地点に徐々に近づいていく、目標地点が遠いほど速度は速くなる
        transform.Translate((this.posX - transform.position.x) * this.speed * Time.deltaTime, 0.0f, 0.0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Player":
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
                break;

            case "Plazm":
                this.se[0].Play();
                this.animator.SetTrigger("Death");
                this.scrollStop = true;
                iTween.MoveTo(gameObject, iTween.Hash("x", -13.0f, "time", 4.0f, "delay", 2.5f, "easeType", "linear"));
                break;

            default:
                break;
        }
    }

    private void GameEnd()
    {
        this.se[1].Stop();
        this.animator.SetTrigger("End");
        iTween.RotateTo(gameObject, iTween.Hash("y", 0.0f));
        iTween.MoveTo(gameObject, iTween.Hash("x", -12.0f, "time", 3.0f, "delay", 0.1f, "easeType", "linear"));
        Destroy(gameObject, 3.1f);
    }
}
