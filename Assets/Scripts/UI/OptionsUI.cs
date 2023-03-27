using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    [SerializeField]
    private Button soundEffectsButton;
    [SerializeField]
    private Button musicButton;
    [SerializeField]
    private Button closeButton;
    [SerializeField]
    private Button moveUpButton;
    [SerializeField]
    private Button moveDownButton;
    [SerializeField]
    private Button moveLeftButton;
    [SerializeField]
    private Button moveRightButton;
    [SerializeField]
    private Button interactButton;
    [SerializeField]
    private Button interactAlternateButton;
    [SerializeField]
    private Button pauseButton;
    [SerializeField]
    private Button gamepadInteractButton;
    [SerializeField]
    private Button gamepadInteractAlternateButton;
    [SerializeField]
    private Button gamepadPauseButton;
    [SerializeField]
    private TextMeshProUGUI soundEffectsText;
    [SerializeField]
    private TextMeshProUGUI musicText;
    [SerializeField]
    private TextMeshProUGUI moveUpText;
    [SerializeField]
    private TextMeshProUGUI moveDownText;
    [SerializeField]
    private TextMeshProUGUI moveLeftText;
    [SerializeField]
    private TextMeshProUGUI moveRightText;
    [SerializeField]
    private TextMeshProUGUI interactText;
    [SerializeField]
    private TextMeshProUGUI interactAlternateText;
    [SerializeField]
    private TextMeshProUGUI pauseText;
    [SerializeField]
    private TextMeshProUGUI gamepadInteractText;
    [SerializeField]
    private TextMeshProUGUI gamepadInteractAlternateText;
    [SerializeField]
    private TextMeshProUGUI gamepadPauseText;
    [SerializeField]
    private GameObject pressToRebindKeyPanel;

    private Action onCloseButonAction;

    private void Awake()
    {
        soundEffectsButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();

            UpdateVisual();
        });
        musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();

            UpdateVisual();
        });
        closeButton.onClick.AddListener(() =>
        {
            Hide();

            onCloseButonAction?.Invoke();
        });

        moveUpButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Move_Up);
        });
        moveDownButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Move_Down);
        });
        moveLeftButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Move_Left);
        });
        moveRightButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Move_Right);
        });
        interactButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Interact);
        });
        interactAlternateButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.InteractAlternate);
        });
        pauseButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Pause);
        });
        gamepadInteractButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Gamepad_Interact);
        });
        gamepadInteractAlternateButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Gamepad_InteractAlternate);
        });
        gamepadPauseButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Gamepad_Pause);
        });
    }

    private void Start()
    {
        GameManager.Instance.OnLocalGameUnpaused += GameManager_OnLocalGameUnpaused;

        UpdateVisual();

        HidePressToRebindKeyPanel();
        Hide();
    }

    private void GameManager_OnLocalGameUnpaused(object sender, System.EventArgs args)
    {
        Hide();
    }

    private void UpdateVisual()
    {
        int soundEffectsValue = Mathf.RoundToInt(SoundManager.Instance.GetVolume() * 10.0f);
        soundEffectsText.text = $"Sound Effects: {soundEffectsValue}";

        int musicValue = Mathf.RoundToInt(MusicManager.Instance.GetVolume() * 10.0f);
        musicText.text = $"Music: {musicValue}";

        moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        interactAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);
        pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
        gamepadInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Interact);
        gamepadInteractAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_InteractAlternate);
        gamepadPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Pause);
    }

    public void Show(Action onCloseButonAction)
    {
        this.onCloseButonAction = onCloseButonAction;

        gameObject.SetActive(true);

        soundEffectsButton.Select();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void ShowPressToRebindKeyPanel()
    {
        pressToRebindKeyPanel.SetActive(true);
    }

    private void HidePressToRebindKeyPanel()
    {
        pressToRebindKeyPanel.SetActive(false);
    }

    private void RebindBinding(GameInput.Binding binding)
    {
        ShowPressToRebindKeyPanel();

        GameInput.Instance.RebindBinding(binding, () =>
        {
            HidePressToRebindKeyPanel();

            UpdateVisual();
        });
    }
}
