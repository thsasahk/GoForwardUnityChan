using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSoundController : MonoBehaviour
{
    private float destroyTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.destroyTime+=Time.deltaTime;
        if(this.destroyTime>=2)Destroy(gameObject);
    }
}
