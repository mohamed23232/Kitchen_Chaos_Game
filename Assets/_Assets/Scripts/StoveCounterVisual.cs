using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private GameObject StoveOn;
    [SerializeField] private GameObject ParticlesOn;

    private void Start() {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e) {
        bool showVisual = e.state == StoveCounter.State.Cooking || e.state == StoveCounter.State.Cooked || e.state == StoveCounter.State.Burned;
        StoveOn.SetActive(showVisual);
        ParticlesOn.SetActive(showVisual);
    }
}
