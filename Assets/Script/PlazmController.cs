using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlazmController : MonoBehaviour
{
    [SerializeField] private float speed;

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
}
