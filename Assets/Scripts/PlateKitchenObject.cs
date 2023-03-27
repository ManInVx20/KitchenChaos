using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public class OnIngredientAddedArgs : EventArgs
    {
        public KitchenObjectSO KitchenObjectSO;
    }
    public event EventHandler<OnIngredientAddedArgs> OnIngredientAdded;

    [SerializeField]
    private List<KitchenObjectSO> validKitchenObjectSOList;

    private List<KitchenObjectSO> kitchenObjectSOList;

    protected override void Awake()
    {
        base.Awake();

        kitchenObjectSOList = new List<KitchenObjectSO>();
    }

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if (validKitchenObjectSOList.Contains(kitchenObjectSO))
        {
            if (!kitchenObjectSOList.Contains(kitchenObjectSO))
            {
                AddIngredientServerRpc(GameMultiplayer.Instance.GetKitchenObjectSOIndex(kitchenObjectSO));

                return true;
            }
        }

        return false;
    }

    public List<KitchenObjectSO> GetKitchenObjectSOList()
    {
        return kitchenObjectSOList;
    }

    [ServerRpc(RequireOwnership = false)]
    private void AddIngredientServerRpc(int kitchenObjectSOIndex)
    {
        AddIngredientClientRpc(kitchenObjectSOIndex);
    }

    [ClientRpc]
    private void AddIngredientClientRpc(int kitchenObjectSOIndex)
    {
        KitchenObjectSO kitchenObjectSO = GameMultiplayer.Instance.GetKitchenObjectSOFromIndex(kitchenObjectSOIndex);
        kitchenObjectSOList.Add(kitchenObjectSO);

        OnIngredientAdded?.Invoke(this, new OnIngredientAddedArgs
        {
            KitchenObjectSO = kitchenObjectSO
        });
    }
}
