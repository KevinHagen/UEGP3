using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace UEGP3.CustomPlayables
{
    /// <summary>
    /// PlayableClip to block the players input
    /// </summary>
    [Serializable]
    public class BlockInputPlayableClip : PlayableAsset, ITimelineClipAsset
    {
        public ClipCaps clipCaps => ClipCaps.None;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<BlockInputPlayableBehaviour>.Create(graph);
            return playable;
        }
    }
}
