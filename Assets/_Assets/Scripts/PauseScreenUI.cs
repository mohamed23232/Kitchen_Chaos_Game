using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PauseScreenUI : MonoBehaviour
{
    public static PauseScreenUI Instance { get; private set; }

    [SerializeField] private Button MainMenuButton;
    [SerializeField] private Button ResumeButton;
    [SerializeField] private Button OptionsButton;


    private void Awake() {
        Instance = this;
        MainMenuButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
        ResumeButton.onClick.AddListener(() => {
            GameHandler.Instance.ToggleGamePause();
        });
        OptionsButton.onClick.AddListener(() => {
            OptionsUI.Instance.Show(Show);
            Hide();
        });
    }

    private void Start() {
        GameHandler.Instance.OnGamePaused += Instance_OnGamePaused;
        GameHandler.Instance.OnGameResumed += Instance_OnGameResumed;
        Hide();
    }

    private void Instance_OnGameResumed(object sender, System.EventArgs e) {
        Hide();
    }

    private void Instance_OnGamePaused(object sender, System.EventArgs e) {
        Show();
    }

    public void Show() {
        gameObject.SetActive(true);
        ResumeButton.Select();
    }
    private void Hide() {
        gameObject.SetActive(false);
    }
}
