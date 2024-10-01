using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameHandler : MonoBehaviour {
    public static GameHandler Instance { get; private set; }

    public event EventHandler OnGamePaused;
    public event EventHandler OnGameResumed;
    public event EventHandler<OnGameStateChangedEventArgs> OnGameStateChanged;
    public class OnGameStateChangedEventArgs : EventArgs {
        public State state;
    }

    public enum State {
        WaitingForStart,
        CountdownToStart,
        Playing,
        GameOver
    }
    private State state;
    private float countDownTimer = 3f;
    private float playingTimer;
    private float playingTimerMax = 50f;

    private bool isPaused = false;

    private void Awake() {
        Instance = this;
        state = State.WaitingForStart;
    }

    private void Start() {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e) {
        if(state == State.WaitingForStart) {
            state = State.CountdownToStart;
            OnGameStateChanged?.Invoke(this, new OnGameStateChangedEventArgs {
                state = state
            });
        }
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e) {
        if (state == State.Playing)
            ToggleGamePause();
    }

    private void Update() {
        switch (state) {
            case State.WaitingForStart:
                break;
            case State.CountdownToStart:
                countDownTimer -= Time.deltaTime;
                if (countDownTimer < 0f) {
                    state = State.Playing;
                    playingTimer = playingTimerMax;
                    OnGameStateChanged?.Invoke(this, new OnGameStateChangedEventArgs {
                        state = state
                    });
                }
                break;
            case State.Playing:
                playingTimer -= Time.deltaTime;
                if (playingTimer < 0f) {
                    state = State.GameOver;
                    OnGameStateChanged?.Invoke(this, new OnGameStateChangedEventArgs {
                        state = state
                    });
                }
                break;
            case State.GameOver:
                break;
        }
    }
    public bool IsPlaying() {
        return state == State.Playing;
    }
    public float GetCountDownTimer() {
        return countDownTimer;
    }
    public float GetPlayingTimer() {
        return 1 - (playingTimer / playingTimerMax);
    }
    public bool IsGameOver() {
        return state == State.GameOver;
    }

    public void ToggleGamePause() {
        isPaused = !isPaused;
        if (isPaused) {
            Time.timeScale = 0;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else {
            Time.timeScale = 1;
            OnGameResumed?.Invoke(this, EventArgs.Empty);
        }
    }
}
