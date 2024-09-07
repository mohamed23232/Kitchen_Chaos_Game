using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualGameObject;
    private void Start() {
        PlayerScript.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, PlayerScript.OnSelectedCounterChangedEventArgs e) {
        if(e.SelectedCounter == baseCounter) {
            Show();
        }
        else {
            Hide();
        }
    }

    private void Show() {
        foreach (var visualGameObject in visualGameObject) {
            visualGameObject.SetActive(true);
        }
    }
    private void Hide() {
        foreach (var visualGameObject in visualGameObject) {
            visualGameObject.SetActive(false);
        }
    }
}
