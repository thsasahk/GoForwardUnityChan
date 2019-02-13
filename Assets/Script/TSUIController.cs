using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TSUIController : MonoBehaviour
{
    /// <summary>
    /// TutorialSceneManagerオブジェクト
    /// </summary>
    [SerializeField] private GameObject tutorialSceneManager;
    /// <summary>
    /// TutorialSceneManagerオブジェクトのスクリプト
    /// </summary>
    private TutorialSceneManagerController tutorialSceneManagerController;
    /// <summary>
    /// オブジェクトのAnimatorコンポーネント
    /// </summary>
    private Animator animator;
    /// <summary>
    /// オブジェクトのRectTransformオブジェクト
    /// </summary>
    private RectTransform rectTransform;
    /// <summary>
    /// オブジェクトのTextMeshProUGUIオブジェクト
    /// </summary>
    private TextMeshProUGUI lessonManual;

    void Start()
    {
        this.tutorialSceneManagerController = this.tutorialSceneManager.GetComponent<TutorialSceneManagerController>();
        this.animator = GetComponent<Animator>();
        this.rectTransform = GetComponent<RectTransform>();
        this.lessonManual = GetComponent<TextMeshProUGUI>();
        TextMoveCenter();
    }

    void Update()
    {
        
    }

    /// <summary>
    /// オブジェクトのTextを空にする
    /// オブジェクトを画面外上部に移動させる
    /// 1秒後にTextMoveCenter関数を起動させる
    /// </summary>
    public void TextMoveTop()
    {
        if (this.tutorialSceneManagerController.loadScene)
        {
            return;
        }
        this.lessonManual.text = " ";
        this.animator.SetTrigger("NextLesson");
        Invoke("TextMoveCenter", 1.0f);
    }

    /// <summary>
    /// オブジェクトのTextをlesson変数に合わせて変化させる
    /// オブジェクトを画面中央に移動させる
    /// </summary>
    public void TextMoveCenter()
    {
        switch (this.tutorialSceneManagerController.lesson)
        {
            case 1:
                this.lessonManual.text = "Lesson1\nCtr or Right-Click\nShot";
                break;

            case 2:
                this.lessonManual.text = "Lesson2\nLong Press Ctr or Right-Click\nCharge Shot";
                break;

            case 3:
                this.lessonManual.text = "Lesson3\nMore Long Press Ctr or Right-Click\nPierce Shot";
                break;

            case 4:
                this.lessonManual.text = "Lesson4\nHold Down Space or Left-Click\nHover";
                break;

            default:
                break;
        }
        this.animator.SetTrigger("LessonStart");
    }

    /// <summary>
    /// オブジェクトを画面外下部に移動させる
    /// 1秒後にTextMoveTop関数を起動させる
    /// </summary>
    public void TextMoveBotom()
    {
        if (this.tutorialSceneManagerController.lesson != 5)
        {
            this.animator.SetTrigger("LessonClear");
        }
        Invoke("TextMoveTop", 1.0f);
    }
}
