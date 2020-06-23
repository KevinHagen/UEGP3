using System;
using UnityEngine;
using Random = System.Random;

namespace UEGP3.LevelGenerationSystem.CA
{
	public class CellularAutomaton : MonoBehaviour
	{
		[Header("PRNG")] [SerializeField] [Tooltip("If set to true, the seed below will be used. Else it uses the default seed")]
		private bool _useSeed;
		[SerializeField] [Tooltip("Seed to use for the generation")]
		private int _seed;
		[Tooltip("Chance for a cell to start out alive")] [SerializeField] [Range(0, 1)]
		private float _startAliveChance;
		[SerializeField] [Tooltip("Reference to the gameboard which displays the result of the automaton")]
		private GameBoard _gameBoard;
		[SerializeField] [Tooltip("Number of generations used to smooth the CA result")] 
		private int _generations;
		private Random _rng;
		private int _currentGeneration;
		private bool[][,] _generationSteps;

		private void Start()
		{
			// Init the automaton
			InitAutomaton();
			// Start the generation process
			Generate();
		}

		private void Update()
		{
			// If LMB is pressed, generate a new start configuration
			if (Input.GetMouseButtonDown(0))
			{
				InitAutomaton();
				Generate();
			}
		}

		private void Generate()
		{
			// need to retrieve next generation
			CalculateNextGeneration();
		}

		private void CalculateNextGeneration()
		{
			bool[,] nextGeneration = new bool[_gameBoard.Width, _gameBoard.Height];

			for (int x = 1; x < _gameBoard.Width - 1; x++)
			{
				for (int y = 1; y < _gameBoard.Height - 1; y++)
				{
					int surroundWalls = GetSurroundingWallsCount(x, y);
					
					// is a wall and has >= 4 neighbouring walls --> wall
					
					// is ground and has >= 5 neighbouring walls --> wall
					
					// otherwise its a ground tile
				}
			}
			
			// after the rules have been successfully applied, we can increase current generation and safe the generation as the current one
			_currentGeneration++;
			_generationSteps[_currentGeneration] = nextGeneration;
		}

		private int GetSurroundingWallsCount(int x, int y)
		{
			throw new NotImplementedException();
		}

		private void InitAutomaton()
		{
			// store the current generation and set it to 0
			_currentGeneration = 0;
			
			// store current generation and its configuration
			_generationSteps = new bool[_generations + 1][,];
			for (int i = 0; i < _generationSteps.Length; i++)
			{
				_generationSteps[i] = new bool[_gameBoard.Width, _gameBoard.Height];
			}

			// init prng
			InitPRNG();

			// init random distribution
			InitRandomDistribution();
			_gameBoard.PlotData(_generationSteps[_currentGeneration]);
		}

		private void InitRandomDistribution()
		{
			for (int x = 0; x < _gameBoard.Width; x++)
			{
				for (int y = 0; y < _gameBoard.Height; y++)
				{
					// border tiles should be walls by default
					if ((x == 0) || (x == _gameBoard.Width - 1) || (y == 0) || (y == _gameBoard.Height - 1))
					{
						_generationSteps[0][x, y] = false;
					}
					// others should be randomly chosen
					else
					{
						_generationSteps[0][x, y] = _rng.NextDouble() < _startAliveChance;
					}
				}
			}
		}

		private void InitPRNG()
		{
			_rng = _useSeed ? new Random(_seed) : new Random();
		}
	}
}