using UnityEngine;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

	public ProceduralLevelGenerator proceduralLevel;
	public GameObject playerPrefab;
	private GameObject playerInstance;
	public List<SpawnableObject> propPrefabs;
	public List<SpawnableObject> enemyPrefabs;
	private List<SpawnableObject> enemyInstances;
	private List<SpawnableObject> propInstances;
	private List<Partition> rooms;
	private bool[,] placeableGrid;
	public uint maxDifficulty, minDifficulty, minProps, minEnemies, maxProps, maxEnemies;
	bool first = true;


	// Use this for initialization
	void Start () {
		playerInstance = Instantiate<GameObject> (playerPrefab);
		enemyInstances = new List<SpawnableObject>();
		propInstances = new List<SpawnableObject>();
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

		rooms = proceduralLevel.BSPTree.recoverRooms ();
		placeableGrid = proceduralLevel.passabilityGrid;

		int roomNumber = Random.Range (0, rooms.Count);
		int roomX = (int)rooms[roomNumber].offset.x + Random.Range ((int)rooms [roomNumber].topLeft.x, (int)rooms [roomNumber].bottomRight.x), roomY = (int)rooms[roomNumber].offset.y + Random.Range ((int)rooms [roomNumber].topLeft.y, (int)rooms [roomNumber].bottomRight.y) ;

		placeableGrid [roomX, roomY] = false;
		playerInstance.transform.position = proceduralLevel.transform.position + (new Vector3 (roomX - (proceduralLevel.width/2), 0, roomY- (proceduralLevel.height/2)) * proceduralLevel.tileScale);

		spawnFromList (enemyPrefabs, (uint)Random.Range (minEnemies, maxEnemies), ref enemyInstances);
		spawnFromList (propPrefabs, (uint)Random.Range (minProps, maxProps), ref propInstances);

		playerInstance.SetActive (true);

	}

	void spawnFromList(List<SpawnableObject> spawnList, uint numSpawns, ref List<SpawnableObject> outputList)
	{
		
		spawnList.Sort ();

		bool cantSpawn = false;
		int ind = 0, min = 0, max = 0;
		while (spawnList [ind].levelClass < minDifficulty) {
			ind++; min++; max++;
			if (ind >= spawnList.Count) {
				cantSpawn = true;
			}
		}

		if (!cantSpawn) {
			while (spawnList [ind].levelClass < maxDifficulty) {
				ind++; max++;
				if (ind >= spawnList.Count) {
					cantSpawn = true;
				}
			}
		}

		if (!cantSpawn) {
			for (int i = 0; i < numSpawns; i++) {
				int randItem = Random.Range (min, max + 1);
				int roomNumber = Random.Range (0, rooms.Count);
				int roomX = (int)rooms[roomNumber].offset.x + Random.Range ((int)rooms [roomNumber].topLeft.x, (int)rooms [roomNumber].bottomRight.x), roomY = (int)rooms[roomNumber].offset.y + Random.Range ((int)rooms [roomNumber].topLeft.y, (int)rooms [roomNumber].bottomRight.y) ;
				while (!placeableGrid [roomX, roomY]) {
					roomNumber = Random.Range (0, rooms.Count);
					roomX = (int)rooms [roomNumber].offset.x + Random.Range ((int)rooms [roomNumber].topLeft.x, (int)rooms [roomNumber].bottomRight.x);
					roomY = (int)rooms[roomNumber].offset.y + Random.Range ((int)rooms [roomNumber].topLeft.y, (int)rooms [roomNumber].bottomRight.y);
				}

				placeableGrid [roomX, roomY] = false;
				outputList.Add(Instantiate<SpawnableObject> (spawnList [randItem]));
				outputList [outputList.Count - 1].transform.position = proceduralLevel.transform.position + (new Vector3 (roomX - (proceduralLevel.width/2), outputList[outputList.Count - 1].heightOffset, roomY- (proceduralLevel.height/2)) * proceduralLevel.tileScale);
			}
		}
	}

}