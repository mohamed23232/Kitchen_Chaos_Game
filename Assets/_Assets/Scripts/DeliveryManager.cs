using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using System;
public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawn;
    public event EventHandler OnRecipeDelivered;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFail;

    [SerializeField] private ALLRecipesSO allRecipes;
    

    public static DeliveryManager Instance { get; private set; }

    private List<RecipeSO> WaitingrecipesList;

    private float SpawnRecipeTimer;
    private float SpawnRecipeTimerMax = 4f;
    
    private int numberOfRecipesDelivered = 0;
    
    int maxWaitingRecipes = 5;

    private void Awake() {
        WaitingrecipesList = new List<RecipeSO>();
        Instance = this;
    }
    private void Start() {
        SpawnRecipeTimer = SpawnRecipeTimerMax;
    }
    private void Update() {
        SpawnRecipeTimer -= Time.deltaTime;
        if (SpawnRecipeTimer <= 0f) {
            SpawnRecipeTimer = SpawnRecipeTimerMax;
            if (WaitingrecipesList.Count < maxWaitingRecipes)
                SpawnRecipe();
        }
    }

    private void RecipeSpwan() {
        OnRecipeSpawn?.Invoke(this, EventArgs.Empty);
    }
    private void RecipeDelivered() {
        OnRecipeDelivered?.Invoke(this, EventArgs.Empty);
    }
    private void RecipeSuccess() {
        OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
    }
    private void RecipeFail() {
        OnRecipeFail?.Invoke(this, EventArgs.Empty);
    }


    private void SpawnRecipe() {
        RecipeSO recipeToGo = allRecipes.AllRecipesList[UnityEngine.Random.Range(0, allRecipes.AllRecipesList.Count)];
        WaitingrecipesList.Add(recipeToGo);
        RecipeSpwan();
    }

    public bool Delivery(PlateKitchenObject plateKitchenObject) {
        for (int i =0;i< WaitingrecipesList.Count; i++) {
            RecipeSO recipeSO = WaitingrecipesList[i];
            if(plateKitchenObject.GetKitchenObject_SOs().Count == recipeSO.Objects.Count) {
                bool plateContainsAllObjects = true;
                foreach (KitchenObject_SO kitchenObject_SO in recipeSO.Objects) {
                    bool found = false;
                    foreach (KitchenObject_SO plateKitchenObjectSo in plateKitchenObject.GetKitchenObject_SOs()) {
                        if(plateKitchenObjectSo == kitchenObject_SO) {
                            found = true;
                            break;
                        }
                    }
                    if (!found) {
                        plateContainsAllObjects = false;
                    }
                }
                if (plateContainsAllObjects) {
                    WaitingrecipesList.RemoveAt(i);
                    RecipeDelivered();
                    RecipeSuccess();
                    numberOfRecipesDelivered++;
                    return true;
                }
            }
        }
        RecipeFail();
        return false;
    }

    public List<RecipeSO> GetWaitingRecipesList() {
        return WaitingrecipesList;
    }
    public int GetNumberOfRecipesDelivered() {
        return numberOfRecipesDelivered;
    }
}
