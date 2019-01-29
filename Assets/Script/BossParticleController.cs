using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossParticleController : MonoBehaviour
{
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 3.0f);
    }
}
