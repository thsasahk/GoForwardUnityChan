using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StarPanelTextController : MonoBehaviour
{
    /// <summary>
    /// StarPanelオブジェクト
    /// </summary>
    [SerializeField] private GameObject starPanel;
    /// <summary>
    /// StarPanelオブジェクトのスクリプト
    /// </summary>
    private StarPanelController starPanelController;
    /// <summary>
    /// オブジェクトのフォントマテリアルの器
    /// </summary>
    private Material material;
    /// <summary>
    /// オブジェクトのTextMeshProUGUI
    /// </summary>
    private TextMeshProUGUI tmp;
    /// <summary>
    /// オブジェクトのGradient
    /// </summary>
    public Gradient gradiate;
    /// <summary>
    /// ColorGradiate()関数の返し値に使用
    /// </summary>
    private Color color;
    /// <summary>
    /// Gradien.Evauluent()の引数
    /// </summary>
    private float i;

    void Start()
    {
        this.starPanelController = this.starPanel.GetComponent<StarPanelController>();
        this.tmp = GetComponent<TextMeshProUGUI>();
        this.material = this.tmp.fontMaterial;
    }

    void Update()
    {
        if (this.starPanelController.starCount != 0)
        {
            this.tmp.text = "クリックorZキー";
            this.material.SetColor(ShaderUtilities.ID_UnderlayColor, ColorGradiate());
        }
        else
        {
            this.tmp.text = "";
        }
    }

    private Color ColorGradiate()
    {
        this.i = Mathf.Abs(Mathf.Sin(Time.time));
        this.color = this.gradiate.Evaluate(this.i);
        return this.color;
    }
}
