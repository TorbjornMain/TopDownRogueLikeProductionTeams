using System.Collections.Generic;
using UnityEngine;

public enum Shape
{
	Quad,
	Tri
}

public class MeshMaker
{
	private Mesh _mesh;
	private List<Vector3> verticies = new List<Vector3>();
	private List<int> indicies = new List<int>();
	private List<Vector2> uvs = new List<Vector2>();
	private List<Shape> shapes = new List<Shape>();

	public Mesh mesh {
		get {
			_mesh.SetVertices (verticies);
			_mesh.SetTriangles (indicies.ToArray (), 0);
			_mesh.SetUVs (0, uvs);
			_mesh.RecalculateNormals ();
			return _mesh;
		}
	}

	public MeshMaker ()
	{
		_mesh = new Mesh ();
	}

	public void AddTri (Vector3[] localPositions, Vector2[] uvs)
	{
		shapes.Add (Shape.Tri);
		for (int i = 0; i < 3; i++) {
			verticies.Add (localPositions [i]);
			indicies.Add (verticies.Count - 1);
			this.uvs.Add (uvs [i]);
		}
	}

	public void AddQuad (Vector3[] localPositions, Vector2[] uvs)
	{
		shapes.Add (Shape.Quad);
		for (int i = 0; i < 4; i++) {
			verticies.Add (localPositions [i]);
			this.uvs.Add (uvs [i]);
		}
		indicies.Add (verticies.Count -  1);
		indicies.Add (verticies.Count -  2);
		indicies.Add (verticies.Count -  3);
		indicies.Add (verticies.Count -  1);
		indicies.Add (verticies.Count -  3);
		indicies.Add (verticies.Count -  4);
	}

}

