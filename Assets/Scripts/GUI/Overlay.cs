﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(RectTransform))]
public class Overlay : MonoBehaviour {
	// Update is called once per frame
	void Update () {
		if (Camera.main != null) {
			foreach (var item in OverlayManager.widgets) {
				
				RectTransform rt = item.widgetInstance.GetComponent<RectTransform> ();
				if (rt.parent == null) {
					rt.SetParent (gameObject.GetComponent<RectTransform> ());
				}
				if (item.followsObject) {
					Vector3 screenPos = Camera.main.WorldToScreenPoint (item.transform.position + new Vector3(0, item.heightOffset, 0));
					rt.anchoredPosition = new Vector2 (screenPos.x, screenPos.y);
				}
			}
		}
	}
}
