using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private enum Mode {
        Lookforward,
        LookforwardInvereted,
        LookAt,
        LookAtInvereted,
    }
    [SerializeField] private Mode modes;


    private void LateUpdate() {
        switch(modes) {
            case Mode.Lookforward:
                transform.forward = Camera.main.transform.forward;
                break;
            case Mode.LookforwardInvereted:
                transform.forward = -Camera.main.transform.forward;
                break;
            case Mode.LookAt:
                transform.LookAt(Camera.main.transform);
                break;
            case Mode.LookAtInvereted:
                transform.LookAt(Camera.main.transform);
                transform.Rotate(0, 180, 0);

                //-------- Another way to do it --------
                //Vector3 oppositeDir = transform.position - Camera.main.transform.position;
                //transform.LookAt(transform.position + oppositeDir);
                break;
        }
    }
}
