using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace UEGP3.LevelGenerationSystem.DW
{
	public class DrunkardsWalk : MonoBehaviour
	{
		[Header("Drunkards Walk")]
		[SerializeField] private GameBoardDW _gameBoard;
		[SerializeField] private Walker _walkerPrefab;
		[SerializeField] private bool _useSeed = true;
		[SerializeField] private int _seed;
		[Header("Animate")]
		[SerializeField] private bool _animateProcess;
		[SerializeField] private float _timeBetweenSteps;
		[Header("Levy Flight")] [SerializeField]
		private bool _enableLevyFlight;
		[SerializeField] [Range(0, 1)]
		private float _levyFlightChance;
		[SerializeField] private int _levyFlightStepSize = 8;
		
		private Random _rng;
		private List<Walker> _walkers = new List<Walker>();
		
		private void Awake()
		{
			InitRNG();
		}

		private void SpawnWalkers()
		{
			int xStart = _rng.Next(0, _gameBoard.Columns);
			int yStart = _rng.Next(0, _gameBoard.Rows);
			
			Walker instantiate = Instantiate(_walkerPrefab);
			instantiate.SetPosition(xStart, yStart);
			instantiate.transform.position = _gameBoard.GetPositionAt(xStart, yStart);

			_walkers.Add(instantiate);
		}

		private void InitRNG()
		{
			_rng = _useSeed ? new Random(_seed) : new Random();
		}

		private void Start()
		{
			SpawnWalkers();
			
			if (_animateProcess)
			{
				StartCoroutine(nameof(WalkCoroutine));
			}
			else
			{
				WalkInstant();
			}
		}

		private IEnumerator WalkCoroutine()
		{
			while (!_gameBoard.IsDone())
			{
				WalkSingleStep();
				yield return new WaitForSeconds(_timeBetweenSteps);
			}
		}

		private void WalkInstant()
		{
			while (!_gameBoard.IsDone())
			{
				WalkSingleStep();
			}
		}

		private void WalkSingleStep()
		{
			int x = _walkers[0].CurrentXPosition;
			int y = _walkers[0].CurrentYPosition;
			WalkDirection randomDirection;

			bool doLevyFlight = _enableLevyFlight && (_rng.NextDouble() <= _levyFlightChance);

			do
			{
				randomDirection = GetRandomMoveDirection();
			} while (!_gameBoard.IsInBoard(x, y, randomDirection, doLevyFlight ? _levyFlightStepSize : 1));

			_walkers[0].Walk(randomDirection, doLevyFlight ? _levyFlightStepSize : 1);
			_walkers[0].transform.position = _gameBoard.GetPositionAt(_walkers[0].CurrentXPosition, _walkers[0].CurrentYPosition);
			_gameBoard.MarkAsGround(_walkers[0].CurrentXPosition, _walkers[0].CurrentYPosition);
		}
		
		private WalkDirection GetRandomMoveDirection()
		{
			int randomDirection = _rng.Next(1, 5);
			switch (randomDirection)
			{
				case 1:
					return WalkDirection.North;
				case 2:
					return WalkDirection.South;
				case 3:
					return WalkDirection.East;
				case 4:
					return WalkDirection.West;
			}

			throw new ArgumentOutOfRangeException();
		}
	}
}