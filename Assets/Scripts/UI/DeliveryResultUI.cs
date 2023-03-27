using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryResultUI : MonoBehaviour
{
    [SerializeField]
    private Image backgroundImage;
    [SerializeField]
    private Image iconImage;
    [SerializeField]
    private TextMeshProUGUI messageText;
    [SerializeField]
    private Color successColor;
    [SerializeField]
    private Color failedColor;
    [SerializeField]
    private Sprite successSprite;
    [SerializeField]
    private Sprite failedSprite;

    private Animator animator;
    private int popupHash;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        popupHash = Animator.StringToHash("Popup");
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;

        Hide();
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs args)
    {
        Show();

        animator.SetTrigger(popupHash);
        backgroundImage.color = successColor;
        iconImage.sprite = successSprite;
        messageText.text = "DELIVERY\nSUCCESS";
    }

    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs args)
    {
        Show();

        animator.SetTrigger(popupHash);
        backgroundImage.color = failedColor;
        iconImage.sprite = failedSprite;
        messageText.text = "DELIVERY\nFAILED";
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
