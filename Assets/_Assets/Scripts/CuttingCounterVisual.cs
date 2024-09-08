using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    Animator animator;

    string CUT = "Cut";

    [SerializeField] private CuttingCounter cuttingCounter;
    public void Awake() {
        animator = GetComponent<Animator>();
    }
    public void Start() {
        cuttingCounter.OnplayerCutObject += CuttingCounter_OnplayerCutObject; ;
    }

    private void CuttingCounter_OnplayerCutObject(object sender, System.EventArgs e) {
        animator.SetTrigger(CUT);
    }
}

