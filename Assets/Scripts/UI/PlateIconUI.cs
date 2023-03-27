using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconUI : MonoBehaviour
{
    [SerializeField]
    private PlateKitchenObject plateKitchenObject;
    [SerializeField]
    private Transform iconTemplatePrefab;

    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;

        iconTemplatePrefab.gameObject.SetActive(false);
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedArgs args)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in transform)
        {
            if (child != iconTemplatePrefab)
            {
                Destroy(child.gameObject);
            }
        }

        foreach (KitchenObjectSO kitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
        {
            Transform iconTemplateTransform = Instantiate(iconTemplatePrefab, transform);
            iconTemplateTransform.gameObject.SetActive(true);
            iconTemplateTransform.GetComponent<PlateIconSingleUI>().SetIconImage(kitchenObjectSO);
        }
    }
}
