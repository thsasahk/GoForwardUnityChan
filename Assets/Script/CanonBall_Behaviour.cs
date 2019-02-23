using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

// A behaviour that is attached to a playable
public class CanonBall_Behaviour : PlayableBehaviour
{
    public GameObject sceneCanonBall;
    /// <summary>
    /// CanonBallオブジェクト
    /// </summary>
    public GameObject canonBall;
    /// <summary>
    /// 破棄時に生成されるパーティクル
    /// </summary>
    private GameObject particle;
    /// <summary>
    /// CanonBallオブジェクトの移動速度
    /// </summary>
    private float speed = -7;
    /// <summary>
    /// CanonBallオブジェクトの速度ベクトル
    /// </summary>
    private Vector2 canonBallSpeed;

    private float count = 0;

    // Called when the owning graph starts playing
    public override void OnGraphStart(Playable playable)
    {
        this.canonBall = GameObject.Find("CanonBall");
        this.canonBall.transform.position = new Vector2(0.6f, -2.4f);
        this.canonBall.transform.Rotate(Vector2.zero);
        this.canonBallSpeed = new Vector2(this.speed * Time.deltaTime, 0.0f);
        /*
        this.particle = GameObject.Find("CanonBallParticle");
        this.particle.transform.position = this.canonBall.transform.position;
        */
    }

    // Called when the owning graph stops playing
    public override void OnGraphStop(Playable playable)
    {
        
    }

    // Called when the state of the playable is set to Play
    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        
    }

    // Called when the state of the playable is set to Paused
    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        
    }

    // Called each frame while the state is set to Play
    public override void PrepareFrame(Playable playable, FrameData info)
    {
        //this.canonBall.transform.Translate(this.canonBallSpeed);
    }
}
