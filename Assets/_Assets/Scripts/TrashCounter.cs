using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter {
    public override void Interact(PlayerScript player) {
        if (player.HasKitchenObject()) {
            //player has an object
            player.GetKitchenObject().Destroy();
        }
        else {
            //player has no object
        }
    }
}
