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

	public Partition (int width, int height, int numSplits, bool dirToggle = true)
	{
		this.width = width;
		this.height = height;
		ratio = Random.Range (0.3f, 0.7f);
		if (numSplits > 0) {
			if (dirToggle) {
				a = new Partition ((int)(this.width * ratio), this.height, numSplits - 1, !dirToggle);
				b = new Partition ((int)(this.width * (1 - ratio)), this.height, numSplits - 1, !dirToggle);
			} else {
				a = new Partition (this.width, (int)(this.height * ratio), numSplits - 1, !dirToggle);
				b = new Partition (this.width, (int)(this.height * (1 - ratio)), numSplits - 1, !dirToggle);
			}
		} else {
			if (width < 5 || height < 5) {
				topLeft = new Vector2 (0, 0);
				bottomRight = new Vector2 (width, height);
			} else {
				topLeft = new Vector2 (Random.Range (0, width / 3), Random.Range (0, height / 3));
				topLeft = new Vector2 (Random.Range (0, width / 3), Random.Range (0, height / 3));
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

	public int width = 100, height = 100, rooms = 10;

	public void Start()
	{
		generateLevel();
		List<Partition> p = BSPTree.recoverRooms ();
		Partition[] array = p.ToArray ();
	}

	public void generateLevel()
	{
		passabilityGrid = new bool[width, height];

		BSPTree = new Partition (width, height, Mathf.Log(Mathf.ClosestPowerOfTwo(rooms), 2));




	}

}
