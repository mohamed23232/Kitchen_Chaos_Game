using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterAnimation : MonoBehaviour
{
    private const string GRAB = "OpenClose_Grabbing";
    private Animator animator;

    [SerializeField] private ContainerCounter containerCounter;

    private void Awake() {
        animator = GetComponent<Animator>();
    }
    private void Start() {
        containerCounter.OnPlayerGrabObject += ContainerCounter_OnPlayerGrabObject; ;
    }

    private void ContainerCounter_OnPlayerGrabObject(object sender, System.EventArgs e) {
        animator.SetTrigger(GRAB);
    }
}

