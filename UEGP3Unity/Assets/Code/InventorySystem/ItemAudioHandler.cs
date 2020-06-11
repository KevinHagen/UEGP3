using UnityEngine;

namespace UEGP3.InventorySystem
{
	[RequireComponent(typeof(AudioSource))]
	public class ItemAudioHandler : MonoBehaviour
	{
		private AudioSource _audioSource;

		private void Awake()
		{
			_audioSource = GetComponent<AudioSource>();
			Item.OnItemUsed += OnItemUsed;
		}

		private void OnDestroy()
		{
			Item.OnItemUsed -= OnItemUsed;
		}

		private void OnItemUsed(Item item)
		{
			if (item == null)
			{
				return;
			}

			item.UseAudioEvent.Play(_audioSource);
		}
	}
}