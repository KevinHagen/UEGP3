using System.Collections;
using UnityEngine;

namespace UEGP3.Demos.Rider
{
	public class AClassWithABadName : MonoBehaviour
	{
		[SerializeField] private float _aPoorlyNamedFloatValue;

		public void DoSomething()
		{
			StartCoroutine("DoSomethingAsACoroutine");
		}

		private IEnumerator DoSomethingAsACoroutine()
		{
			Debug.Log("WOW IT DID SOMETHING!");
			yield return null;
			Debug.Log("WOW IT WAITED FOR A FRAME!");
		}
	}
}