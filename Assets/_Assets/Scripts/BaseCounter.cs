using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCounter : MonoBehaviour, IKitchenObjectHolder {

    [SerializeField] private Transform TopPoint;


    private KitchenObject KitchenObject;

    public abstract void Interact(PlayerScript player);
    public Transform GetHoldingPoint() {
        return TopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.KitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject() {
        return this.KitchenObject;
    }

    public void ClearKitchenObject() {
        this.KitchenObject = null;
    }

    public bool HasKitchenObject() {
        return this.KitchenObject != null;
    }
}
