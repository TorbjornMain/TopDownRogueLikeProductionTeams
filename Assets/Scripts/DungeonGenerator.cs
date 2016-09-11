using UnityEngine;
using System.Collections;

public enum TileState
{
	Open,
	Closed,
	Unmade,
	Connected
}


public class DungeonGenerator : MonoBehaviour {
	[System.NonSerialized]
	public TileState[,] maze; 

	public int roomAttempts = 30;

	public int maxRoomSize = 30, minRoomSize = 10;

	public int width = 200, height = 200;

	public GameObject wallPrefab;

	void Update()
	{
		if (Input.GetButtonDown ("Fire1")) {
			GenerateLevel ();
		}
	}

	public void GenerateLevel()
	{
		maze = new TileState[width, height];
		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				maze [x, y] = TileState.Unmade;
			}
		}

		placeRooms ();

		for (int x = 0; x < maze.GetLength (0); x++) {
			for (int y = 0; y < maze.GetLength (1); y++) {
				if (maze [x, y] == TileState.Open) {
					GameObject g = Instantiate<GameObject> (wallPrefab);
					g.transform.SetParent (transform);
					g.transform.localScale = new Vector3 (g.transform.localScale.x, 10, g.transform.localScale.z);
					g.transform.localPosition = new Vector3 (((-width / 2.0f) + x), 0, ((-height / 2.0f) + y));
				}
			}
		}

	}

	void placeRooms()
	{
		for (int i = 0; i < roomAttempts; i++) {
			int roomWidth = (int)(Random.value * (maxRoomSize - minRoomSize)) + minRoomSize;
			int roomHeight = (int)(Random.value * (maxRoomSize - minRoomSize)) + minRoomSize;

			int roomX = (int)((Random.value * ((width - 3) - roomWidth)) + 1);
			int roomY = (int)((Random.value * ((height - 3) - roomHeight)) + 1);

			bool attemptSuccess = true;

			for (int x = roomX; x < roomX + roomWidth; x++) {
				for (int y = roomY; y < roomY + roomHeight; y++) {
					if (maze [x, y] != TileState.Unmade) {
						attemptSuccess = false;
						break;
					}
				}
				if (!attemptSuccess)
					break;
			}

			if (attemptSuccess) {
				for (int x = roomX - 1; x < roomX + roomWidth + 1; x++) {
					for (int y = roomY - 1; y < roomY + roomHeight + 1; y++) {
						if (x == roomX - 1 || x == roomX + roomWidth || y == roomY - 1 || y == roomY + roomHeight) {
							maze [x, y] = TileState.Closed;
						} else {
							maze [x, y] = TileState.Open;
						}
					}
				}
			}
		}
	}

	struct IntVec2 
	{
		public int x = Random.value * width;
		public int y = Random.value * height;
	}

	void createMaze()
	{
		
		IntVec2 vec = new IntVec2();
	
		while (maze [vec.x, vec.y] != TileState.Unmade) {
			vec.x = Random.value * width;
			vec.y = Random.value * height;
		}



	}

}
