using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UEGP3.Demos.PCGDemos.RNGTests
{
	public class SimpleRNGTestSystem : MonoBehaviour
	{
		[Header("Generation")]
		[SerializeField]
		[Tooltip("Text that displays our random number")]
		private TextMeshProUGUI _randomNumberText;
		[SerializeField] [Tooltip("Button to generate a new random number")]
		Button _generateNumberButton;

		[Header("Seed & PRNG")]
		[SerializeField] 
		[Tooltip("Seed used to generate a random number")]
		private int _seed;
		[SerializeField] [Tooltip("Button used to re-init the PRNG with the configured value")]
		private Button _initPRNGButton;

		private System.Random _randomNumberGenerator;
		
		private void Awake()
		{
			_generateNumberButton.onClick.AddListener(GenerateRandomNumber);
			_initPRNGButton.onClick.AddListener(ResetToSeed);
			ResetToSeed();
		}

		private void GenerateRandomNumber()
		{
			_randomNumberText.text = (_randomNumberGenerator.NextDouble() * 10).ToString("0.00");
		}

		private void ResetToSeed()
		{
			_randomNumberGenerator = new System.Random(_seed);
			GenerateRandomNumber();
		}
	}
}