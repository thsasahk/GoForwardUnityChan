using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-5.5f * Time.deltaTime, 0, 0);//{16:9 -7_1028:786 -5.5}
        Destroy(gameObject, 3.0f);
    }
}
