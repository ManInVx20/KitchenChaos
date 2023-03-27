using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Frying Recipe", fileName = "New Frying Recipe")]
public class FryingRecipeSO : ScriptableObject
{
    [field: SerializeField]
    public KitchenObjectSO Input { get; private set; }
    [field: SerializeField]
    public KitchenObjectSO Output { get; private set; }
    [field: SerializeField]
    public int FryingTimerMax { get; private set; }
}
