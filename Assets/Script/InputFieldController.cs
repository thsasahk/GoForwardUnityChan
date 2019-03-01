using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;
using TMPro;

public class InputFieldController : MonoBehaviour
{
    private TMP_InputField inputField;

    private NCMBObject ranking;

    private string playerName = "NoName";

    [SerializeField] private GameObject canvas;

    private UIController uIController;

    // Start is called before the first frame update
    void Start()
    {
        this.ranking = new NCMBObject("Ranking");
        this.uIController = this.canvas.GetComponent<UIController>();
        this.inputField = GetComponent<TMP_InputField>();
    }

    // Update is called once per frame
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
        //this.ranking = new NCMBObject("Ranking");
        if (inputField.text == null)
        {
            inputField.text = "NoName";
        }
        this.ranking["name"] = inputField.text;
        this.ranking["score"] = this.uIController.score;
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
            }
        });

        //this.uIController.Load();
    }
}
