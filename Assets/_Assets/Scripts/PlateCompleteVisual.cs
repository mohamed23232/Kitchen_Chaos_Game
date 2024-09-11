using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObject_SO_GameObject {
        public KitchenObject_SO kitchenObject_SO;
        public GameObject gameObject;
    }


    [SerializeField] private PlateKitchenObject PlateKitchenObject;
    [SerializeField] private List<KitchenObject_SO_GameObject> kitchenObject_SO_GameObjectsList;

    public void Start() {
        PlateKitchenObject.OnPlateComplete += PlateKitchenObject_OnPlateComplete;
        foreach(KitchenObject_SO_GameObject kitchenObject_SO_GameObject in kitchenObject_SO_GameObjectsList) {
            kitchenObject_SO_GameObject.gameObject.SetActive(false);
        }
    }

    private void PlateKitchenObject_OnPlateComplete(object sender, PlateKitchenObject.OnPlateCompleteEventArgs e) {
        foreach(KitchenObject_SO_GameObject kitchenObject_SO_GameObject in kitchenObject_SO_GameObjectsList) {
            if (kitchenObject_SO_GameObject.kitchenObject_SO == e.kitchenObject_SO) {
                kitchenObject_SO_GameObject.gameObject.SetActive(true);
            }
        }
        
    }
}
