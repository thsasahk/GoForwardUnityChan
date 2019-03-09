using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPanelController : MonoBehaviour
{
    /// <summary>
    /// Playerオブジェクト
    /// </summary>
    [SerializeField] private GameObject player;
    /// <summary>
    /// Playerオブジェクトのスクリプト
    /// </summary>
    private UnityChanController unityChanController;

    void Start()
    {
        this.unityChanController = this.player.GetComponent<UnityChanController>();
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            this.unityChanController.StarShot();
            this.unityChanController.starShot = false;
        }
    }
}
