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
				a = new Partition ((int)(this.width * ratio), this.height, numSplits - 1, !dirToggle, offset);
				b = new Partition ((int)(this.width * (1 - ratio)), this.height, numSplits - 1, !dirToggle, offset + new Vector2(width * ratio, 0));
			} else {
				a = new Partition (this.width, (int)(this.height * ratio), numSplits - 1, !dirToggle, offset);
				b = new Partition (this.width, (int)(this.height * (1 - ratio)), numSplits - 1, !dirToggle, offset + new Vector2(0, height * ratio));
			}
		} else {
			if (width < 5 || height < 5) {
				topLeft = new Vector2 (0, 0);
				bottomRight = new Vector2 (width, height);
			} else {
				topLeft = new Vector2 (Random.Range (1, width / 3), Random.Range (1, height / 3));
				bottomRight = new Vector2 (width - Random.Range (1, width / 3), height - Random.Range (1, height / 3));
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
		BSPTree = new Partition (width, height, (int)Mathf.Log(Mathf.ClosestPowerOfTwo(numRooms), 2));
		List<Partition> rooms = BSPTree.recoverRooms ();

		foreach (var item in rooms) {
			for (int x = (int)item.topLeft.x; x < (int)item.bottomRight.x; x++) {
				for (int y = (int)item.topLeft.y; y < (int)item.bottomRight.y; y++) {
					passabilityGrid [x + (int)item.offset.x, y + (int)item.offset.y] = true;
				}
			}
		}



	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.black;
		if (passabilityGrid != null) {
			for (int x = 0; x < width; x++) {
				for (int y = 0; y < height; y++) {
					if (passabilityGrid [x, y]) {
						Gizmos.color = Color.black;
						Gizmos.DrawSphere (transform.TransformPoint(new Vector3 (x - (width/2), 0, y - (height/2))), 0.5f);
					} else {
						Gizmos.color = Color.red;
						Gizmos.DrawSphere (transform.TransformPoint(new Vector3 (x - (width/2), 10, y - (height/2))), 0.5f);
					}
				}
			}
		}
	}

}
