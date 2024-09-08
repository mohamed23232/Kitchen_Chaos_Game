using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObject_SO KitchenObjectSO;

    private IKitchenObjectHolder KitchenObjectHolder;


    

    public KitchenObject_SO GetKitchenObjectSO() {
        return KitchenObjectSO;
    }

    public void SetKitchenObjectHolder(IKitchenObjectHolder KitchenObjectHolder) {
        if (this.KitchenObjectHolder != null) {
            this.KitchenObjectHolder.ClearKitchenObject();
        }
        this.KitchenObjectHolder = KitchenObjectHolder;
        if(KitchenObjectHolder.HasKitchenObject()) {
            Debug.LogError("Holder already has a KitchenObject");
        }
        KitchenObjectHolder.SetKitchenObject(this);

        transform.parent = KitchenObjectHolder.GetHoldingPoint();
        transform.localPosition = Vector3.zero;
    }
    public IKitchenObjectHolder GetKitchenObjectHolder() {
        return KitchenObjectHolder;
    }

    public void Destroy() {
        KitchenObjectHolder.ClearKitchenObject();
        Destroy(gameObject);
    }

    public static KitchenObject CreateKitchenObject(KitchenObject_SO KitchenObjectSO,IKitchenObjectHolder kitchenObjectHolder) {
        Transform KitchenObjectSOTransform = Instantiate(KitchenObjectSO.prefab);
        KitchenObject kitchenObject = KitchenObjectSOTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectHolder(kitchenObjectHolder);
        return kitchenObject;
    }
}
