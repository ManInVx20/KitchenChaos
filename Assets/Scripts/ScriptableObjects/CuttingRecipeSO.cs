using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cutting Recipe", fileName = "New Cutting Recipe")]
public class CuttingRecipeSO : ScriptableObject
{
    [field: SerializeField]
    public KitchenObjectSO Input { get; private set; }
    [field: SerializeField]
    public KitchenObjectSO Output { get; private set; }
    [field: SerializeField]
    public int CuttingProgressMax { get; private set; }
}
