using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEventsDestroyer : MonoBehaviour
{
    private void Awake() {
        CuttingCounter.ResetCuttingCounter();
        TrashCounter.ResetTrash();
        BaseCounter.ResetDrop();
    }
}
