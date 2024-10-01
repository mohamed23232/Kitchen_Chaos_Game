using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI MoveUP;
    [SerializeField] private TextMeshProUGUI MoveDown;
    [SerializeField] private TextMeshProUGUI MoveLeft;
    [SerializeField] private TextMeshProUGUI MoveRight;
    [SerializeField] private TextMeshProUGUI Interact;
    [SerializeField] private TextMeshProUGUI InteractAlternative;
    [SerializeField] private TextMeshProUGUI Pause;
    [SerializeField] private TextMeshProUGUI GamePadMove;
    [SerializeField] private TextMeshProUGUI GamePadInteract;
    [SerializeField] private TextMeshProUGUI GamePadInteractAlternative;
    [SerializeField] private TextMeshProUGUI GamePadPause;

    private void Start() {
        GameInput.Instance.OnRebindAction += GameInput_OnRebindAction;
        GameHandler.Instance.OnGameStateChanged += GameHandler_OnGameStateChanged;
        UpdateVisuals();
        Show();
    }

    private void GameHandler_OnGameStateChanged(object sender, GameHandler.OnGameStateChangedEventArgs e) {
        if (e.state != GameHandler.State.WaitingForStart) {
            Hide();
        }
    }

    private void GameInput_OnRebindAction(object sender, System.EventArgs e) {
        UpdateVisuals();
    }

    private void UpdateVisuals() {
        MoveUP.text = GameInput.Instance.GetBindingText(GameInput.Bindings.MoveUp);
        MoveDown.text = GameInput.Instance.GetBindingText(GameInput.Bindings.MoveDown);
        MoveLeft.text = GameInput.Instance.GetBindingText(GameInput.Bindings.MoveLeft);
        MoveRight.text = GameInput.Instance.GetBindingText(GameInput.Bindings.MoveRight);
        Interact.text = GameInput.Instance.GetBindingText(GameInput.Bindings.Interact);
        InteractAlternative.text = GameInput.Instance.GetBindingText(GameInput.Bindings.InteractAlternate);
        Pause.text = GameInput.Instance.GetBindingText(GameInput.Bindings.Pause);
        GamePadMove.text = GameInput.Instance.GetBindingText(GameInput.Bindings.GamePadMove);
        GamePadInteract.text = GameInput.Instance.GetBindingText(GameInput.Bindings.GamePadInteract);
        GamePadInteractAlternative.text = GameInput.Instance.GetBindingText(GameInput.Bindings.GamePadInteractAlternate);
        GamePadPause.text = GameInput.Instance.GetBindingText(GameInput.Bindings.GamePadPause);
    }

    private void Show() {
        gameObject.SetActive(true);
    }
    private void Hide() {
        gameObject.SetActive(false);
    }


}
