using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Burning Recipe", fileName = "New Burning Recipe")]
public class BurningRecipeSO : ScriptableObject
{
    [field: SerializeField]
    public KitchenObjectSO Input { get; private set; }
    [field: SerializeField]
    public KitchenObjectSO Output { get; private set; }
    [field: SerializeField]
    public int BurningTimerMax { get; private set; }
}
