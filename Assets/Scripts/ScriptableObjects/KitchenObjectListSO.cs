using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName = "Kitchen Object List", fileName = "New Kitchen Object List")]
public class KitchenObjectListSO : ScriptableObject
{
    [field: SerializeField]
    public List<KitchenObjectSO> KitchenObjectSOList { get; private set; }
}
