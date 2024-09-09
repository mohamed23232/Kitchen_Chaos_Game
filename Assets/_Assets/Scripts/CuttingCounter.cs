using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Windows;

public class CuttingCounter : BaseCounter {
    [SerializeField] private KitchenRecipeSO[] cuttingRecipes;

    public event EventHandler OnplayerCutObject;
    public event EventHandler<ProgressBarEventArgs> OnprogressBarChange;
    public class ProgressBarEventArgs : EventArgs {
        public float progress;
    }



    private int cut_cnt = 0;
    private bool cutCompleted = false;
    public void Cut(PlayerScript player) {
        OnplayerCutObject?.Invoke(this, EventArgs.Empty);
    }

    public void ProgressBarChange() {
        OnprogressBarChange?.Invoke(this, new ProgressBarEventArgs {
            progress = (float)cut_cnt / GetRecipe(GetKitchenObject().GetKitchenObjectSO()).maxCutCounter
        });
    }


    public override void Interact(PlayerScript player) {
        if (!HasKitchenObject()) {
            //there is no object in the counter
            if (player.HasKitchenObject()) {
                //player has an object
                if (ObjectIsCuttable(player.GetKitchenObject().GetKitchenObjectSO())) {
                    player.GetKitchenObject().SetKitchenObjectHolder(this);
                    cut_cnt = 0;
                    cutCompleted = false;
                    ProgressBarChange();
                }
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
                if(cutCompleted || cut_cnt==0)
                this.GetKitchenObject().SetKitchenObjectHolder(player);
            }
        }

    }
    public override void InteractAlternate(PlayerScript player) {
        if (HasKitchenObject() && ObjectIsCuttable(GetKitchenObject().GetKitchenObjectSO())) {
            KitchenObject_SO input = GetKitchenObject().GetKitchenObjectSO();
            KitchenObject_SO output = GetRecipe(input).output;
            cut_cnt++;
            Cut(player);
            ProgressBarChange();
            if (cut_cnt >= GetRecipe(input).maxCutCounter) {
                cutCompleted = true;
                GetKitchenObject().Destroy();
                KitchenObject.CreateKitchenObject(output, this);
            }
        }

    }

    public bool ObjectIsCuttable(KitchenObject_SO inputObject) {
        return GetRecipe(inputObject) != null;
    }

    public KitchenRecipeSO GetRecipe(KitchenObject_SO input) {
        foreach (KitchenRecipeSO recipe in cuttingRecipes) {
            if (recipe.input == input) {
                return recipe;
            }
        }
        return null;
    }

}