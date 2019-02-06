using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSceneManagerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject BG = Resources.Load("BackGround") as GameObject;
        Create(BG,new Vector2(0, 0),new Vector2(0, 0));
        Instantiate(BG, Vector2.zero, Quaternion.identity);
        GameObject player = Resources.Load("TSPlayerPrefab") as GameObject;
        Create(player, new Vector2(-2.9f, 0), new Vector2(0, 0));
        GameObject gem = Resources.Load("TutorialSceneGemPrefab") as GameObject;
        Create(gem, new Vector2(6.0f, 0.0f), new Vector2(0.0f, 0.0f));
    }

    // Update is called once per frame
    void Update()
    {
        Resources.UnloadUnusedAssets();
    }

    void Create(GameObject obj,Vector2 pos,Vector2 rotate)
    {
        GameObject go = Instantiate(obj) as GameObject;
        go.transform.position = pos;
        go.transform.Rotate(rotate);
    }
}