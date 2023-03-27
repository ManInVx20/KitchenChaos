using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField]
    private GameObject hasProgressGameObject;
    [SerializeField]
    private Image barImage;

    private IHasProgress hasProgress;

    private void Start()
    {
        hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();

        if (hasProgress == null)
        {
            Debug.LogError($"Game object {hasProgressGameObject.name} does not have component that implements IHasProgress interface");
        }

        hasProgress.OnProgressChanged += IHasProgress_OnProgressChanged;

        barImage.fillAmount = 0.0f;

        Hide();
    }

    private void IHasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedArgs args)
    {
        barImage.fillAmount = args.ProgressNormalized;

        if (args.ProgressNormalized > 0.0f && args.ProgressNormalized < 1.0f)
        {
            Show();
        }
        else
        {
            Hide();
        }
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
