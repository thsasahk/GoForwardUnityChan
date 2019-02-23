using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewRecordColorController : MonoBehaviour
{

    [SerializeField] private GameObject canvas;

    private UIController uIController;
    /// <summary>
    /// GameOverオブジェクト
    /// </summary>
    [SerializeField] private GameObject gameOverText;
    /// <summary>
    /// GameOverオブジェクトのスクリプト
    /// </summary>
    private GameOverTextColorController gotColorController;

    private TextMeshProUGUI tMPro;

    // Start is called before the first frame update
    void Start()
    {
        this.uIController = this.canvas.GetComponent<UIController>();
        this.gotColorController = this.gameOverText.GetComponent<GameOverTextColorController>();
        this.tMPro = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (this.uIController.recordUpdate)
        {
            case "No":
                this.GetComponent<TextMeshProUGUI>().text = "Score:" + this.uIController.score.ToString() + "pts";
                break;

            case "Yes":
                GetComponent<TextMeshProUGUI>().faceColor = this.gotColorController.color;
                this.tMPro.text = "NEW RECORD:" + this.uIController.score.ToString() + "PTS";
                break;

            default:
                break;

        }
    }
}
