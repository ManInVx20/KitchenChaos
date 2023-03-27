using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPauseAction;
    public event EventHandler OnBindingRebind;

    private const string PLAYER_PREFS_BINDINGS = "InputBindings";

    public enum Binding
    {
        Move_Up,
        Move_Down,
        Move_Left,
        Move_Right,
        Interact,
        InteractAlternate,
        Pause,
        Gamepad_Interact,
        Gamepad_InteractAlternate,
        Gamepad_Pause
    }

    private GameControls gameControls;

    private void Awake()
    {
        Instance = this;

        gameControls = new GameControls();

        if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS))
        {
            gameControls.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));
        }

        gameControls.Player.Enable();

        gameControls.Player.Interact.performed += Interact_performed;
        gameControls.Player.InteractAlternate.performed += InteractAlternate_performed;
        gameControls.Player.Pause.performed += Pause_performed;
    }

    private void OnDestroy()
    {
        gameControls.Player.Interact.performed -= Interact_performed;
        gameControls.Player.InteractAlternate.performed -= InteractAlternate_performed;
        gameControls.Player.Pause.performed -= Pause_performed;

        gameControls.Dispose();
    }

    public Vector2 GetMoveInput()
    {
        return gameControls.Player.Move.ReadValue<Vector2>();
    }

    public string GetBindingText(Binding binding)
    {
        switch (binding)
        {
            case Binding.Move_Up:
                return gameControls.Player.Move.bindings[1].ToDisplayString();
            case Binding.Move_Down:
                return gameControls.Player.Move.bindings[2].ToDisplayString();
            case Binding.Move_Left:
                return gameControls.Player.Move.bindings[3].ToDisplayString();
            case Binding.Move_Right:
                return gameControls.Player.Move.bindings[4].ToDisplayString();
            case Binding.Interact:
                return gameControls.Player.Interact.bindings[0].ToDisplayString();
            case Binding.InteractAlternate:
                return gameControls.Player.InteractAlternate.bindings[0].ToDisplayString();
            case Binding.Pause:
                return gameControls.Player.Pause.bindings[0].ToDisplayString();
            case Binding.Gamepad_Interact:
                return gameControls.Player.Interact.bindings[1].ToDisplayString();
            case Binding.Gamepad_InteractAlternate:
                return gameControls.Player.InteractAlternate.bindings[1].ToDisplayString();
            case Binding.Gamepad_Pause:
                return gameControls.Player.Pause.bindings[1].ToDisplayString();
            default:
                return null;
        }
    }

    public void RebindBinding(Binding binding, Action onActionRebound)
    {
        gameControls.Player.Disable();

        InputAction inputAction;
        int bindingIndex;

        switch (binding)
        {
            case Binding.Move_Up:
                inputAction = gameControls.Player.Move;
                bindingIndex = 1;
                break;
            case Binding.Move_Down:
                inputAction = gameControls.Player.Move;
                bindingIndex = 2;
                break;
            case Binding.Move_Left:
                inputAction = gameControls.Player.Move;
                bindingIndex = 3;
                break;
            case Binding.Move_Right:
                inputAction = gameControls.Player.Move;
                bindingIndex = 4;
                break;
            case Binding.Interact:
                inputAction = gameControls.Player.Interact;
                bindingIndex = 0;
                break;
            case Binding.InteractAlternate:
                inputAction = gameControls.Player.InteractAlternate;
                bindingIndex = 0;
                break;
            case Binding.Pause:
                inputAction = gameControls.Player.Pause;
                bindingIndex = 0;
                break;
            case Binding.Gamepad_Interact:
                inputAction = gameControls.Player.Interact;
                bindingIndex = 1;
                break;
            case Binding.Gamepad_InteractAlternate:
                inputAction = gameControls.Player.InteractAlternate;
                bindingIndex = 1;
                break;
            case Binding.Gamepad_Pause:
                inputAction = gameControls.Player.Pause;
                bindingIndex = 1;
                break;
            default:
                inputAction = null;
                bindingIndex = -1;
                break;
        }

        inputAction?.PerformInteractiveRebinding(bindingIndex)
            .OnComplete((callback) =>
            {
                callback.Dispose();

                gameControls.Player.Enable();

                onActionRebound?.Invoke();

                PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS, gameControls.SaveBindingOverridesAsJson());
                PlayerPrefs.Save();

                OnBindingRebind?.Invoke(this, EventArgs.Empty);
            })
            .Start();
    }

    private void Interact_performed(InputAction.CallbackContext context)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternate_performed(InputAction.CallbackContext context)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Pause_performed(InputAction.CallbackContext context)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }
}
