using UnityEngine;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

	public ProceduralLevelGenerator proceduralLevel;
	public GameObject playerPrefab;
	private GameObject _playerInstance;
	private BodySockets playerBody;
	public BodySockets playerInstance
	{
		get {
			return playerBody;
		}
	}
	public List<SpawnableObject> propPrefabs;
	public List<SpawnableObject> enemyPrefabs;
	public LevelUpPortal portalPrefab;
	private LevelUpPortal portalInstance;
	private List<SpawnableObject> enemyInstances;
	private List<SpawnableObject> propInstances;
	private List<Partition> rooms;
	private bool[,] placeableGrid;
	public uint maxDifficulty, minProps, minEnemies, maxProps, maxEnemies;
	bool first = true;


	// Use this for initialization
	void Start () {
		_playerInstance = Instantiate<GameObject> (playerPrefab);
		playerBody = _playerInstance.GetComponentInChildren<BodySockets> ();
		enemyInstances = new List<SpawnableObject>();
		propInstances = new List<SpawnableObject>();
		_playerInstance.SetActive(false);

	}
	
	// Update is called once per frame
	void Update () {
		if (first) {
			initializeLevel ();
		}
		first = false;


		if (enemyInstances.Count == 1 && (enemyInstances [0] == null || enemyInstances [0].Equals (null))) {
			RaycastHit rc = new RaycastHit();
			Vector3 portalPos;
			if (Physics.Raycast (playerInstance.transform.position, playerInstance.transform.forward, out rc, 20, LayerMask.NameToLayer ("Terrain"))) {
				portalPos = rc.point - playerInstance.transform.forward;
			} else {
				portalPos = playerInstance.transform.position + (playerInstance.transform.forward * 20);
			}
			portalInstance = Instantiate<LevelUpPortal> (portalPrefab);
			portalInstance.levelManager = this;
			portalInstance.transform.position = portalPos;
		}
		int destroyedIndex = 0;
		bool isDestroyed = false;

		for (int i = 0; i < enemyInstances.Count; i++) {
			if (enemyInstances [i] == null || enemyInstances[i].Equals(null)) {
				destroyedIndex = i;
				isDestroyed = true;
				break;
			}
		}

		if (isDestroyed) {
			enemyInstances.RemoveAt (destroyedIndex);
		}
	}

	void initializeLevel()
	{
		proceduralLevel.generateLevel ();

		rooms = proceduralLevel.BSPTree.recoverRooms ();
		placeableGrid = proceduralLevel.passabilityGrid;

		int roomNumber = Random.Range (0, rooms.Count);
		int roomX = (int)rooms[roomNumber].offset.x + Random.Range ((int)rooms [roomNumber].topLeft.x, (int)rooms [roomNumber].bottomRight.x), roomY = (int)rooms[roomNumber].offset.y + Random.Range ((int)rooms [roomNumber].topLeft.y, (int)rooms [roomNumber].bottomRight.y) ;

		placeableGrid [roomX, roomY] = false;
		_playerInstance.transform.position = proceduralLevel.transform.position + (new Vector3 (roomX - (proceduralLevel.width/2), 0, roomY- (proceduralLevel.height/2)) * proceduralLevel.tileScale);
		playerInstance.transform.position = proceduralLevel.transform.position + (new Vector3 (roomX - (proceduralLevel.width/2), playerInstance.transform.position.y, roomY- (proceduralLevel.height/2)) * proceduralLevel.tileScale);

		spawnFromList (enemyPrefabs, (uint)Random.Range (minEnemies, maxEnemies), ref enemyInstances);
		spawnFromList (propPrefabs, (uint)Random.Range (minProps, maxProps), ref propInstances);

		_playerInstance.SetActive (true);

	}

	public void transitionLevel()
	{
		_playerInstance.SetActive (false);
		maxDifficulty++;
		destroyLevel ();
		initializeLevel ();
	}

	public void destroyLevel()
	{
		foreach (var item in propInstances) {
			if (!(item == null)) {
				Destroy (item.gameObject);
			}
		}
		propInstances.Clear ();
		foreach (var item in enemyInstances) {
			if (!(item == null)) {
				Destroy (item.gameObject);
			}
		}
		enemyInstances.Clear ();
		Destroy (portalInstance.gameObject);
	}

	void spawnFromList(List<SpawnableObject> spawnList, uint numSpawns, ref List<SpawnableObject> outputList)
	{
		if (spawnList.Count == 0)
			return;
		
		spawnList.Sort ();

		bool cantSpawn = false;
		int ind = 0, max = 0;

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
				int randItem = Random.Range (0, max + 1);
				int itemWidth = (int)spawnList[randItem].width, itemHeight = (int)spawnList[randItem].height;
				int roomNumber = Random.Range (0, rooms.Count);
				int roomX = 0, roomY = 0;
				bool placeable = false;
				while (!placeable) {
					roomNumber = Random.Range (0, rooms.Count);
					roomX = (int)rooms [roomNumber].offset.x + Random.Range ((int)rooms [roomNumber].topLeft.x + 1, (int)rooms [roomNumber].bottomRight.x - itemWidth);
					roomY = (int)rooms[roomNumber].offset.y + Random.Range ((int)rooms [roomNumber].topLeft.y + 1, (int)rooms [roomNumber].bottomRight.y - itemHeight);
					int numPlaceable = 0;
					for (int x = 0; x < itemWidth; x++) {
						for (int y = 0; y < itemHeight; y++) {
							if (placeableGrid [roomX + x, roomY + y])
								numPlaceable++;
						}
					}
					if (numPlaceable == (itemWidth * itemHeight)) {
						placeable = true;
					}
				}
				for (int x = 0; x < itemWidth; x++) {
					for (int y = 0; y < itemHeight; y++) {
						placeableGrid [roomX + x, roomY + y] = false;
					}
				}

				outputList.Add(Instantiate<SpawnableObject> (spawnList [randItem]));
				outputList [outputList.Count - 1].transform.position = proceduralLevel.transform.position + (new Vector3 ((roomX - (proceduralLevel.width/2) + itemWidth/2) * proceduralLevel.tileScale, outputList[outputList.Count - 1].heightOffset, (roomY- (proceduralLevel.height/2) + itemHeight/2)* proceduralLevel.tileScale) );
				outputList [outputList.Count - 1].lm = this;
			}
		}
	}

}