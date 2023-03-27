using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerAnimator : NetworkBehaviour
{
    [SerializeField]
    private Player player;

    private Animator animator;

    private int isWalkingHash;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("IsWalking");
    }

    private void Update()
    {
        if (!IsOwner)
        {
            return;
        }

        animator.SetBool(isWalkingHash, player.IsWalking());
    }
}
