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

    public void TextMoveTop()
    {
        this.lessonManual.text = " ";
        this.animator.SetTrigger("NextLesson");
        Invoke("TextMoveCenter", 1.0f);
    }

    public void TextMoveCenter()
    {
        switch (this.tutorialSceneManagerController.lesson)
        {
            case 1:
                this.lessonManual.text = "Lesson1/nRight-Click or Ctr/nShot";
                break;

            case 2:
                this.lessonManual.text = "Lesson2/nHold Down Right-Click or Ctr/nCharge Shot";
                break;

            case 3:
                this.lessonManual.text = "Lesson3";
                break;

            case 4:
                this.lessonManual.text = "Lesson4";
                break;

            default:
                break;
        }
        this.animator.SetTrigger("LessonStart");
    }

    public void TextMoveBotom()
    {
        this.animator.SetTrigger("LessonClear");
        Invoke("TextMoveTop", 1.0f);
    }
}
