using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewRecordColorController : MonoBehaviour
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
    /// GameOverオブジェクト
    /// </summary>
    [SerializeField] private GameObject gameOverText;
    /// <summary>
    /// GameOverオブジェクトのスクリプト
    /// </summary>
    private GameOverTextColorController gotColorController;
    /// <summary>
    /// オブジェクトのTextMeshProUGUI
    /// </summary>
    private TextMeshProUGUI tMPro;
    /// <summary>
    /// オブジェクトのフォントマテリアル
    /// </summary>
    private Material material;
    /// <summary>
    /// GameOver時のオブジェクトのUnderlayの色
    /// </summary>
    private Color color;

    // Start is called before the first frame update
    void Start()
    {
        this.uIController = this.canvas.GetComponent<UIController>();
        this.gotColorController = this.gameOverText.GetComponent<GameOverTextColorController>();
        this.tMPro = GetComponent<TextMeshProUGUI>();
        this.material = tMPro.fontMaterial;
        this.color = new Color(0, 0, 0, 255);
    }

    // Update is called once per frame
    void Update()
    {
        switch (this.uIController.recordUpdate)
        {
            case "No":
                if (this.uIController.isGameOver)
                {
                    GetComponent<TextMeshProUGUI>().faceColor = new Color(255, 255, 255, 128);
                    this.material.SetColor(ShaderUtilities.ID_UnderlayColor, this.color);
                    this.GetComponent<TextMeshProUGUI>().text = "Score:" + this.uIController.score.ToString() + "pts";
                    return;
                }
                this.GetComponent<TextMeshProUGUI>().text = "Score:" + this.uIController.score.ToString() + "pts";
                break;

            case "Yes":
                if (this.uIController.isGameOver)
                {
                    GetComponent<TextMeshProUGUI>().faceColor = new Color(255, 255, 255, 128);
                    this.material.SetColor(ShaderUtilities.ID_UnderlayColor, this.color);
                    this.GetComponent<TextMeshProUGUI>().text = "New Record:" + this.uIController.score.ToString() + "pts";
                    return;
                }
                GetComponent<TextMeshProUGUI>().faceColor = this.gotColorController.color;
                this.tMPro.text = "New Record:" + this.uIController.score.ToString() + "pts";
                break;

            default:
                break;

        }
    }
}
