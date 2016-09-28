using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Direction 
{
	Up = 0,
	Right = 1,
	Down = 2,
	Left = 3
}

public class Partition
{
	public int width, height;
	public Partition a, b;
	public float ratio;

	public Vector2 topLeft;
	public Vector2 bottomRight;
	public Vector2 offset;


	public Partition (int width, int height, int numSplits, bool dirToggle = true, Vector2 offset = new Vector2())
	{
		this.offset = offset;
		this.width = width;
		this.height = height;
		ratio = Random.Range (0.3f, 0.7f);
		if (numSplits > 0) {
			if (dirToggle) {
				a = new Partition (Mathf.RoundToInt(this.width * ratio), this.height, numSplits - 1, !dirToggle, offset);
				b = new Partition (Mathf.RoundToInt(this.width * (1 - ratio)), this.height, numSplits - 1, !dirToggle, offset + new Vector2(Mathf.RoundToInt(width * ratio), 0));
			} else {
				a = new Partition (this.width, Mathf.RoundToInt(this.height * ratio), numSplits - 1, !dirToggle, offset);
				b = new Partition (this.width, Mathf.RoundToInt(this.height * (1 - ratio)), numSplits - 1, !dirToggle, offset + new Vector2(0, Mathf.RoundToInt(height * ratio)));
			}
		} else {
			if (width < 5 || height < 5) {
				topLeft = new Vector2 (0, 0);
				bottomRight = new Vector2 (width, height);
			} else {
				topLeft = new Vector2 (Mathf.RoundToInt(Random.Range (1, width / 3)), Mathf.RoundToInt(Random.Range (1, height / 3)));
				bottomRight = new Vector2 (width - Mathf.RoundToInt(Random.Range (1, width / 3)), height - Mathf.RoundToInt(Random.Range (1, height / 3)));
			}
		}
	}

	public List<Partition> recoverRooms()
	{
		List<Partition> output = new List<Partition> ();
		if (a != null && b != null) {
			output = a.recoverRooms ();
			output.AddRange (b.recoverRooms ());
		} else {
			output.Add (this);
		}
		return output;
	}

}

public class ProceduralLevelGenerator : MonoBehaviour {

	private bool[,] passabilityGrid;

	private Partition BSPTree;

	public int width = 100, height = 100, numRooms = 10;

	public void Start()
	{
		generateLevel();
	}

	public void generateLevel()
	{
		passabilityGrid = new bool[width, height];
		BSPTree = new Partition (width, height, Mathf.RoundToInt(Mathf.Log(Mathf.ClosestPowerOfTwo(numRooms), 2)));
		List<Partition> rooms = BSPTree.recoverRooms ();

		foreach (var item in rooms) {
			for (int x = (int)item.topLeft.x; x < (int)item.bottomRight.x; x++) {
				for (int y = (int)item.topLeft.y; y < (int)item.bottomRight.y; y++) {
					passabilityGrid [x + (int)item.offset.x, y + (int)item.offset.y] = true;
				}
			}
		}


		generateCorridors (ref rooms);


	}

	void generateCorridors(ref List<Partition> rooms)
	{
		bool keepRooming = false;
		while (!keepRooming) {
			int roomA = (int)(Random.value * rooms.Count), roomB = (int)(Random.value * rooms.Count);
			while (roomA == roomB) {
				roomA = (int)(Random.value * rooms.Count); roomB = (int)(Random.value * rooms.Count);
			}
			Vector2 roomOffset = rooms [roomB].offset - rooms [roomA].offset, pathA, pathB, pathStep, stepSign;
			if (Mathf.Abs (roomOffset.x) > Mathf.Abs (roomOffset.y)) {
				if (roomOffset.x > 0) {
					pathA = rooms[roomA].offset + new Vector2 (rooms [roomA].bottomRight.x, Mathf.Round(Random.Range (rooms [roomA].topLeft.y, rooms [roomA].bottomRight.y)));
					pathB = rooms[roomB].offset + new Vector2 (rooms [roomB].topLeft.x, Mathf.Round(Random.Range (rooms [roomB].topLeft.y, rooms [roomB].bottomRight.y)));
				} else {
					pathA = rooms[roomA].offset + new Vector2 (rooms [roomA].topLeft.x, Mathf.Round(Random.Range (rooms [roomA].topLeft.y, rooms [roomA].bottomRight.y)));
					pathB = rooms[roomB].offset + new Vector2 (rooms [roomB].bottomRight.x, Mathf.Round(Random.Range (rooms [roomB].topLeft.y, rooms [roomB].bottomRight.y)));
				}

				pathStep = pathB - pathA;
				stepSign = new Vector2 (Mathf.Sign (pathStep.x), Mathf.Sign (pathStep.y));

				for(int i = 0; i < Mathf.RoundToInt(Mathf.Abs(pathStep.x / 2)); i++)
				{
					passabilityGrid [Mathf.RoundToInt(pathA.x + (i * stepSign.x)), Mathf.RoundToInt(pathA.y)] = true;
					passabilityGrid [Mathf.RoundToInt(pathA.x + (pathStep.x/2) + (i * stepSign.x)), Mathf.RoundToInt(pathB.y)] = true;
				}
				for (int i = 0; i < Mathf.RoundToInt(Mathf.Abs(pathStep.y)); i++) {
					passabilityGrid [Mathf.RoundToInt((pathStep.x / 2) + pathA.x), Mathf.RoundToInt(pathA.y + (i * stepSign.y))] = true; 
				}
			} else {
				if (roomOffset.y > 0) {
					pathA = rooms[roomA].offset + new Vector2 (Mathf.Round(Random.Range (rooms [roomA].topLeft.x, rooms [roomA].bottomRight.x)), rooms [roomA].bottomRight.y);
					pathB = rooms[roomB].offset + new Vector2 (Mathf.Round(Random.Range (rooms [roomB].topLeft.x, rooms [roomB].bottomRight.x)), rooms [roomB].topLeft.y);
				} else {
					pathA = rooms[roomA].offset + new Vector2 (Mathf.Round(Random.Range (rooms [roomA].topLeft.x, rooms [roomA].bottomRight.x)), rooms [roomA].topLeft.y);
					pathB = rooms[roomB].offset + new Vector2 (Mathf.Round(Random.Range (rooms [roomB].topLeft.x, rooms [roomB].bottomRight.x)), rooms [roomB].bottomRight.y);
				}

				pathStep = pathB - pathA;
				stepSign = new Vector2 (Mathf.Sign (pathStep.x), Mathf.Sign (pathStep.y));

				for(int i = 0; i < Mathf.RoundToInt(Mathf.Abs(pathStep.y / 2)); i++)
				{
					passabilityGrid [Mathf.RoundToInt(pathA.x), Mathf.RoundToInt(pathA.y + (i * stepSign.y))] = true; 
					passabilityGrid [Mathf.RoundToInt(pathB.x), Mathf.RoundToInt(pathA.y + (pathStep.y / 2) + (stepSign.y * i))] = true; 
				}
				for (int i = 0; i < Mathf.RoundToInt(Mathf.Abs(pathStep.x)); i++) {
					passabilityGrid [Mathf.RoundToInt(pathA.x + (stepSign.x * i)), Mathf.RoundToInt(pathA.y + (pathStep.y/2))] = true;
				}
			}

			prune ();

			if (floodFillCheck () == passableCount ()) {
				keepRooming = !keepRooming;
			}


		}
		for (int i = 0; i < 20; i++) {
			prune ();
		}
	}

	void prune()
	{
		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				if (passabilityGrid [x, y] == true) {
					if (checkNeighbours (new Vector2 (x, y)) < 2) {
						passabilityGrid [x, y] = false;
					}
				}
			}
		}
	}

	int checkNeighbours(Vector2 v)
	{
		int output = 0;
		for (int x = -1; x <= 1; x++) {
			for (int y = -1; y <= 1; y++) {
				Vector2 scanVec = v + new Vector2 (x, y);
				if (scanVec.x < 0 || scanVec.x >= width || scanVec.y < 0 || scanVec.y >= height || (x == 0 && y == 0))
					continue;
				else if (passabilityGrid [(int)scanVec.x, (int)scanVec.y])
					output++;
			}
		}
		return output;
	}

	int floodFillCheck()
	{
		int output = 0;
		bool[,] checkedGrid = new bool[width, height];
		int randomX = 0, randomY = 0;
		while (!passabilityGrid [randomX, randomY]) {
			randomX = Mathf.RoundToInt (Random.value * (width - 1));
			randomY = Mathf.RoundToInt (Random.value * (height - 1));
		}
		Vector2 xy = new Vector2 (randomX, randomY);
		Stack<Vector2> s = new Stack<Vector2>();
		s.Push (xy);
		while (s.Count > 0) {
			xy = s.Pop ();
			if (!checkedGrid [(int)xy.x, (int)xy.y] && passabilityGrid [(int)xy.x, (int)xy.y]) {
				checkedGrid [(int)xy.x, (int)xy.y] = true;
				output++;
				if (xy.x < width - 1)
					s.Push (new Vector2 (1, 0) + xy);
				if (xy.x > 0)
					s.Push (new Vector2 (-1, 0) + xy);
				if (xy.y < height - 1)
					s.Push (new Vector2 (0, 1) + xy);
				if (xy.y > 0)
					s.Push (new Vector2 (0, -1) + xy);
			}
		}


		return output;
	}

	int passableCount()
	{
		int output = 0;
		foreach (var item in passabilityGrid) {
			if (item)
				output++;
		}
		return output;
	}


	void OnDrawGizmos()
	{
		Gizmos.color = Color.black;
		if (passabilityGrid != null) {
			for (int x = 0; x < width; x++) {
				for (int y = 0; y < height; y++) {
					if (passabilityGrid [x, y]) {
						Gizmos.color = Color.blue;
						Gizmos.DrawSphere (transform.TransformPoint (new Vector3 (x - (width / 2), 0, y - (height / 2))), 0.5f);
					}
				}
			}
		}
	}

}
