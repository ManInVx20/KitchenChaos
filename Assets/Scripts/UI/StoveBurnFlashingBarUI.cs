using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurnFlashingBarUI : MonoBehaviour
{
    [SerializeField]
    private StoveCounter stoveCounter;

    private Animator animator;
    private int isFlashingHash;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        isFlashingHash = Animator.StringToHash("IsFlashing");
    }

    private void Start()
    {
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;

        animator.SetBool(isFlashingHash, false);
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedArgs args)
    {
        float burnShowProgressAmount = 0.5f;
        bool show = stoveCounter.IsFried() && args.ProgressNormalized >= burnShowProgressAmount;

        animator.SetBool(isFlashingHash, show);
    }
}
