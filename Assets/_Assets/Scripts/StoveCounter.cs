using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class StoveCounter : BaseCounter,IHasProgress {

    public event EventHandler<IHasProgress.ProgressBarEventArgs> OnprogressBarChange;
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs : EventArgs {
        public State state;
    }


    [SerializeField] private FryingRecipeSO[] fryingRecipeSOarray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOarray;
    public enum State {
        Empty,
        Cooking,
        Cooked,
        Burned,
    }

    private float fryingTimer = 0;
    private float burningTimer = 0;

    private FryingRecipeSO fryingRecipeSO;
    private BurningRecipeSO burningRecipeSO;

    private State state;

    public void OnState(State state) {
        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
    }

    public void ProgressBarChange(float timer,float maxTimer,Color color) {
        OnprogressBarChange?.Invoke(this, new IHasProgress.ProgressBarEventArgs {
            progress = timer / maxTimer
            , color = color
        });
    }


    private void Start() {
        state = State.Empty;
    }


    private void Update() {
        if (HasKitchenObject()) {
            switch (state) {
                case State.Empty:
                    break;
                case State.Cooking:
                    fryingTimer += Time.deltaTime;
                    ProgressBarChange(fryingTimer, fryingRecipeSO.MaxfryingTimer,Color.green);
                    if (fryingTimer > fryingRecipeSO.MaxfryingTimer) {
                        //frying is done
                        GetKitchenObject().Destroy();
                        KitchenObject.CreateKitchenObject(fryingRecipeSO.output, this);
                        state = State.Cooked;
                        burningTimer = 0f;
                        burningRecipeSO = GetBurningRecipe(GetKitchenObject().GetKitchenObjectSO());
                        OnState(state);
                    }
                    break;
                case State.Cooked:
                    burningTimer += Time.deltaTime;
                    ProgressBarChange(burningTimer,burningRecipeSO.MaxburningTimer,Color.red);
                    if (burningTimer > burningRecipeSO.MaxburningTimer) {
                        //burning is done
                        GetKitchenObject().Destroy();
                        KitchenObject.CreateKitchenObject(burningRecipeSO.output, this);
                        state = State.Burned;
                        OnState(state);
                        ProgressBarChange(0, burningRecipeSO.MaxburningTimer,Color.green);
                    }
                    break;
                case State.Burned:
                    break;
            }
        }
    }

    public override void Interact(PlayerScript player) {
        if (!HasKitchenObject()) {
            //there is no object in the counter
            if (player.HasKitchenObject()) {
                //player has an object
                if (ObjectIsFryable(player.GetKitchenObject().GetKitchenObjectSO())) {
                    player.GetKitchenObject().SetKitchenObjectHolder(this);
                    fryingRecipeSO = GetFryingRecipe(GetKitchenObject().GetKitchenObjectSO());
                    state = State.Cooking;
                    fryingTimer = 0f;
                    ProgressBarChange(fryingTimer, fryingRecipeSO.MaxfryingTimer, Color.green);
                    OnState(state);
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
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                        GetKitchenObject().Destroy();
                    }
                }
                state = State.Empty;
                OnState(state);
                ProgressBarChange(0, burningRecipeSO.MaxburningTimer, Color.green);

            }
            else {
                //player has no object
                this.GetKitchenObject().SetKitchenObjectHolder(player);
                state = State.Empty;
                OnState(state);
                ProgressBarChange(0, burningRecipeSO.MaxburningTimer, Color.green);
            }
        }
    }
    public bool ObjectIsFryable(KitchenObject_SO inputObject) {
        return GetFryingRecipe(inputObject) != null;
    }

    public FryingRecipeSO GetFryingRecipe(KitchenObject_SO input) {
        foreach (FryingRecipeSO recipe in fryingRecipeSOarray) {
            if (recipe.input == input) {
                return recipe;
            }
        }
        return null;
    }
    public BurningRecipeSO GetBurningRecipe(KitchenObject_SO input) {
        foreach (BurningRecipeSO recipe in burningRecipeSOarray) {
            if (recipe.input == input) {
                return recipe;
            }
        }
        return null;
    }

    
}
