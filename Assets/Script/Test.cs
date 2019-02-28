using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;

public class Test : MonoBehaviour
{
    //NCMBクラスのオブジェクト
    NCMBObject testClass;
    // Start is called before the first frame update
    void Start()
    {
        this.testClass = new NCMBObject("TestClass");
    }

    // Update is called once per frame
    void Update()
    {
        // オブジェクトに値を設定
        this.testClass["message"] = "Hello, NCMB!";
        // データストアへの登録
        this.testClass.SaveAsync();
    }
}
