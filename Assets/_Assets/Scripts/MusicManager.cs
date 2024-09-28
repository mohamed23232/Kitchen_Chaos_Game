using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MusicManager : MonoBehaviour
{
    private const string VOLUME_KEY = "MusicVolume";
    public static MusicManager Instance { get; private set; }
    private AudioSource audioSource;


    private void Awake() {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        volume = PlayerPrefs.GetFloat(VOLUME_KEY, 0.3f);
        audioSource.volume = volume;
    }

    private float volume = 0.3f;
    public void ChangeVolume() {
        volume += 0.1f;
        if (volume > 1f) {
            volume = 0f;
        }
        audioSource.volume = volume;
        PlayerPrefs.SetFloat(VOLUME_KEY, volume);
        PlayerPrefs.Save();
    }
    public float GetVolume() {
        return volume;
    }
}
