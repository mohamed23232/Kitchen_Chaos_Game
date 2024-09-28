using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }

    [SerializeField] private Button SoundEffectsButton;
    [SerializeField] private Button MusicButton;
    [SerializeField] private Button CloseButton;
    [SerializeField] private Button MoveUpButton;
    [SerializeField] private Button MoveDownButton;
    [SerializeField] private Button MoveLeftButton;
    [SerializeField] private Button MoveRightButton;
    [SerializeField] private Button InteractButton;
    [SerializeField] private Button InteractAlternativeButton;
    [SerializeField] private TextMeshProUGUI SoundEffectsText;
    [SerializeField] private TextMeshProUGUI MusicText;
    [SerializeField] private TextMeshProUGUI MoveUpText;
    [SerializeField] private TextMeshProUGUI MoveDownText;
    [SerializeField] private TextMeshProUGUI MoveLeftText;
    [SerializeField] private TextMeshProUGUI MoveRightText;
    [SerializeField] private TextMeshProUGUI InteractText;
    [SerializeField] private TextMeshProUGUI InteractAlternativeText;

    [SerializeField] private Transform PressAnyKey;


    private void Awake() {
        Instance = this;
        SoundEffectsButton.onClick.AddListener(() => {
            SoundManager.Instance.ChangeVolume();

            ChangeUI();

        });

        MusicButton.onClick.AddListener(() => {
            MusicManager.Instance.ChangeVolume();

            ChangeUI();
        });
        CloseButton.onClick.AddListener(() => {
            Hide();
            PauseScreenUI.Instance.Show();
        });
        MoveUpButton.onClick.AddListener(() => {
            RebindKeys(GameInput.Bindings.MoveUp);
        });
        MoveDownButton.onClick.AddListener(() => {
            RebindKeys(GameInput.Bindings.MoveDown);
        });
        MoveLeftButton.onClick.AddListener(() => {
            RebindKeys(GameInput.Bindings.MoveLeft);
        });
        MoveRightButton.onClick.AddListener(() => {
            RebindKeys(GameInput.Bindings.MoveRight);
        });
        InteractButton.onClick.AddListener(() => {
            RebindKeys(GameInput.Bindings.Interact);
        });
        InteractAlternativeButton.onClick.AddListener(() => {
            RebindKeys(GameInput.Bindings.InteractAlternate);
        });
    }
    private void Start() {
        GameHandler.Instance.OnGamePaused += Instance_OnGamePaused;
        GameHandler.Instance.OnGameResumed += Instance_OnGameResumed;
        ChangeUI();
        Hide();
    }

    private void Instance_OnGameResumed(object sender, System.EventArgs e) {
        Hide();
    }

    private void Instance_OnGamePaused(object sender, System.EventArgs e) {
        Hide();
    }

    private void ChangeUI() {
        SoundEffectsText.text = "Sound Effects: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        MusicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);

        MoveUpText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.MoveUp);
        MoveDownText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.MoveDown);
        MoveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.MoveLeft);
        MoveRightText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.MoveRight);
        InteractText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.Interact);
        InteractAlternativeText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.InteractAlternate);
    }

    public void Show() { gameObject.SetActive(true); }
    private void Hide() { gameObject.SetActive(false); }

    private void ShowPressAnyKey() {
        PressAnyKey.gameObject.SetActive(true);
    }
    private void HidePressAnyKey() {
        PressAnyKey.gameObject.SetActive(false);
    }

    private void RebindKeys(GameInput.Bindings binding) {
        ShowPressAnyKey();
        GameInput.Instance.RebindBinding(binding, () => {
            HidePressAnyKey();
            ChangeUI();
        });

    }


}
