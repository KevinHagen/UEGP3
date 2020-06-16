using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace UEGP3.Demos.PCGDemos
{
    public class SimpleRNGTest : MonoBehaviour
    {
        [Header("Generation")] [SerializeField] [Tooltip("Text that display our random number")]
        private TextMeshProUGUI _randomNumberText;
        [SerializeField] [Tooltip("Button to generate a new random number")]
        private Button _randomNumberButton;

        [Header("Seed & PRNG")] [SerializeField] [Tooltip("Seed used to generate a random number")]
        private int _seed;
        [SerializeField] [Tooltip("Button to re-init the prng with the configured seed")]
        private Button _initPRNGButton;

        private void Awake()
        {
            _randomNumberButton.onClick.AddListener(GenerateRandomNumber);
            _initPRNGButton.onClick.AddListener(ResetToSeed);
            ResetToSeed();
        }

        private void GenerateRandomNumber()
        {
            _randomNumberText.text = (Random.value * 10f).ToString("0.00");
        }

        private void ResetToSeed()
        {
            Random.InitState(_seed);
            GenerateRandomNumber();
        }
    }
}
