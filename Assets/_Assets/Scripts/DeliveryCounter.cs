using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public static DeliveryCounter Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }

    public override void Interact(PlayerScript player) {
        if (player.HasKitchenObject()) {
            //player has an object
            if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                //only plate can be delivered
                if (DeliveryManager.Instance.Delivery(plateKitchenObject)) {
                    //delivered
                    player.GetKitchenObject().Destroy();
                }
            }
        }
    }

}
