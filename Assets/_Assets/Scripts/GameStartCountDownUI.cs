using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;

public class GameStartCountDownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countDownText;
    
    private void Start() {
        GameHandler.Instance.OnGameStateChanged += Instance_OnGameStateChanged;
        Hide();
    }

    private void Update() {
        countDownText.text = GameHandler.Instance.GetCountDownTimer().ToString("#");
    }

    private void Instance_OnGameStateChanged(object sender, GameHandler.OnGameStateChangedEventArgs e) {
        if(e.state == GameHandler.State.CountdownToStart) {
            Show();
        }
        else {
            Hide();
        }
    }
    private void Show() {
        gameObject.SetActive(true);
    }
    private void Hide() {
        gameObject.SetActive(false);
    }
}
