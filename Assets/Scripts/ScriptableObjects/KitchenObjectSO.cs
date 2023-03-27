using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Kitchen Object", fileName = "New Kitchen Object")]
public class KitchenObjectSO : ScriptableObject
{
    [field: SerializeField]
    public GameObject Prefab { get; private set; }
    [field: SerializeField]
    public Sprite Sprite { get; private set; }
    [field: SerializeField]
    public string ObjectName { get; private set; }
}
