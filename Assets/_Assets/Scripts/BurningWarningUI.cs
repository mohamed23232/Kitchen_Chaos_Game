using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
public class BurningWarningUI : MonoBehaviour
{
    [SerializeField] private GameObject stoveCounter;

    private IHasWarning hasWarning;
    private void Start() {
        hasWarning = stoveCounter.GetComponent<IHasWarning>();
        hasWarning.OnWarning += HasWarning_OnWarning;
        Hide();
    }

    private void HasWarning_OnWarning(object sender, IHasWarning. OnWarningEventArgs e) {
        Debug.Log(e.Empty);
        if (e.state == StoveCounter.State.Cooked) {
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
