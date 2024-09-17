using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class TrashCounter : BaseCounter {

    public static event EventHandler OnTrash;

    public override void Interact(PlayerScript player) {
        if (player.HasKitchenObject()) {
            //player has an object
            player.GetKitchenObject().Destroy();
            OnTrash?.Invoke(this, EventArgs.Empty);
        }
        else {
            //player has no object
        }
    }
}
