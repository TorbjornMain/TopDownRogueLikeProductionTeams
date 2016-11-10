using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent(typeof(DamageableItem))]
public class SceneTransitionOnDeath : MonoBehaviour {
	public float fadeOutTime = 5;
	public string sceneName;

	public OverlayWidget fader;
	Image fadeImage;

	IEnumerator Fade()
	{
		while (fadeImage.color.a < 0.95f) {
			fadeImage.color += new Color (0, 0, 0, Mathf.Clamp01 (Time.deltaTime / fadeOutTime));
			yield return null;
		}
		SceneManager.LoadSceneAsync (sceneName, LoadSceneMode.Single);
	}

	void Die()
	{
		fadeImage = fader.widgetInstance.GetComponent<Image> ();
		StartCoroutine (Fade ());
	}
}
