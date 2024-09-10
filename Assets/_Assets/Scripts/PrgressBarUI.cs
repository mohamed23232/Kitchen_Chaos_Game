using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Rendering.CameraUI;
using UnityEngine.Windows;
public class PrgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject Counter;
    [SerializeField] private Image progressBar;

    private IHasProgress hasProgress;

    public void Start() {
        hasProgress = Counter.GetComponent<IHasProgress>();
        hasProgress.OnprogressBarChange += HasProgress_OnprogressBarChange;
        progressBar.fillAmount = 0f;
        hide();
    }

    private void HasProgress_OnprogressBarChange(object sender, IHasProgress.ProgressBarEventArgs e) {
        progressBar.fillAmount = e.progress;
        progressBar.color = e.color;
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
