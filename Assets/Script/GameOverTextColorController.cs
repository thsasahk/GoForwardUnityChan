using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverTextColorController : MonoBehaviour
{
    /// <summary>
    /// オブジェクトの色の変化を管理する
    /// </summary>
    public Gradient gradiate;
    /// <summary>
    /// オブジェクトのTextMeshProUGUI
    /// </summary>
    private TextMeshProUGUI tmp;
    /// <summary>
    /// オブジェクトのMaterial
    /// </summary>
    private Material material;
    /// <summary>
    /// Canvasオブジェクト
    /// </summary>
    [SerializeField] private GameObject canvas;
    /// <summary>
    /// Canvasオブジェクトのスクリプト
    /// </summary>
    private UIController uIController;
    /// <summary>
    /// 色変数、NewRecordColorControllerクラスから参照する
    /// </summary>
    public Color color;
    /// <summary>
    /// ハイスコア非更新時のUnderLayの色、Update内でnewを使わないために事前に設定
    /// </summary>
    private Color color2;

    void Start()
    {
        this.tmp = GetComponent<TextMeshProUGUI>();
        this.material = tmp.fontMaterial;
        this.uIController = this.canvas.GetComponent<UIController>();
        this.color2 = new Color(255, 255, 0, 255);
    }

    void Update()
    {
        //Clear時に表示されるテキストのUnderlayColorを変更する
        if (this.uIController.clear || this.uIController.isGameOver)
        {
            this.material.SetColor(ShaderUtilities.ID_UnderlayColor, ColorGradiate());
        }
        /*ハイスコア更新時と非更新時で演出を変える
        switch (this.uIController.recordUpdate)
        {
            case "No":
                this.material.SetColor(ShaderUtilities.ID_UnderlayColor, this.color2);
                break;

            case "Yes":
                this.material.SetColor(ShaderUtilities.ID_UnderlayColor, ColorGradiate());
                break;

            default:
                break;
        }
        */
    }

    /// <summary>
    /// 0～1の値をとる変数iをgradiate.Evaluateの引数としてColor型の変数を返す
    /// </summary>
    /// <returns></returns>
    private Color ColorGradiate()
    {
        /*
        float i = Mathf.Abs(Mathf.Sin(Time.time));
        this.color = this.gradiate.Evaluate(i);
        */
        switch (this.uIController.recordUpdate)
        {
            case "No":
                this.color = this.color2;
                break;

            case "Yes":
                float i = Mathf.Abs(Mathf.Sin(Time.time));
                this.color = this.gradiate.Evaluate(i);
                break;

            default:
                break;

        }
        if (this.uIController.isGameOver)
        {
            this.color = new Color(255, 0, 0, 255);
        }
        return this.color;
    }
}
