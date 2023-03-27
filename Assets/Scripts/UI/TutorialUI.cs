using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI keyMoveUpText;
    [SerializeField]
    private TextMeshProUGUI keyMoveDownText;
    [SerializeField]
    private TextMeshProUGUI keyMoveLeftText;
    [SerializeField]
    private TextMeshProUGUI keyMoveRightText;
    [SerializeField]
    private TextMeshProUGUI keyInteractText;
    [SerializeField]
    private TextMeshProUGUI keyInteractAlternateText;
    [SerializeField]
    private TextMeshProUGUI keyPauseText;
    [SerializeField]
    private TextMeshProUGUI gamepadKeyMove;
    [SerializeField]
    private TextMeshProUGUI gamepadKeyInteractText;
    [SerializeField]
    private TextMeshProUGUI gamepadKeyInteractAlternateText;
    [SerializeField]
    private TextMeshProUGUI gamepadKeyPauseText;

    private void Start()
    {
        GameInput.Instance.OnBindingRebind += GameInput_OnBindingRebind;

        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        GameManager.Instance.OnLocalPlayerReadyChanged += GameManager_OnLocalPlayerReadyChanged;

        UpdateVisual();

        Show();
    }

    private void GameInput_OnBindingRebind(object sender, System.EventArgs args)
    {
        UpdateVisual();
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs args)
    {
        if (GameManager.Instance.IsCountdownToStart())
        {
            Hide();
        }
    }

    private void GameManager_OnLocalPlayerReadyChanged(object sender, System.EventArgs args)
    {
        if (GameManager.Instance.IsLocalPlayerReady())
        {
            Hide();
        }
    }

    private void UpdateVisual()
    {
        keyMoveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        keyMoveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        keyMoveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        keyMoveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        keyInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        keyInteractAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);
        keyPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
        gamepadKeyMove.text = "Left Stick";
        gamepadKeyInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Interact);
        gamepadKeyInteractAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_InteractAlternate);
        gamepadKeyPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Pause);
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
