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
    private TextMeshProUGUI tmp;
    private Color color;
    private Material material;
    [SerializeField] private GameObject canvas;
    private UIController uIController;

    void Start()
    {
        this.tmp = GetComponent<TextMeshProUGUI>();
        this.material = tmp.fontMaterial;
        this.uIController = this.canvas.GetComponent<UIController>();
    }

    void Update()
    {
        if (this.uIController.clear)
        {
            this.material.SetColor(ShaderUtilities.ID_UnderlayColor, ColorGradiate());
        }
    }

    private Color ColorGradiate()
    {
        float i = Mathf.Abs(Mathf.Sin(Time.time));
        this.color = this.gradiate.Evaluate(i);
        return this.color;
    }
}
