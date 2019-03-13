using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class TutorialSceneManagerController : MonoBehaviour
{
    /// <summary>
    /// チュートリアルの進行を管理する変数
    /// </summary>
    public int lesson = 1;
    /// <summary>
    /// Playerオブジェクトの有無を表す
    /// </summary>
    public bool isPlayer = true;
    /// <summary>
    /// Playerオブジェクト
    /// </summary>
    [SerializeField] private GameObject player;
    /// <summary>
    /// LoadSceneを実行した際にtrueにする
    /// </summary>
    public bool loadScene = false;
    /// <summary>
    /// シーン遷移の際に再生するTimeLine
    /// </summary>
    [SerializeField] private GameObject tutorialTimeLine;
    /// <summary>
    /// TimeLineを停止させる際に使用するPlayableDirector
    /// </summary>
    private PlayableDirector tutorialTimeLineDirector;

    void Start()
    {
        this.tutorialTimeLineDirector = this.tutorialTimeLine.GetComponent<PlayableDirector>();
        //this.manualText = GameObject.Find("ManualText");
        //this.tSUIController = this.manualText.GetComponent<TSUIController>();
    }

    void Update()
    {
        if((Input.GetKeyDown(KeyCode.LeftShift)|| Input.GetKeyDown(KeyCode.RightShift)) && this.loadScene == false)
        {
            TimeLine();
        }
        /*lessonが5になるかShiftキーでゲームシーンへ移行
        if ((this.lesson == 5||Input.GetKeyDown(KeyCode.LeftShift)|| Input.GetKeyDown(KeyCode.RightShift))
            && this.loadScene == false)
        {
            LoadScene();
        }
        //Playerオブジェクトが破棄されたとき、新しいPlayerオブジェクトを生成する
        //lesson4Start変数を切り替えてlesson4を仕切り直す
        if (this.isPlayer == false)
        {
            this.isPlayer = true;
            GameObject clone = Instantiate(this.player) as GameObject;
            clone.transform.position = new Vector2(-2.9f, 7.0f);
            clone.transform.Rotate(new Vector2(0.0f, 0.0f));
            this.hCube = GameObject.Find("TutrialSceneHardPrefab(Clone)");
            this.tSCubeController = this.hCube.GetComponent<TSCubeController>();
            this.tSCubeController.lesson4Start = false;
        }*/
    }

    public void TimeLine()
    {
        this.loadScene = true;
        Invoke("TimeLineStart", 1.0f);
        Invoke("LoadScene", 6.0f);
    }

    /// <summary>
    /// キー入力から呼び出しまでに間を置くために関数化
    /// </summary>
    void TimeLineStart()
    {
        this.tutorialTimeLineDirector.Play();
    }

    void LoadScene()
    {
        this.tutorialTimeLineDirector.playableGraph.GetRootPlayable(0).SetSpeed(0);
        FadeManager.Instance.LoadScene("GameScene", 1.0f);
    }
}
