using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform template;

    private void Awake() {
        template.gameObject.SetActive(false);
    }

    private void Start() {
        DeliveryManager.Instance.OnRecipeSpawn += DeliveryManager_OnRecipeSpawn;
        DeliveryManager.Instance.OnRecipeDelivered += DeliveryManager_OnRecipeDelivered;
        DeliveryUI();
    }

    private void DeliveryManager_OnRecipeDelivered(object sender, System.EventArgs e) {
        DeliveryUI();
    }

    private void DeliveryManager_OnRecipeSpawn(object sender, System.EventArgs e) {
        DeliveryUI();
    }

    private void DeliveryUI() {
        foreach (Transform child in container) {
            if (child == template) {
                continue;
            }
            Destroy(child.gameObject);
        }
        foreach (RecipeSO recipeSO in DeliveryManager.Instance.GetWaitingRecipesList()) {
            Transform recipeTransform = Instantiate(template, container);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<RecipeUI>().SetRecipe(recipeSO);
        }
    }
}
