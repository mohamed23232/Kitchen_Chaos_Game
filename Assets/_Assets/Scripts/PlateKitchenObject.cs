using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using UnityEngine.UIElements.Experimental;
public class PlateKitchenObject : KitchenObject {

    public event EventHandler<OnPlateCompleteEventArgs> OnPlateComplete;
    public class OnPlateCompleteEventArgs : EventArgs {
        public KitchenObject_SO kitchenObject_SO;
    }

    [SerializeField] private KitchenObject_SO[] ValidkitchenObject_SO;

    private List<KitchenObject_SO> KitchenObject_SOs;

    public void Awake() {
        KitchenObject_SOs = new List<KitchenObject_SO>();
    }

    public void OnPlateCompleteEvent(KitchenObject_SO kitchenObject_SO) {
        OnPlateComplete?.Invoke(this, new OnPlateCompleteEventArgs { kitchenObject_SO = kitchenObject_SO });
    }


    public bool TryAddIngredient(KitchenObject_SO kitchenObject_SO) {
        if (KitchenObject_SOs.Contains(kitchenObject_SO) || !ValidkitchenObject_SO.Contains(kitchenObject_SO)) {
            return false;
        }
        else {
            KitchenObject_SOs.Add(kitchenObject_SO);
            OnPlateCompleteEvent(kitchenObject_SO);
            return true;
        }
    }

    public List<KitchenObject_SO> GetKitchenObject_SOs() {
        return KitchenObject_SOs;
    }
}