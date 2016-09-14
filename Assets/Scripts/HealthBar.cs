using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {

	public float percentFilled;
	public RectTransform image;
	private float baseWidth;

	void Start() {
		baseWidth = image.sizeDelta.x;
	}

	// Update is called once per frame
	void Update () {
		image.sizeDelta = new Vector2 (baseWidth * percentFilled, image.sizeDelta.y);
	}
}
