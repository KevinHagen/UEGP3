using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace UEGP3.Demos.Rider
{
	[RequireComponent(typeof(Camera))]
	public class SomeRiderDemoClass : MonoBehaviour
	{
		[SerializeField] private float _speed;
		[SerializeField] private Vector3 _explosionForce;

		public Color AColor = new Color(0.75f, 0.12f, 0.13f);
		public Vector3 ExplosionForce => this._explosionForce;

		public void ADemoMethodWithOneParameter(float health)
		{
		}

		private List<HealthBar> _healthBars = new List<HealthBar>();
		private IEnumerator PlayHealFX(ParticleSystem particles)
		{
			if (particles == null)
			{
				yield break;
			}
			
			particles.Play();
			
			while (_currentHealth < _maxHealth)
			{
				_currentHealth += Time.deltaTime;
				yield return null;
			}
			foreach (HealthBar healthBar in _healthBars)
			{
				healthBar.Refill();
			}
		}
		
		private void SmartDotSemicolonDemoUsingStringBuilder()
		{
			StringBuilder sb = new StringBuilder();

			sb.AppendLine().AppendLine().AppendLine().AppendLine();
		}

		private float _currentHealth;
		
		public float GetCurrentHealth()
		{
			return _currentHealth;
		}

		private float _maxHealth;
		public float CurrentHealthNormalized
		{
			get
			{
				if (_maxHealth <= 0)
				{
					Debug.LogError("Cant divide by zero!");
					throw new DivideByZeroException();
				}
				return _currentHealth / _maxHealth;
			}
		}

		// whether or not the enemy can fly
		private bool _canEnemyFly;
	}

	public class HealthBar
	{
		public void Refill()
		{
		}
	}
}