using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class CanonBall_Asset : PlayableAsset
{
    public ExposedReference<GameObject> sceneCanonBall;

    public GameObject canonBall;

    // Factory method that generates a playable based on this asset
    // https://www.crossroad-tech.com/entry/Unity2017_2_0_timeline#4-5-Playable-Track
    // https://simplestar-tech.hatenablog.com/entry/2018/09/23/141755
    public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
    {
        CanonBall_Behaviour canon_Behaviour = new CanonBall_Behaviour();
        canon_Behaviour.sceneCanonBall = sceneCanonBall.Resolve(graph.GetResolver());
        canon_Behaviour.canonBall = canonBall;
        return ScriptPlayable<CanonBall_Behaviour>.Create(graph, canon_Behaviour);
        //return Playable.Create(graph);
    }
}
