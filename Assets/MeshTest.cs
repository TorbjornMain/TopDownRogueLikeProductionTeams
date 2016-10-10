using UnityEngine;
using System.Collections.Generic;
public class MeshTest : MonoBehaviour {

	MeshFilter mf;
	MeshMaker mm;
	// Use this for initialization
	void Start () {
		mm = new MeshMaker();
		mf = GetComponent <MeshFilter> ();
		List<Vector3> vecs = new List<Vector3>();
		List<Vector2> uvs = new List<Vector2> ();

		vecs.Add (new Vector3 (0, 0, 0));
		vecs.Add (new Vector3 (1, 0, 0));
		vecs.Add (new Vector3 (1, 0, 1));
		vecs.Add (new Vector3 (0, 0, 1));

		uvs.Add (new Vector2 (0, 0));
		uvs.Add (new Vector2 (0, 0));
		uvs.Add (new Vector2 (0, 0));
		uvs.Add (new Vector2 (0, 0));
		mm.AddQuad (vecs.ToArray (), uvs.ToArray ());
		mf.mesh = mm.mesh;
	}
}
