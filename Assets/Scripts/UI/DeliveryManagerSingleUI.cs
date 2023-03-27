using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI recipeNameText;
    [SerializeField]
    private Transform iconContainer;
    [SerializeField]
    private Transform iconTemplatePrefab;

    private void Start()
    {
        iconTemplatePrefab.gameObject.SetActive(false);
    }

    public void SetRecipeSO(RecipeSO recipeSO)
    {
        recipeNameText.text = recipeSO.RecipeName;

        foreach(Transform child in iconContainer)
        {
            if (child != iconTemplatePrefab)
            {
                Destroy(child.gameObject);
            }
        }

        foreach (KitchenObjectSO kitchenObjectSO in recipeSO.KitchenObjectSOList)
        {
            Transform iconTemplateTrasform = Instantiate(iconTemplatePrefab, iconContainer);
            iconTemplateTrasform.gameObject.SetActive(true);
            iconTemplateTrasform.GetComponent<Image>().sprite = kitchenObjectSO.Sprite;
        }
    }
}
