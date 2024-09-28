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
    [SerializeField] private TextMeshProUGUI SoundEffectsText;
    [SerializeField] private TextMeshProUGUI MusicText;
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
    }

    public void Show() { gameObject.SetActive(true); }
    private void Hide() { gameObject.SetActive(false); }

}
