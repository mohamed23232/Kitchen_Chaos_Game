using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public abstract class BaseCounter : MonoBehaviour, IKitchenObjectHolder {

    [SerializeField] private Transform TopPoint;

    public static event EventHandler OnDrop;

    private KitchenObject KitchenObject;

    public abstract void Interact(PlayerScript player);
    public virtual void InteractAlternate(PlayerScript player) { }


    public Transform GetHoldingPoint() {
        return TopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.KitchenObject = kitchenObject;
        if (kitchenObject != null) {
            OnDrop?.Invoke(this, EventArgs.Empty);
        }
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
