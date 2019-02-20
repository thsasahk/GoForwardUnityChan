using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PauseCanvasController : MonoBehaviour
{
    /// <summary>
    /// ポーズした時に表示するUI
    /// </summary>
    [SerializeField] private GameObject pausePanel;
    /// <summary>
    /// TimeLineオブジェクト
    /// </summary>
    [SerializeField] private GameObject timeLine;
    /// <summary>
    /// TimeLineDirector
    /// </summary>
    private PlayableDirector timeLineDirector;
    /// <summary>
    /// Canvasオブジェクト
    /// </summary>
    [SerializeField] private GameObject canvas;
    /// <summary>
    /// Canvasオブジェクトのスクリプト
    /// </summary>
    private UIController uiController;
    /// <summary>
    /// 処理を一度きりにするための変数
    /// </summary>
    private bool onetime = false;

    void Start()
    {
        this.timeLineDirector = this.timeLine.GetComponent<PlayableDirector>();
        if (this.canvas == null)
        {
            return;
        }
        this.uiController = this.canvas.GetComponent<UIController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            //TimeLine再生中はポーズ禁止
            if (this.timeLineDirector.state == PlayState.Playing)
            {
                return;
            }
            /*TimeLineも停止
            if (this.timeLineDirector.state == PlayState.Playing)
            {
                this.timeLineDirector.playableGraph.GetRootPlayable(0).SetSpeed(0);
            }*/

            //　ポーズUIのアクティブ、非アクティブを切り替え
            this.pausePanel.SetActive(!this.pausePanel.activeSelf);
            //　ポーズUIが表示されてる時は停止
            if (this.pausePanel.activeSelf)
            {
                Time.timeScale = 0f;
                //　ポーズUIが表示されてなければ通常通り進行
            }
            else
            {
                Time.timeScale = 1f;
                if (this.timeLineDirector.state == PlayState.Playing)
                {
                    this.timeLineDirector.playableGraph.GetRootPlayable(0).SetSpeed(1);
                }
            }
        }
        if (this.canvas == null || this.onetime)
        {
            return;
        }
        if (this.uiController.isGameOver)
        {
            this.timeLine = GameObject.Find("GameOverSceneTimeLine");
            this.timeLineDirector = this.timeLine.GetComponent<PlayableDirector>();
            this.onetime = true;
        }
    }
}
