using UEGP3.PlayerSystem;
using UnityEngine.Playables;

namespace UEGP3.CustomPlayables
{
    /// <summary>
    /// MixerBehaviour that determines if the input needs to be blocked
    /// </summary>
    public class BlockInputPlayableMixerBehaviour : PlayableBehaviour
    {
        // Store binding for use in GraphStop
        private PlayerController _trackBinding;
        
        // NOTE: This function is called at runtime and edit time.  Keep that in mind when setting the values of properties.
        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            // cast playerData into our track binding
            _trackBinding = playerData as PlayerController;
            if (_trackBinding == null)
            {
                return;
            }

            // set default value for block input
            bool blockInput = false;
            int inputCount = playable.GetInputCount();

            for (int i = 0; i < inputCount; i++)
            {
                float inputWeight = playable.GetInputWeight(i);
                ScriptPlayable<BlockInputPlayableBehaviour> inputPlayable = (ScriptPlayable<BlockInputPlayableBehaviour>) playable.GetInput(i);
                BlockInputPlayableBehaviour input = inputPlayable.GetBehaviour();
            
                // Use the above variables to process each frame of this playable.

                // if any clip of this track is playing, set blockInput = true
                if (inputWeight > 0)
                {
                    blockInput = true;
                }
            }

            // set playerController.BlockInput according to our value
            _trackBinding.BlockInput = blockInput;
        }

        public override void OnGraphStop(Playable playable)
        {
            base.OnGraphStop(playable);

            // Need to reset the BlockInput variable once the playable gets stopped, so we can play again
            _trackBinding.BlockInput = false;
        }
    }
}
