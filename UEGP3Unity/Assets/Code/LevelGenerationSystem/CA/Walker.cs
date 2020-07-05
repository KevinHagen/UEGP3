using System;
using UnityEngine;

namespace UEGP3.LevelGenerationSystem.DW
{
	public class Walker : MonoBehaviour
	{
		private int _currentXPosition;
		private int _currentYPosition;
		public int CurrentXPosition => _currentXPosition;
		public int CurrentYPosition => _currentYPosition;
		
		public void SetPosition(int x, int y)
		{
			_currentXPosition = x;
			_currentYPosition = y;
		}
		
		public void Walk(WalkDirection direction, int stepSize)
		{
			switch (direction)
			{
				case WalkDirection.North:
					_currentYPosition += stepSize;
					break;
				case WalkDirection.South:
					_currentYPosition -= stepSize;
					break;
				case WalkDirection.East:
					_currentXPosition += stepSize;
					break;
				case WalkDirection.West:
					_currentXPosition -= stepSize;
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
			}
		}
	}
}