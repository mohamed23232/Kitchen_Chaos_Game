using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameHandler : MonoBehaviour {
    public static GameHandler Instance { get; private set; }

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
    private float waitingForStartTime = 1f;
    private float countDownTimer = 3f;
    private float playingTimer = 10f;

    private void Awake() {
        Instance = this;
        state = State.WaitingForStart;
    }

    private void Update() {
        switch (state) {
            case State.WaitingForStart:
                waitingForStartTime -= Time.deltaTime;
                if (waitingForStartTime < 0f) {
                    state = State.CountdownToStart;
                    OnGameStateChanged?.Invoke(this, new OnGameStateChangedEventArgs {
                        state = state
                    });
                }
                break;
            case State.CountdownToStart:
                countDownTimer -= Time.deltaTime;
                if (countDownTimer < 0f) {
                    state = State.Playing;
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
        Debug.Log(state);
    }
    public bool IsPlaying() {
        return state == State.Playing;
    }
    public float GetCountDownTimer() {
        return countDownTimer;
    }
}
