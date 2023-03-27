using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    [SerializeField]
    private ContainerCounter containerCounter;

    private Animator animator;

    private int openCloseHash;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        openCloseHash = Animator.StringToHash("OpenClose");
    }

    private void Start()
    {
        containerCounter.OnPlayerGrabbedObject += ContainerCounter_OnPlayerGrabbedObject;
    }

    private void ContainerCounter_OnPlayerGrabbedObject(object sender, EventArgs args)
    {
        animator.SetTrigger(openCloseHash);
    }
}
