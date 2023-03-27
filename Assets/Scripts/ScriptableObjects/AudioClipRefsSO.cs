using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Audio Clip Refs", fileName = "New Audio Clip Refs")]
public class AudioClipRefsSO : ScriptableObject
{
    [field: SerializeField]
    public AudioClip[] ChopArray { get; private set; }
    [field: SerializeField]
    public AudioClip[] DeliveryFailedArray { get; private set; }
    [field: SerializeField]
    public AudioClip[] DeliverySuccessArray { get; private set; }
    [field: SerializeField]
    public AudioClip[] FootstepArray { get; private set; }
    [field: SerializeField]
    public AudioClip[] ObjectDropArray { get; private set; }
    [field: SerializeField]
    public AudioClip[] ObjectPickupArray { get; private set; }
    [field: SerializeField]
    public AudioClip StoveSizzle { get; private set; }
    [field: SerializeField]
    public AudioClip[] TrashArray { get; private set; }
    [field: SerializeField]
    public AudioClip[] WarningArray { get; private set; }
}
