using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseCanvasController : MonoBehaviour
{
    //　ポーズした時に表示するUI
    [SerializeField] private GameObject pausePanel;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
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
            }
        }
    }
}
