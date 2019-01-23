using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour
{
    public float fallTime;
    public float stopPosY;
    public float stopPosX;
    public float rollAngle;
    public float speedX;
    public float speedY;
    void Start()
    {
        iTween.MoveTo(gameObject,iTween.Hash("x",this.stopPosX,"y",this.stopPosY,"time",this.fallTime));
        iTween.MoveAdd(this.gameObject,iTween.Hash("x",this.speedX*Mathf.Cos(this.transform.eulerAngles.z * Mathf.Deg2Rad),
        "y",this.speedY*Mathf.Sin(this.transform.eulerAngles.z * Mathf.Deg2Rad),"time",this.fallTime));
        iTween.RotateBy(gameObject,iTween.Hash("z",this.rollAngle,"time",this.fallTime));
        
    }

    
    void Update()
    {
    }
}

