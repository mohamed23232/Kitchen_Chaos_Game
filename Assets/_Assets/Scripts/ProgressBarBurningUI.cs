using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBarBurningUI : MonoBehaviour
{
    private const string AnimatorIsFlashing = "isFlashing";
    private Animator animator;

    [SerializeField] private GameObject stoveCounter;

    private IHasWarning hasWarning;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        hasWarning = stoveCounter.GetComponent<IHasWarning>();
        hasWarning.OnWarning += HasWarning_OnWarning;
        animator.SetBool(AnimatorIsFlashing, false);
    }

    private void HasWarning_OnWarning(object sender, IHasWarning.OnWarningEventArgs e) {
        if (e.state == StoveCounter.State.Cooked) {
            animator.SetBool(AnimatorIsFlashing, true);
        }
        else {
            animator.SetBool(AnimatorIsFlashing, false);
        }
    }
}
