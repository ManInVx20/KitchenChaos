using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObjectData
    {
        public KitchenObjectSO KitchenObjectSO;
        public GameObject GameObject;
    }

    [SerializeField]
    private PlateKitchenObject plateKitchenObject;
    [SerializeField]
    private List<KitchenObjectData> kitchenObjectDataList;

    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;

        foreach (KitchenObjectData kitchenObjectData in kitchenObjectDataList)
        {
            kitchenObjectData.GameObject.SetActive(false);
        }
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedArgs args)
    {
        foreach (KitchenObjectData kitchenObjectData in kitchenObjectDataList)
        {
            if (kitchenObjectData.KitchenObjectSO == args.KitchenObjectSO)
            {
                kitchenObjectData.GameObject.SetActive(true);
            }
        }
    }
}
