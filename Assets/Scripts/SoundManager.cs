using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";

    [SerializeField]
    private AudioClipRefsSO audioClipRefsSO;

    private float volume = 0.5f;

    private void Awake()
    {
        Instance = this;

        if (PlayerPrefs.HasKey(PLAYER_PREFS_SOUND_EFFECTS_VOLUME))
        {
            volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 0.5f);
        }
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;

        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;

        Player.OnAnyPickedSomething += Player_OnAnyPickedSomething;

        BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;

        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
    }

    public void PlayFootstepSound(Vector3 position, float volumeMultiplier)
    {
        PlaySound(audioClipRefsSO.FootstepArray, position, volumeMultiplier);
    }

    public void PlayCountdownSound()
    {
        PlaySound(audioClipRefsSO.WarningArray, Vector3.zero);
    }

    public void PlayWarningSound(Vector3 position)
    {
        PlaySound(audioClipRefsSO.WarningArray, position);
    }

    public void ChangeVolume()
    {
        volume += 0.1f;

        if (volume > 1.0f)
        {
            volume = 0.0f;
        }

        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return volume;
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, EventArgs args)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefsSO.DeliverySuccessArray, deliveryCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, EventArgs args)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefsSO.DeliveryFailedArray, deliveryCounter.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, EventArgs args)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioClipRefsSO.ChopArray, cuttingCounter.transform.position);
    }

    private void Player_OnAnyPickedSomething(object sender, EventArgs args)
    {
        Player player = sender as Player;
        PlaySound(audioClipRefsSO.ObjectPickupArray, player.transform.position);
    }

    private void BaseCounter_OnAnyObjectPlacedHere(object sender, EventArgs args)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(audioClipRefsSO.ObjectDropArray, baseCounter.transform.position);
    }

    private void TrashCounter_OnAnyObjectTrashed(object sender, EventArgs args)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(audioClipRefsSO.TrashArray, trashCounter.transform.position);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volumeMultiplier = 1.0f)
    {
        int randomIndex = UnityEngine.Random.Range(0, audioClipArray.Length);
        PlaySound(audioClipArray[randomIndex], position, volumeMultiplier);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1.0f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * volume);
    }
}
