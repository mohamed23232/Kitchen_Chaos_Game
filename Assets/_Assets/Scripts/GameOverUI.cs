using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI DeliveredNumber;
    private void Start() {
        GameHandler.Instance.OnGameStateChanged += Instance_OnGameStateChanged;
        Hide();
    }

    private void Update() {
        DeliveredNumber.text = DeliveryManager.Instance.GetNumberOfRecipesDelivered().ToString();
    }

    private void Instance_OnGameStateChanged(object sender, GameHandler.OnGameStateChangedEventArgs e) {
        if (e.state == GameHandler.State.GameOver) {
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
