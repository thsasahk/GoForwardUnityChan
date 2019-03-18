using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TSStarGenerator : MonoBehaviour
{
    /// <summary>
    /// Starオブジェクト
    /// </summary>
    [SerializeField] private GameObject star;
    /// <summary>
    /// Starの生成間隔を決定する
    /// </summary>
    private float span;
    /// <summary>
    /// span変数の最低値
    /// </summary>
    [SerializeField] private float minSpan;
    /// <summary>
    /// span変数の最大値
    /// </summary>
    [SerializeField] private float maxSpan;
    /// <summary>
    /// 最後にStarを生成してからの時間を管理する
    /// </summary>
    private float time = 0;

    void Start()
    {
        //最初のStarを生成する時間を決定する
        this.span = Random.Range(this.minSpan, this.maxSpan);
    }

    void Update()
    {
        this.time += Time.deltaTime;
        if (this.time >= this.span)
        {
            Instantiate(this.star, new Vector2(13, 5), Quaternion.identity);
            //次にStarを生成する時間を決定する
            this.span = Random.Range(this.minSpan, this.maxSpan);
            this.time = 0;
        }
    }
}
