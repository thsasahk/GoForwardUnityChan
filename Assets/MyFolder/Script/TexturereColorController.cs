using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TexturereColorController : MonoBehaviour
{
    /// <summary>
    /// フォントのマテリアル
    /// </summary>
    private Material material;
    /// <summary>
    /// オブジェクトのTextMeshコンポーネント
    /// </summary>
    private TextMeshProUGUI tmp;
    /// <summary>
    /// Gradientを作成
    /// </summary>
    [SerializeField] private Gradient gradient;
    /// <summary>
    /// 返し値の器
    /// </summary>
    private Color color;

    void Start()
    {
        this.tmp = GetComponent<TextMeshProUGUI>();
        this.material = this.tmp.fontMaterial;
    }

    void Update()
    {
        this.material.SetColor(ShaderUtilities.ID_UnderlayColor, ColorGradient());
    }

    private Color ColorGradient()
    {
        this.color = this.gradient.Evaluate(Mathf.Abs(Mathf.Sin(Time.time)));
        return color;
    }
}
