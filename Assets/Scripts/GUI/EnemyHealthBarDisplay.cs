using UnityEngine;
using System.Collections;

[RequireComponent(typeof(RectTransform))]
public class EnemyHealthBarDisplay : MonoBehaviour {
	// Update is called once per frame
	void Update () {
		if (Camera.main != null) {
			foreach (var item in EnemyManager.healthBars) {
				Vector3 screenPos = Camera.main.WorldToScreenPoint (item.transform.position + new Vector3(0, item.offset, 0));
				RectTransform rt = item.healthBarInstance.GetComponent<RectTransform> ();
				if (rt.parent == null) {
					rt.SetParent (gameObject.GetComponent<RectTransform> ());
				}
				rt.anchoredPosition = new Vector2 (screenPos.x, screenPos.y);
			}
		}
	}
}
