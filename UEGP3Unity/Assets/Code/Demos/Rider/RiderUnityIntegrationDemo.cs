using System;
using UEGP3.InventorySystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace UEGP3.Demos.Rider
{
	public class RiderUnityIntegrationDemo : MonoBehaviour
	{
		[FormerlySerializedAs("_gameObjectReferenceToBeRenamed")] [SerializeField] private GameObject _renamedObject = null;
		[SerializeField] private GameObject _gameObjectReference = null;
		[SerializeField] private float _someValue = 0.4f;
		[SerializeField] private float _someValueThatHasBeenChanged = 0.5f;

		private void Update()
		{
			if (_gameObjectReference == null)
			{
				Debug.Log($"Variable _gameObjectReference is null. Wow this was a performance heavy call!");
			}

			IndirectPerformanceHeavyCall();

			if (_someValue < 0)
			{
				DoSomethingEasy();
			}
			
			NestedIndirectPerformanceHeavyCall();
		}

		private void IndirectPerformanceHeavyCall()
		{
			Debug.Log("This is a performance heavy method call because it uses Debug.Log");
		}

		private void NestedIndirectPerformanceHeavyCall()
		{
			IndirectPerformanceHeavyCall();
		}

		private void DoSomethingEasy()
		{
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Player"))
			{
				// Deal Damage to Player
			}
		}

		private void CodeInspectionTests()
		{
			if (gameObject.CompareTag("Player"))
			{
				// Try removing the above string and retyping it. Rider should suggest available strings!
			}

			if (Input.GetButtonDown("Fire1"))
			{
				// Try removing the above string and retyping it. Rider should suggest available strings!
			}
			
			// This also works for layers, coroutine names, Invoke calls, ... All kinds of magic strings in unity!
		}

		private void TryNewOnUnityEngineObject()
		{
			// we can't call new on UnityEngine.Object deriving types and Rider knows this!
			ItemPickup animator = new ItemPickup();
			Item item = new HealItem();
		}
	}
}