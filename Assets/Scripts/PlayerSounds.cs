using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private Player player;
    private float footstepTimer;
    private float footstepTimerMax = 0.1f;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        footstepTimer -= Time.deltaTime;
        if (footstepTimer <= 0.0f)
        {
            footstepTimer = footstepTimerMax;

            if (player.IsWalking())
            {
                float volumeMultiplier = 1.0f;
                SoundManager.Instance.PlayFootstepSound(player.transform.position, volumeMultiplier);
            }
        }
    }
}
