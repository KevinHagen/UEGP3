using UEGP3.PlayerSystem;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace UEGP3.CustomPlayables
{
    /// <summary>
    /// Track to block the players input
    /// </summary>
    [TrackColor(0.355f, 0.8623f, 0.27f)]
    [TrackClipType(typeof(BlockInputPlayableClip))]
    [TrackBindingType(typeof(PlayerController))]
    public class BlockInputPlayableTrack : TrackAsset
    {
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            return ScriptPlayable<BlockInputPlayableMixerBehaviour>.Create(graph, inputCount);
        }
    }
}
