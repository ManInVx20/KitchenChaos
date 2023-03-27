using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu(menuName = "Recipe List", fileName = "New Recipe List")]
public class RecipeListSO : ScriptableObject
{
    [field: SerializeField]
    public List<RecipeSO> RecipeSOList { get; private set; }
}
