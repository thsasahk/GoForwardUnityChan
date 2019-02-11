using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonController : MonoBehaviour
{
    /// <summary>
    /// Canvasオブジェクト
    /// </summary>
    [SerializeField] private GameObject canvas;
    /// <summary>
    /// Canvasオブジェクトのスクリプト
    /// </summary>
    private UIController uIController;
    /// <summary>
    /// Prazmオブジェクト
    /// </summary>
    [SerializeField] private GameObject prazm;
    /// <summary>
    /// Preparationを連続して実行しないための変数
    /// </summary>
    private bool oneTime = false;

    void Start()
    {
        this.uIController = this.canvas.GetComponent<UIController>();
    }

    void Update()
    {
        if (this.uIController.length >= 150)
        {
            Preparation();
        }
    }

    void Preparation()
    {
        if (this.oneTime)
        {
            return;
        }
        iTween.MoveTo(gameObject, iTween.Hash("x", 6.6f, "time", 3.0f, "easeType", "linear"));
        iTween.ShakeScale(gameObject, iTween.Hash("x", 0.1f, "y", 0.1f, "time", 5.0f, "delay", 3.0f));
        iTween.MoveTo(gameObject, iTween.Hash("x", 4.5f, "time", 2.0f, "delay", 3.0f, "easeType", "linear"));
        iTween.MoveTo(gameObject, iTween.Hash("x", -2.5f, "time", 4.0f, "delay", 8.0f, "easeType", "linear"));
        Invoke("PrazmShot", 5.1f);
        this.oneTime = true;
    }

    void PrazmShot()
    {
        GameObject go = Instantiate(prazm) as GameObject;
        go.transform.position = new Vector2(0.6f, -2.4f);
        go.transform.Rotate(new Vector2(0.0f, 0.0f));
    }
}
