using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[System.Serializable]
public class Dialog : PlayableAsset, ITimelineClipAsset
{
    public DialogTemplate template = new DialogTemplate();

    public ClipCaps clipCaps
    {
        get { return ClipCaps.None; }
    }

    public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
    {
        var playable = ScriptPlayable<DialogTemplate>.Create(graph, template);
        return playable;
    }
}
