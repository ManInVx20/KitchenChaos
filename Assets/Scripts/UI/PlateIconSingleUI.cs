using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlateIconSingleUI : MonoBehaviour
{
    [SerializeField]
    private Image iconImage;

    public void SetIconImage(KitchenObjectSO kitchenObjectSO)
    {
        iconImage.sprite = kitchenObjectSO.Sprite;
    }
}
