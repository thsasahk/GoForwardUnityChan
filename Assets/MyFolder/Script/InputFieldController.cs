using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;
using TMPro;

public class InputFieldController : MonoBehaviour
{
    /// <summary>
    /// InputFieldController
    /// </summary>
    private TMP_InputField inputField;
    /// <summary>
    /// NCMBオブジェクト
    /// </summary>
    private NCMBObject ranking;
    /// <summary>
    /// InputFieldに入力される名前
    /// </summary>
    private string playerName = "NoName";
    /// <summary>
    /// Canvasオブジェクト
    /// </summary>
    [SerializeField] private GameObject canvas;
    /// <summary>
    /// Canvasオブジェクトのスクリプト
    /// </summary>
    private UIController uIController;

    private bool save;

    void Start()
    {
        this.ranking = new NCMBObject("Ranking");
        this.uIController = this.canvas.GetComponent<UIController>();
        this.inputField = GetComponent<TMP_InputField>();
    }

    void Update()
    {
        
    }
    /*
    public void InputName()
    {
        this.playerName = inputField.text;
    }
    */

    /// <summary>
    /// スコアをNCMBに記録
    /// </summary>
    /// <param name="i">今回プレイヤーが記録したスコア</param>
    public void NCMBSaveAsync()
    {
        //セーブは一度だけ
        if (!this.save)
        {
            this.ranking["name"] = inputField.text;
            this.ranking["score"] = this.uIController.score;
            this.save = true;
            this.ranking.SaveAsync();
            /*
            this.ranking.SaveAsync((NCMBException e) =>
            {
                if (e != null)
                {
                    //エラー処理
                    return;
                }
                else
                {
                    //成功時の処理
                    this.save = true;
                }
            });
            */
        }
        //this.uIController.Load();
    }
}
