using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    private const string PLAYER_PREFS_MUSIC_VOLUME = "MusicVolume";

    private AudioSource audioSource;
    private float volume = 0.3f;

    private void Awake()
    {
        Instance = this;

        audioSource = GetComponent<AudioSource>();

        if (PlayerPrefs.HasKey(PLAYER_PREFS_MUSIC_VOLUME))
        {
            volume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME, 0.3f);
            audioSource.volume = volume;
        }
    }

    public void ChangeVolume()
    {
        volume += 0.1f;

        if (volume > 1.0f)
        {
            volume = 0.0f;
        }

        audioSource.volume = volume;

        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return volume;
    }
}
