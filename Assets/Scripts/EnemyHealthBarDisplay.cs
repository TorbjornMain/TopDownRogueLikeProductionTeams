using UnityEngine;
using System.Collections;

[RequireComponent(typeof(RectTransform))]
public class EnemyHealthBarDisplay : MonoBehaviour {
	public Camera viewport;
	// Update is called once per frame
	void Update () {
		foreach (var item in EnemyManager.healthBars) {
			
			Vector3 screenPos = viewport.WorldToScreenPoint (item.transform.position + new Vector3(0, item.offset, 0));
			RectTransform rt = item.healthBarInstance.GetComponent<RectTransform> ();
			if (rt.parent == null) {
				rt.parent = gameObject.GetComponent<RectTransform>();
			}
			rt.anchoredPosition = new Vector2(screenPos.x, screenPos.y);
		}
	}
}
