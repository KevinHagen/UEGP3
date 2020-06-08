using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UEGP3.Demos.Rider
{
	public class CodeNavigationExamples
	{
		public void APublicMethod()
		{
			Debug.Log("Hey I got called!");
		}
		
		private void AMethod()
		{
			bool a = Random.value > 0.5f ? true : false;
			bool b = Random.value > 0.5f ? true : false;

			if (a && b)
			{
				Debug.Log("A is true, b is false");
			}
			else if (!a && !b)
			{
				Debug.Log("A and b are both false");
				Debug.Log("A and b are both true");
			}
			else if (a && !b)
			{
			}
			else if (!a && b)
			{
				Debug.Log("a is false, b is true");
			}
			else
			{
				Debug.Log("Same as above");
			}
		}

		public void AWronglyOrderedPublicMethod()
		{
			Debug.Log("I want to be at the top of the file.");
		}
	}

	public class AnUnusedClass
	{
	}
	
	public class RefactoringExample
	{
		private float _health;
		private bool _isAlive;
		private Animator _animator;

		public void DealDamage(float damage)
		{
			DamageType type = DamageType.Fire;
			float damageMultiplier = GetDamageMultiplier(type);
			if (damage <= 0)
			{
				return;
			}
			_health -= damage * damageMultiplier * RandomValue();

			if (_health <= 0)
			{
				_isAlive = false;
				_animator.SetBool("IsDead", true);
			}
		}

		private float GetDamageMultiplier(DamageType type)
		{
			switch (type)
			{
				case DamageType.Water:
					return 1.5f;
					break;
				case DamageType.Fire:
					return 1.0f;
					break;
				case DamageType.Poison:
					return 0.5f;
					break;
				case DamageType.Holy:
					return 0f;
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(type), type, null);
			}
		}

		private float RandomValue()
		{
			return Random.value;
		}
	}

	public enum DamageType
	{
		Water,
		Fire,
		Poison,
		Holy
	}
}