using UEGP3.Core;
using UnityEngine;

namespace UEGP3.PlayerSystem
{
	[RequireComponent(typeof(AudioSource))]
	public class AudioEventHandler : MonoBehaviour
	{
		private AudioSource _audioSource;

		private void Awake()
		{
			_audioSource = GetComponent<AudioSource>();
		}

		// Called as an animation event
		private void PlayAudioEvent(AnimationEvent animEvent)
		{
			ScriptableAudioEvent audioEvent = animEvent.objectReferenceParameter as ScriptableAudioEvent;

			if (audioEvent != null)
			{
				audioEvent.Play(_audioSource);
			}
		}
	}
}