using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    [SerializeField]
    private CuttingCounter cuttingCounter;

    private Animator animator;

    private int cutHash;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        cutHash = Animator.StringToHash("Cut");
    }

    private void Start()
    {
        cuttingCounter.OnCut += CuttingCounter_OnCut;
    }

    private void CuttingCounter_OnCut(object sender, EventArgs args)
    {
        animator.SetTrigger(cutHash);
    }
}
