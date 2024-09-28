using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour {
    private const string BINDING_SCHEME = "Bindings_Scheme";

    public static GameInput Instance { get; private set; }

    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPauseAction;

    private PlayerInputActions playerInputActions;

    public enum Bindings {
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight,
        Interact,
        InteractAlternate,
    }

    private void Awake() {
        Instance = this;
        playerInputActions = new PlayerInputActions();


        if (PlayerPrefs.HasKey(BINDING_SCHEME)) {
            playerInputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(BINDING_SCHEME));
        }

        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
        playerInputActions.Player.Pause.performed += Pause_performed;


    }

    private void OnDestroy() {
        playerInputActions.Player.Interact.performed -= Interact_performed;
        playerInputActions.Player.InteractAlternate.performed -= InteractAlternate_performed;
        playerInputActions.Player.Pause.performed -= Pause_performed;
        playerInputActions.Dispose();
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNorm() {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        return inputVector;
    }

    public string GetBindingText(Bindings binding) {
        switch (binding) {
            default:
            case Bindings.Interact:
                return playerInputActions.Player.Interact.bindings[0].ToDisplayString();
            case Bindings.InteractAlternate:
                return playerInputActions.Player.InteractAlternate.bindings[0].ToDisplayString();
            case Bindings.MoveUp:
                return playerInputActions.Player.Move.bindings[1].ToDisplayString();
            case Bindings.MoveDown:
                return playerInputActions.Player.Move.bindings[2].ToDisplayString();
            case Bindings.MoveLeft:
                return playerInputActions.Player.Move.bindings[3].ToDisplayString();
            case Bindings.MoveRight:
                return playerInputActions.Player.Move.bindings[4].ToDisplayString();
        }
    }

    public void RebindBinding(Bindings binding,Action action) {
        playerInputActions.Disable();

        InputAction inputAction;
        int bindingIndex;
        switch (binding) {
            default:
            case Bindings.Interact:
                inputAction = playerInputActions.Player.Interact;
                bindingIndex = 0;
                break;
            case Bindings.InteractAlternate:
                inputAction = playerInputActions.Player.InteractAlternate;
                bindingIndex = 0;
                break;
            case Bindings.MoveUp:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 1;
                break;
            case Bindings.MoveDown:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 2;
                break;
            case Bindings.MoveLeft:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 3;
                break;
            case Bindings.MoveRight:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 4;
                break;

        }


        inputAction.PerformInteractiveRebinding(bindingIndex).OnComplete(callback => {
            callback.Dispose();
            playerInputActions.Player.Enable();
            action();
            PlayerPrefs.SetString(BINDING_SCHEME, playerInputActions.SaveBindingOverridesAsJson());
        }).Start();
    }

}
