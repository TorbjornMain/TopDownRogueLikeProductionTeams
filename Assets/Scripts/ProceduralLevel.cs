using UnityEngine;
using System.Collections;

public struct Tile
{
	public bool passable;
}

public class ProceduralLevel : MonoBehaviour {
	[SerializeField]
	private int width = 100, height = 100;
	[SerializeField]
	private float tileSize = 10, wallYOffset = 0.5f;
	public Tile[,] tileGrid;
	public float initialPassablePercentage = 0.5f;
	public int iterations = 5, starveLimit = 4, birthLimit = 4;
	public GameObject wallPrefab;

	void Start()
	{
		tileGrid = new Tile[width, height];
	}

	void Update()
	{
		if (Input.GetButtonDown ("Fire1")) {
			generateLevel ();
		}
	}

	public void destroyLevel()
	{
		foreach (Transform child in transform) {
			Destroy(child.gameObject);
		}
		for (int x = 0; x < tileGrid.GetLength (0); x++) {
			for (int y = 0; y < tileGrid.GetLength (1); y++) {
				tileGrid [x, y].passable = true;
			}
		}
	}

	public void generateLevel()
	{
		destroyLevel();
		for (int x = 0; x < tileGrid.GetLength (0); x++) {
			for (int y = 0; y < tileGrid.GetLength (1); y++) {
				if (Random.value < initialPassablePercentage) {
					tileGrid [x, y].passable = true;
				} else {
					tileGrid [x, y].passable = false;
				}
			}
		}
		Tile[,] tempGrid = new Tile[width, height];
		for (int iter = 0; iter <= iterations; iter++) {
			for (int x = 0; x < tileGrid.GetLength (0); x++) {
				for (int y = 0; y < tileGrid.GetLength (1); y++) {
					int neighboursBlocked = 0;
					for (int xOff = Mathf.Max (0, x - 1); xOff < Mathf.Min (width - 1, x + 1); xOff++) {
						for (int yOff = Mathf.Max (0, y - 1); yOff < Mathf.Min (height - 1, y + 1); yOff++) {
							if (!tileGrid [xOff, yOff].passable) {
								if (xOff != x && yOff != y) {
									neighboursBlocked++;
								}
							}
						}
					}
					if (tileGrid [x, y].passable) {
						if (neighboursBlocked > birthLimit) {
							tempGrid [x, y].passable = true;
						} else {
							tempGrid [x, y].passable = false;

						}
					} else {
						if (neighboursBlocked < starveLimit) {
							tempGrid [x, y].passable = true;
						} else {
							tempGrid [x, y].passable = false;
						}
					}
				}
			}
			tileGrid = (Tile[,])tempGrid.Clone();
		}

		for (int x = 0; x < tileGrid.GetLength (0); x++) {
			for (int y = 0; y < tileGrid.GetLength (1); y++) {
				if (!tileGrid [x, y].passable) {
					GameObject g = Instantiate<GameObject> (wallPrefab);
					g.transform.SetParent (transform);
					g.transform.localScale = new Vector3(g.transform.localScale.x * tileSize, 10, g.transform.localScale.z * tileSize);
					g.transform.localPosition = new Vector3 (((-width / 2.0f) + x) * tileSize, wallYOffset, ((-height / 2.0f) + y) * tileSize);
				}
			}
		}

	}
}
