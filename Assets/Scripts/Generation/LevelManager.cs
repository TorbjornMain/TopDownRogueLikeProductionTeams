﻿using UnityEngine;
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
				min = spawnList.Count - 1;
				break;
			}
		}

		if (!cantSpawn) {
			while (spawnList [ind].levelClass <= maxDifficulty) {
				ind++; max++;
				if (ind >= spawnList.Count) {
					max = spawnList.Count - 1;
					break;
				}
			}
		}

		if (!cantSpawn) {
			for (int i = 0; i < numSpawns; i++) {
				int randItem = Random.Range (min, max + 1);
				int itemSize = (int)propPrefabs[randItem].size;
				int roomNumber = Random.Range (0, rooms.Count);
				int roomX = 0, roomY = 0;
				bool placeable = false;
				while (!placeable) {
					roomNumber = Random.Range (0, rooms.Count);
					roomX = (int)rooms [roomNumber].offset.x + Random.Range ((int)rooms [roomNumber].topLeft.x + 1, (int)rooms [roomNumber].bottomRight.x - itemSize);
					roomY = (int)rooms[roomNumber].offset.y + Random.Range ((int)rooms [roomNumber].topLeft.y + 1, (int)rooms [roomNumber].bottomRight.y - itemSize);
					int numPlaceable = 0;
					for (int x = 0; x < itemSize; x++) {
						for (int y = 0; y < itemSize; y++) {
							if (placeableGrid [roomX + x, roomY + y])
								numPlaceable++;
						}
					}
					if (numPlaceable == (itemSize * itemSize)) {
						placeable = true;
					}
				}
				for (int x = 0; x < itemSize; x++) {
					for (int y = 0; y < itemSize; y++) {
						placeableGrid [roomX + x, roomY + x] = false;
					}
				}

				outputList.Add(Instantiate<SpawnableObject> (spawnList [randItem]));
				outputList [outputList.Count - 1].transform.position = proceduralLevel.transform.position + (new Vector3 (roomX - (proceduralLevel.width/2) + itemSize/2, outputList[outputList.Count - 1].heightOffset, roomY- (proceduralLevel.height/2) + itemSize/2) * proceduralLevel.tileScale);
			}
		}
	}

}