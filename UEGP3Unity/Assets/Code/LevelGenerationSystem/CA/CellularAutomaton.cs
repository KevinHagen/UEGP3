using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
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
		[SerializeField] [Tooltip("If set true, the generation process is being animated")]
		private bool _animate;
		[SerializeField] [Tooltip("Time it takes to get from one generation to the next in seconds")]
		private float _timeBetweenGenerations;

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

		private void Generate()
		{
			if (_animate)
			{
				StartCoroutine(nameof(GenerateStepByStep));
			}
			else
			{
				GenerateInstantly();
			}
		}

		private IEnumerator GenerateStepByStep()
		{
			for (int i = 0; i < _generations; i++)
			{
				_gameBoard.PlotData(_generationSteps[_currentGeneration]);
				CalculateNextGeneration();
				yield return new WaitForSeconds(_timeBetweenGenerations);
			}
		}
		
		private void GenerateInstantly()
		{
			// need to retrieve next generation
			for (int i = 0; i < _generations; i++)
			{
				CalculateNextGeneration();
			}

			_gameBoard.PlotData(_generationSteps[_currentGeneration]);
		}

		private void CalculateNextGeneration()
		{
			// first generate new array to hold the next generation, as we may not override the current one before all changes are done
			bool[,] nextGeneration = new bool[_gameBoard.Width, _gameBoard.Height];

			// next check the 4-5-rule
			for (int x = 1; x < _gameBoard.Width - 1; x++)
			{
				for (int y = 1; y < _gameBoard.Height - 1; y++)
				{
					int surroundingWallsCount = GetSurroundingWallsCount(x, y);
					bool isGround = _generationSteps[_currentGeneration][x, y];
					
					// is a wall and has >= 4 neighbouring walls --> wall
					if (!isGround && (surroundingWallsCount >= 4))
					{
						nextGeneration[x, y] = false;
					}
					// is ground and has >= 5 neighbouring walls --> wall
					else if (isGround && (surroundingWallsCount >= 5))
					{
						nextGeneration[x, y] = false;
					}
					// otherwise its a ground tile
					else
					{
						nextGeneration[x, y] = true;
					}
				}
			}
			
			// after the rules have been successfully applied, we can increase current generation and safe the generation as the current one
			_currentGeneration++;
			_generationSteps[_currentGeneration] = nextGeneration;
		}

		private int GetSurroundingWallsCount(int x, int y)
		{
			// Moore-rule counts all eight surrounding tiles
			int surroundingWallsCount = 0;
			for (int neighbourX = x - 1; neighbourX <= x + 1; neighbourX++)
			{
				for (int neighbourY = y - 1; neighbourY <= y + 1; neighbourY++)
				{
					if ((neighbourX == x) && (neighbourY == y))
					{
						continue;
					}
			
					if (!_generationSteps[_currentGeneration][neighbourX, neighbourY])
					{
						surroundingWallsCount++;
					}
				}
			}
			
			return surroundingWallsCount;
			
			// Alternatively you can use this approach. Both are completely fine and you should pick the one thats more readable to you!
			// bool[] surroundingTilesStates =
			// {
			// 	_generationSteps[_currentGeneration][x - 1, y - 1],
			// 	_generationSteps[_currentGeneration][x - 1, y + 0],
			// 	_generationSteps[_currentGeneration][x - 1, y + 1],
			// 	_generationSteps[_currentGeneration][x + 0, y - 1],
			// 	_generationSteps[_currentGeneration][x + 0, y + 1],
			// 	_generationSteps[_currentGeneration][x + 1, y - 1],
			// 	_generationSteps[_currentGeneration][x + 1, y + 0],
			// 	_generationSteps[_currentGeneration][x + 1, y + 1],
			// };
			//
			// // LINQ => Language Integrated Query Language
			// return surroundingTilesStates.Count(isGround => !isGround);
		}
	}
}