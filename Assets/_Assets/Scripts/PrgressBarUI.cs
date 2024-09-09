using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Rendering.CameraUI;
using UnityEngine.Windows;
public class PrgressBarUI : MonoBehaviour
{
    [SerializeField] private CuttingCounter cuttingCounter;
    [SerializeField] private Image progressBar;

    public void Start() {
        cuttingCounter.OnprogressBarChange += CuttingCounter_OnprogressBarChange;
        progressBar.fillAmount = 0f;
        hide();
    }

    private void CuttingCounter_OnprogressBarChange(object sender, CuttingCounter.ProgressBarEventArgs e) {
        progressBar.fillAmount = e.progress;
        if (progressBar.fillAmount == 0f || progressBar.fillAmount == 1f) {
            hide();
        }
        else {
            show();
        }
    }

    private void show() {
        gameObject.SetActive(true);
    }
    private void hide() {
        gameObject.SetActive(false);
    }
}
