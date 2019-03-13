using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlazmController : MonoBehaviour
{
    /// <summary>
    /// オブジェクトの移動速度
    /// </summary>
    [SerializeField] private float speed;
    /// <summary>
    /// 破棄時に生成されるパーティクル
    /// </summary>
    [SerializeField] private GameObject particle;

    private Vector2 plazmSpeed;
    void Start()
    {
        this.plazmSpeed = new Vector2(this.speed * Time.deltaTime, 0.0f);
    }

    void Update()
    {
        transform.Translate(this.plazmSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Instantiate(this.particle, transform.position, Quaternion.identity);
    }
}
