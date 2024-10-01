using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;

public class GameStartCountDownUI : MonoBehaviour
{
    private const string NUMBER_POPUP = "NumberPopUp";
    [SerializeField] private TextMeshProUGUI countDownText;
    
    private Animator animator;

    private int previousCountDown;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        GameHandler.Instance.OnGameStateChanged += Instance_OnGameStateChanged;
        Hide();
    }

    private void Update() {
        int countDown = Mathf.CeilToInt(GameHandler.Instance.GetCountDownTimer());
        countDownText.text = countDown.ToString("#");
        if (countDown != previousCountDown) {
            animator.SetTrigger(NUMBER_POPUP);
            SoundManager.Instance.playSoundCountDown();
            previousCountDown = countDown;
        }
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
