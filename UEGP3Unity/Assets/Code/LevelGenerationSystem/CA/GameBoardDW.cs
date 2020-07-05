using UnityEngine;

namespace UEGP3.LevelGenerationSystem.DW
{
	public class GameBoardDW : MonoBehaviour
	{
		[SerializeField] private int _rows;
		[SerializeField] private int _columns;
		[SerializeField] [Range(0, 1)] private float _targetCarveRate;
		[SerializeField] private SpriteRenderer _wallPrefab;
		[SerializeField] private Transform _levelHolder;
		[SerializeField] private Color _wallColor = Color.black;
		[SerializeField] private Color _groundColor = Color.white;
		
		private SpriteRenderer[,] _tiles;
		private int _groundTiles;
		private float _currentCarveRate => _groundTiles / (float) (_rows * _columns);
		public int Rows => _rows;
		public int Columns => _columns;

		private void Awake()
		{
			GenerateBoard();
		}

		private void GenerateBoard()
		{
			_tiles = new SpriteRenderer[_columns, _rows];
			
			for (int x = 0; x < _columns; x++)
			{
				for (int y = 0; y < _rows; y++)
				{
					SpriteRenderer newTile = Instantiate(_wallPrefab, _levelHolder);
					newTile.transform.position = new Vector3(x, y, 0);
					newTile.color = _wallColor;
					_tiles[x, y] = newTile;
				}
			}
		}

		public Vector3 GetPositionAt(int x, int y)
		{
			return _tiles[x, y].transform.position;
		}
		
		public void MarkAsGround(int x, int y)
		{
			// Cant colorize if already ground
			if (_tiles[x, y].color.Equals(_groundColor))
			{
				return;
			}
			
			_tiles[x, y].color = _groundColor;
			_groundTiles++;
		}

		public bool IsDone()
		{
			return _currentCarveRate >= _targetCarveRate;
		}
		
		public bool IsInBoard(int x, int y, WalkDirection randomDirection, int stepSize)
		{
			switch (randomDirection)
			{
				case WalkDirection.None:
					break;
				case WalkDirection.North:
					y += stepSize;
					break;
				case WalkDirection.South:
					y -= stepSize;
					break;
				case WalkDirection.East:
					x += stepSize;
					break;
				case WalkDirection.West:
					x -= stepSize;
					break;
			}
			
			return (x >= 0) && (x < _columns) && (y >= 0) && (y < _rows);
		}
	}
}