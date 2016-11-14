using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundOnDeath:MonoBehaviour
{
	AudioSource snd;

	void Start()
	{
		snd = GetComponent<AudioSource> ();
	}

	void Die()
	{
		snd.Play ();
	}
}

