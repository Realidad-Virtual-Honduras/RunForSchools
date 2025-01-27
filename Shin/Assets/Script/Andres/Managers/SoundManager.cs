using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource musicAudioSource;
    public AudioMixer audioMixer;
    public AudioMixerGroup audioMixerGroup;

    public AudioClip[] mainMenuAudioClip;
    public AudioClip[] levelsAudioClip;
    [Space]
    public AudioClip jumpClip;
    public AudioClip damageClip;
    public AudioClip grabClip;
    public AudioClip btnClip;

    private void Awake()
    {
        instance = this;
    }

    public void SoundOnPlace(AudioClip clip, Vector3 position, float volume = 1f)
    {
        GameObject gameObject = new GameObject("One shot audio");
        gameObject.transform.position = position;
        AudioSource audioSource = (AudioSource)gameObject.AddComponent(typeof(AudioSource));
        audioSource.clip = clip;
        audioSource.outputAudioMixerGroup = audioMixerGroup;
        audioSource.spatialBlend = 0f;
        audioSource.volume = volume;
        audioSource.Play();
        Object.Destroy(gameObject, clip.length * ((Time.timeScale < 0.01f) ? 0.01f : Time.timeScale));
    }
}
