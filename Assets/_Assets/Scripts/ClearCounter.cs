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
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                        GetKitchenObject().Destroy();
                    }
                }
                else
                {
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject)) {
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO())) {
                            player.GetKitchenObject().Destroy();
                        }
                    }

                }
                //player has an object
            }
            else {
                //player has no object
                this.GetKitchenObject().SetKitchenObjectHolder(player);
            }
        }

    }

}
