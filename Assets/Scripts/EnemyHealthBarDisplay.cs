using UnityEngine;
using System.Collections;

public class EnemyHealthBarDisplay : MonoBehaviour {
	public Camera viewport;
	// Update is called once per frame
	void Update () {
		foreach (var item in EnemyManager.healthBars) {
			Vector3 screenPos = viewport.WorldToScreenPoint (item.transform.position + new Vector3(0, item.offset, 0));
			item.healthBarInstance.GetComponent<RectTransform> ().anchoredPosition = new Vector2(screenPos.x, screenPos.y);
		}
	}
}
