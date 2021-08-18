using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManagerEffect : Singleton<AudioManagerEffect>
{

	public AudioMixerGroup mixerGroup;

	public Sound[] soundFxBank;

    public void Play(string sound, GameObject gameObjectSource, float volumeScale = 1)
	{
		Sound s = Array.Find(soundFxBank, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}
		AudioSource audioSource = gameObjectSource.GetComponent<AudioSource>();
		if(audioSource == null)
        {
			gameObjectSource.AddComponent(typeof(AudioSource));
        }

		audioSource.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		audioSource.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		audioSource.PlayOneShot(s.clip, volumeScale);
	}
}
