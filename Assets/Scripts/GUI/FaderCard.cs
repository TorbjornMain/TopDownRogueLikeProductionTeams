using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(RectTransform))]
public class FaderCard : MonoBehaviour {
	RectTransform rt;

	// Use this for initialization
	void Start () {
		rt = GetComponent<RectTransform> ();
		rt.offsetMax = Vector2.zero;
		rt.offsetMin = Vector2.zero;
	}
	void Update()
	{
		rt.offsetMax = Vector2.zero;
		rt.offsetMin = Vector2.zero;
	}
}
