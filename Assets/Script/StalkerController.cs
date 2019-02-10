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

    void Start()
    {
        this.posX = this.posMin;
        this.animator = GetComponent<Animator>();
        this.canvas = GameObject.Find("Canvas");
        this.uIController = this.canvas.GetComponent<UIController>();
    }

    void Update()
    {
        if (this.uIController.isGameOver)
        {
            return;
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
                iTween.PunchScale(gameObject, iTween.Hash("x", 3.5f, "time",10.0f, "delay", 0.1f));
                Invoke("GameEnd", 7.0f);
                break;

            default:
                break;
        }
    }

    private void GameEnd()
    {
        this.animator.SetTrigger("End");
        iTween.RotateTo(gameObject, iTween.Hash("y", 0.0f));
        iTween.MoveTo(gameObject, iTween.Hash("x", -12.0f, "time", 3.0f, "delay", 0.1f, "easeType", "linear"));
        Destroy(gameObject, 3.1f);
    }
}
