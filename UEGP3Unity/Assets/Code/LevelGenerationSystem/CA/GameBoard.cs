using UnityEngine;

namespace UEGP3.LevelGenerationSystem.CA
{
	public class GameBoard : MonoBehaviour
	{
		[SerializeField] [Tooltip("Width of the board")]
		private int _width;
		[SerializeField] [Tooltip("Height of the board")]
		private int _height;
		[SerializeField] [Tooltip("Prefab used to generate the whole board.")]
		private SpriteRenderer _groundPrefab;

		// 2d array to store all tiles on the gameboard
		private SpriteRenderer[,] _tiles;
		// example to show how a "jagged" array looks like
		private SpriteRenderer[][] _otherTiles;
		
		public int Width => _width;
		public int Height => _height;

		private void Awake()
		{
			SpawnMap();
		}

		private void SpawnMap()
		{
			_tiles = new SpriteRenderer[_width, _height];
			for (int x = 0; x < _width; x++)
			{
				for (int y = 0; y < _height; y++)
				{
					_tiles[x, y] = Instantiate(_groundPrefab, gameObject.transform);
					_tiles[x, y].transform.position = new Vector3(x, y);
					// All tiles start as walls, which are represented by the color black
					_tiles[x, y].color = Color.black;
				}
			}
			
			// Example usage of jagged arrays
			// _otherTiles = new SpriteRenderer[5][];
			// _otherTiles[1] = new SpriteRenderer[3];
			// _otherTiles[2] = new SpriteRenderer[10];
		}

		/// <summary>
		/// Takes a configuration of the map and plots the data onto the game board.
		/// Ground tiles are shown white, wall tiles are shown black.
		/// </summary>
		/// <param name="tileData">The underlying data of the map.</param>
		public void PlotData(bool[,] tileData)
		{
			for (int x = 0; x < _width; x++)
			{
				for (int y = 0; y < _height; y++)
				{
					_tiles[x, y].color = tileData[x, y] ? Color.white : Color.black;
				}
			}
		}
	}
}