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

    void Start()
    {
        this.tmp = GetComponent<TextMeshProUGUI>();
        this.material = tmp.fontMaterial;
        this.uIController = this.canvas.GetComponent<UIController>();
    }

    void Update()
    {
        //Clear時に表示されるテキストのUnderlayColorを変更する
        if (this.uIController.clear)
        {
            this.material.SetColor(ShaderUtilities.ID_UnderlayColor, ColorGradiate());
        }
    }

    /// <summary>
    /// 0～1の値をとる変数iをgradiate.Evaluateの引数としてColor型の変数を返す
    /// </summary>
    /// <returns></returns>
    private Color ColorGradiate()
    {
        float i = Mathf.Abs(Mathf.Sin(Time.time));
        Color color = this.gradiate.Evaluate(i);
        return color;
    }
}
