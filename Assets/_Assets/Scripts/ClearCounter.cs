using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClearCounter : MonoBehaviour,IKitchenObjectHolder
{
    [SerializeField] private KitchenObject_SO KitchenObjectSO;
    [SerializeField] private Transform TopPoint;


    private KitchenObject KitchenObject;

    

    public void Interact(PlayerScript player) {
        if (KitchenObject == null) {
            Transform KitchenObjectSOTransform = Instantiate(KitchenObjectSO.prefab, TopPoint);
            KitchenObjectSOTransform.GetComponent<KitchenObject>().SetKitchenObjectHolder(this);
        }
        else {
            KitchenObject.SetKitchenObjectHolder(player);
        }
    }

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
