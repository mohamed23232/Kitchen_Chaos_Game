using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObject_SO KitchenObjectSO;

    

    public override void Interact(PlayerScript player) {
        if (!HasKitchenObject()) {
            //there is no object in the counter
            if (player.HasKitchenObject()) {
                //player has an object
                player.GetKitchenObject().SetKitchenObjectHolder(this);
            }
            else {
                //player has no object
            }
        }
        else {
            //there is an object in the counter
            if (player.HasKitchenObject()) {
                //player has an object
            }
            else {
                //player has no object
                this.GetKitchenObject().SetKitchenObjectHolder(player);
            }
        }

    }

}
