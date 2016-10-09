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

public class Partition //Class that recusrively generates a BSP tree, terminating in leaf nodes that define rooms
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

		ratio = Random.Range (0.3f, 0.7f); //The ratio that the space is split by


		if (numSplits > 0) { //If the recursion is incomplete
			if (dirToggle) { //If we want to split the space horizontally
				a = new Partition (Mathf.RoundToInt(this.width * ratio), this.height, numSplits - 1, !dirToggle, offset);
				b = new Partition (Mathf.RoundToInt(this.width * (1 - ratio)), this.height, numSplits - 1, !dirToggle, offset + new Vector2(Mathf.RoundToInt(width * ratio), 0));
			} else { //If we want to split the space vertically
				a = new Partition (this.width, Mathf.RoundToInt(this.height * ratio), numSplits - 1, !dirToggle, offset);
				b = new Partition (this.width, Mathf.RoundToInt(this.height * (1 - ratio)), numSplits - 1, !dirToggle, offset + new Vector2(0, Mathf.RoundToInt(height * ratio)));
			}
		} else { //if the recursion is complete
			if (width < 5 || height < 5) { //if the space for the room is too small to make a room smaller than the overall space
				//set the room boundaries
				topLeft = new Vector2 (0, 0); 
				bottomRight = new Vector2 (width, height);
			} else { //if the space for the room is big enough to make a room smaller than the overall space
				//set the room boundaries
				topLeft = new Vector2 (Mathf.RoundToInt(Random.Range (1, width / 3)), Mathf.RoundToInt(Random.Range (1, height / 3)));
				bottomRight = new Vector2 (width - Mathf.RoundToInt(Random.Range (1, width / 3)), height - Mathf.RoundToInt(Random.Range (1, height / 3)));
			}
		}
	}

	public List<Partition> recoverRooms() //finds all leaf nodes of the bsp tree and returns them as a list
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


[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
[DisallowMultipleComponent]
public class ProceduralLevelGenerator : MonoBehaviour {

	private bool[,] passabilityGrid;

	private Partition BSPTree;

	public int width = 100, height = 100, numRooms = 8, wallHeight = 1;
	public float tileScale = 1;

	private MeshFilter mf;
	private MeshCollider mc;

	public void Start()
	{
		mf = GetComponent<MeshFilter> ();
		mc = GetComponent <MeshCollider> ();
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
		generateMesh ();

	}

	void generateMesh()
	{
		Mesh m = new Mesh ();
		//TODO: GENERATE A MESH
		List<Vector3> verticies = generateVerticies();
		int[] indicies = generateIndicies();
		m.SetVertices (verticies);
		m.SetTriangles (indicies, 0);
		m.RecalculateNormals ();
		mf.mesh = m;
		mc.sharedMesh = m;
	}

	int[] generateIndicies()
	{
		List<int> l = new List<int> ();
		for (int x = 0; x < (width * 2) - 1; x++) {
			for (int y = 0; y < (height * 2) - 1; y++) {
				l.Add ((x) + ((y) * width * 2));
				l.Add ((x + 1) + ((y) * width * 2));
				l.Add ((x) + ((y + 1) * width * 2));
				l.Add ((x + 1) + ((y) * width * 2));
				l.Add ((x + 1) + ((y + 1) * width * 2));
				l.Add ((x) + ((y + 1) * width * 2));
			}
		}
		return l.ToArray ();
	}

	List<Vector3> generateVerticies()
	{
		bool[,] pg = doublePassabilityGrid();
		List<Vector3> l = new List<Vector3>();
		for (int x = 0; x < width * 2; x++) {
			for (int y = 0; y < height * 2; y++) {
				if ( pg [x, y]) {
					l.Add(new Vector3((x - width) * tileScale/2, 0, (y - height) * tileScale/2));
				} else {
					l.Add(new Vector3((x - width) * tileScale/2, wallHeight, (y - height) * tileScale/2));
				}
			}
		}
		return l;
	}

	bool[,] doublePassabilityGrid()
	{
		bool[,] output = new bool[width * 2, height * 2];
		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				output [(x * 2), (y * 2)] = passabilityGrid [x, y];
				output [(x * 2) + 1, (y * 2)] = passabilityGrid [x, y];
				output [(x * 2) + 1, (y * 2) + 1] = passabilityGrid [x, y];
				output [(x * 2), (y * 2) + 1] = passabilityGrid [x, y];
			}
		}
		return output;
	}

	void generateCorridors(ref List<Partition> rooms) //Generates the corridors (please dont touch this its extremely messy)
	{
		bool keepRooming = false;
		while (!keepRooming) {
			int roomA = Random.Range (0, rooms.Count), roomB = Random.Range (0, rooms.Count); //picks random rooms
			while ((roomA == roomB)) { //ensures rooms arent the same
				roomB = Random.Range (0, rooms.Count);
			}
			//defining temp variables
			Vector2 roomOffset = rooms [roomB].offset - rooms [roomA].offset, pathA, pathB, pathStep, stepSign;

			//checking wether a vertical or horizontal path is required
			if (Mathf.Abs (roomOffset.x) > Mathf.Abs (roomOffset.y)) {
				// finds path offsets
				if (roomOffset.x > 0) {
					pathA = rooms[roomA].offset + new Vector2 (rooms [roomA].bottomRight.x, Mathf.Round(Random.Range (rooms [roomA].topLeft.y, rooms [roomA].bottomRight.y)));
					pathB = rooms[roomB].offset + new Vector2 (rooms [roomB].topLeft.x, Mathf.Round(Random.Range (rooms [roomB].topLeft.y, rooms [roomB].bottomRight.y)));
				} else {
					pathA = rooms[roomA].offset + new Vector2 (rooms [roomA].topLeft.x, Mathf.Round(Random.Range (rooms [roomA].topLeft.y, rooms [roomA].bottomRight.y)));
					pathB = rooms[roomB].offset + new Vector2 (rooms [roomB].bottomRight.x, Mathf.Round(Random.Range (rooms [roomB].topLeft.y, rooms [roomB].bottomRight.y)));
				}
				//calculates the path step (the distance along the x and y axes that the path needs to traverse)
				pathStep = pathB - pathA;

				//determines wether the offsets are positive or negative, to be used in for loops
				stepSign = new Vector2 (Mathf.Sign (pathStep.x), Mathf.Sign (pathStep.y));


				//creates path in the passability grid
				for(int i = 0; i < Mathf.RoundToInt(Mathf.Abs(pathStep.x / 2)); i++)
				{
					passabilityGrid [Mathf.RoundToInt(pathA.x + (i * stepSign.x)), Mathf.RoundToInt(pathA.y)] = true;
					passabilityGrid [Mathf.RoundToInt(pathA.x + (pathStep.x/2) + (i * stepSign.x)), Mathf.RoundToInt(pathB.y)] = true;
				}
				for (int i = 0; i < Mathf.RoundToInt(Mathf.Abs(pathStep.y)); i++) {
					passabilityGrid [Mathf.RoundToInt((pathStep.x / 2) + pathA.x), Mathf.RoundToInt(pathA.y + (i * stepSign.y))] = true; 
				}
			} else {
				//finds path offsets
				if (roomOffset.y > 0) {
					pathA = rooms[roomA].offset + new Vector2 (Mathf.Round(Random.Range (rooms [roomA].topLeft.x, rooms [roomA].bottomRight.x)), rooms [roomA].bottomRight.y);
					pathB = rooms[roomB].offset + new Vector2 (Mathf.Round(Random.Range (rooms [roomB].topLeft.x, rooms [roomB].bottomRight.x)), rooms [roomB].topLeft.y);
				} else {
					pathA = rooms[roomA].offset + new Vector2 (Mathf.Round(Random.Range (rooms [roomA].topLeft.x, rooms [roomA].bottomRight.x)), rooms [roomA].topLeft.y);
					pathB = rooms[roomB].offset + new Vector2 (Mathf.Round(Random.Range (rooms [roomB].topLeft.x, rooms [roomB].bottomRight.x)), rooms [roomB].bottomRight.y);
				}

				//determines path step and stepsign as above
				pathStep = pathB - pathA;
				stepSign = new Vector2 (Mathf.Sign (pathStep.x), Mathf.Sign (pathStep.y));

				//generates path in passability grid
				for(int i = 0; i < Mathf.RoundToInt(Mathf.Abs(pathStep.y / 2)); i++)
				{
					passabilityGrid [Mathf.RoundToInt(pathA.x), Mathf.RoundToInt(pathA.y + (i * stepSign.y))] = true; 
					passabilityGrid [Mathf.RoundToInt(pathB.x), Mathf.RoundToInt(pathA.y + (pathStep.y / 2) + (stepSign.y * i))] = true; 
				}
				for (int i = 0; i < Mathf.RoundToInt(Mathf.Abs(pathStep.x)); i++) {
					passabilityGrid [Mathf.RoundToInt(pathA.x + (stepSign.x * i)), Mathf.RoundToInt(pathA.y + (pathStep.y/2))] = true;
				}
			}

			//prunes "dead paths" that generate with holes in them
			prune ();

			//checks if the dungeon is fully connected and stops generating paths if it is
			if (floodFillCheck () == passableCount ()) {
				keepRooming = !keepRooming;
			}


		}
		//prunes dead ends from the passability grid
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
						//kills cells without at least 2 direct neighbours
						passabilityGrid [x, y] = false;
					}
				}
			}
		}
	}

	int checkNeighbours(Vector2 v)
	{
		//counts the total number of direct (up, down, left, right) neighbours for each cell
		int output = 0;
		for (int x = -1; x <= 1; x++) {
			for (int y = -1; y <= 1; y++) {
				Vector2 scanVec = v + new Vector2 (x, y);
				if (scanVec.x < 0 || scanVec.x >= width || scanVec.y < 0 || scanVec.y >= height || (Mathf.Abs(x) == Mathf.Abs(y)))
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
		//finds a passable location on the grid
		while (!passabilityGrid [randomX, randomY]) {
			randomX = Mathf.RoundToInt (Random.value * (width - 1));
			randomY = Mathf.RoundToInt (Random.value * (height - 1));
		}
		Vector2 xy = new Vector2 (randomX, randomY);
		Stack<Vector2> s = new Stack<Vector2>();
		s.Push (xy);

		//uses a stack to check surrounding tiles to see if they are passable, flagging ones that have been checked
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

	int passableCount() //counts total number of passable squares in the grid
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
