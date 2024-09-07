using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabObject;

    [SerializeField] private KitchenObject_SO KitchenObjectSO;


    public void Grab(PlayerScript player) {
        OnPlayerGrabObject?.Invoke(this, EventArgs.Empty);
    }

    public override void Interact(PlayerScript player) {
        if (!player.HasKitchenObject()) {
            //player has no object
            Transform KitchenObjectSOTransform = Instantiate(KitchenObjectSO.prefab);
            KitchenObjectSOTransform.GetComponent<KitchenObject>().SetKitchenObjectHolder(player);
            Grab(player);
        }
    }
    
}
