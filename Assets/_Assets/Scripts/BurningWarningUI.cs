using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
public class BurningWarningUI : MonoBehaviour
{
    [SerializeField] private GameObject stoveCounter;

    private IHasWarning hasWarning;
    
    private float warningTime;
    private float warningTimeLimit = 0.5f;
    private void Start() {
        hasWarning = stoveCounter.GetComponent<IHasWarning>();
        hasWarning.OnWarning += HasWarning_OnWarning;
        Hide();
    }

    private void HasWarning_OnWarning(object sender, IHasWarning. OnWarningEventArgs e) {
        if (e.state == StoveCounter.State.Cooked) {
            Show();
            warningTime += Time.deltaTime;
            if (warningTime > warningTimeLimit) {
                warningTime = 0f;
                SoundManager.Instance.PlaySoundWarning(stoveCounter.transform.position);
            }
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
