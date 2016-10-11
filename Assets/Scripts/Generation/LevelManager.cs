using UnityEngine;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

	public ProceduralLevelGenerator proceduralLevel;
	public GameObject playerPrefab;
	private GameObject playerInstance;
	bool first = true;


	// Use this for initialization
	void Start () {
		playerInstance = Instantiate<GameObject> (playerPrefab);
		playerInstance.SetActive(false);

	}
	
	// Update is called once per frame
	void Update () {
		if (first) {
			initializeLevel ();
		}
		first = false;
	}

	void initializeLevel()
	{
		proceduralLevel.generateLevel ();
		List<Partition> rooms = proceduralLevel.BSPTree.recoverRooms ();
		bool[,] placeableGrid = proceduralLevel.passabilityGrid;

		int roomNumber = Random.Range (0, rooms.Count);
		int roomX = (int)rooms[roomNumber].offset.x + Random.Range ((int)rooms [roomNumber].topLeft.x, (int)rooms [roomNumber].bottomRight.x + 1), roomY = (int)rooms[roomNumber].offset.y + Random.Range ((int)rooms [roomNumber].topLeft.y, (int)rooms [roomNumber].bottomRight.y + 1) ;

		placeableGrid [roomX, roomY] = false;

		playerInstance.transform.position = proceduralLevel.transform.position + (new Vector3 (roomX - (proceduralLevel.width/2), 0, roomY- (proceduralLevel.height/2)) * proceduralLevel.tileScale);
		playerInstance.SetActive (true);

	}

}
