using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField]
    private Transform container;
    [SerializeField]
    private Transform recipeTemplatePrefab;

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSpawned += DeliveryManager_OnRecipeSpawned;
        DeliveryManager.Instance.OnRecipeCompleted += DeliveryManager_OnRecipeCompleted;

        recipeTemplatePrefab.gameObject.SetActive(false);

        UpdateVisual();
    }

    private void DeliveryManager_OnRecipeSpawned(object sender, EventArgs args)
    {
        UpdateVisual();
    }

    private void DeliveryManager_OnRecipeCompleted(object sender, EventArgs args)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in container)
        {
            if (child != recipeTemplatePrefab)
            {
                Destroy(child.gameObject);
            }
        }

        foreach (RecipeSO recipeSO in DeliveryManager.Instance.GetWaitingRecipeSOList())
        {
            Transform recipeTemplateTransform = Instantiate(recipeTemplatePrefab, container);
            recipeTemplateTransform.gameObject.SetActive(true);
            recipeTemplateTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(recipeSO);
        }
    }
}
