using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialSceneManagerController : MonoBehaviour
{
    public int lesson = 1;
    public bool isPlayer = true;
    [SerializeField] private GameObject player;
    private bool loadScene = false;

    private GameObject hCube;

    private TSCubeController tSCubeController;

    void Start()
    {
        
    }

    void Update()
    {
        if ((this.lesson == 5 || Input.GetKeyDown(KeyCode.RightShift)|| Input.GetKeyDown(KeyCode.LeftShift)) 
            && this.loadScene == false)
        {
            LoadScene();
        }

        if (this.isPlayer == false)
        {
            this.isPlayer = true;
            GameObject clone = Instantiate(this.player) as GameObject;
            clone.transform.position = new Vector2(-2.9f, 7.0f);
            clone.transform.Rotate(new Vector2(0.0f, 0.0f));
            this.hCube = GameObject.Find("TutrialSceneHardPrefab(Clone)");
            this.tSCubeController = this.hCube.GetComponent<TSCubeController>();
            this.tSCubeController.lesson4Start = false;
            this.lesson++;
        }
    }

    void LoadScene()
    {
        loadScene = true;
        FadeManager.Instance.LoadScene("GameScene", 1.0f);
    }
}
