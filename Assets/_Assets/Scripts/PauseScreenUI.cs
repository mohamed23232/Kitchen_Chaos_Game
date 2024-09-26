using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PauseScreenUI : MonoBehaviour
{
    [SerializeField] private Button MainMenuButton;
    [SerializeField] private Button ResumeButton;


    private void Awake() {
        MainMenuButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
        ResumeButton.onClick.AddListener(() => {
            GameHandler.Instance.ToggleGamePause();
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

    private void Show() {
        gameObject.SetActive(true);
    }
    private void Hide() {
        gameObject.SetActive(false);
    }
}
