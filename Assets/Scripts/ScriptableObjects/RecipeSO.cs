using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Recipe", fileName = "New Recipe")]
public class RecipeSO : ScriptableObject
{
    [field: SerializeField]
    public List<KitchenObjectSO> KitchenObjectSOList { get; private set; }
    [field: SerializeField]
    public string RecipeName { get; private set; }
}
