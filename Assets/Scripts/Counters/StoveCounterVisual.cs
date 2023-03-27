using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField]
    private StoveCounter stoveCounter;
    [SerializeField]
    private GameObject stoveOnGameObject;
    [SerializeField]
    private GameObject stoveParticlesGameObject;

    private void Start()
    {
        stoveCounter.OnStatedChanged += StoveCounter_OnStatedChanged;
    }

    private void StoveCounter_OnStatedChanged(object sender, StoveCounter.OnStatedChangedArgs args)
    {
        bool showVisual = args.State == StoveCounter.State.Frying || args.State == StoveCounter.State.Fried;
        stoveOnGameObject.SetActive(showVisual);
        stoveParticlesGameObject.SetActive(showVisual);

        
    }
}
