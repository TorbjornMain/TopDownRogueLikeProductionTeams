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
		fadeImage.color -= new Color(0, 0, 0, Mathf.Clamp01 (Time.deltaTime / fadeOutTime));

		if (fadeImage.color.a < 0.05) {
			SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
			yield break;
		}

		yield return null;
	}

	void Die()
	{
		fadeImage = fader.widgetInstance.GetComponent<Image> ();
		StartCoroutine (Fade ());
	}
}
