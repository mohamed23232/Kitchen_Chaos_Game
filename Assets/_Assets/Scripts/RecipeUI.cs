using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeUI : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI recipeName;
    [SerializeField] private Transform container;
    [SerializeField] private Transform template;

    private void Awake() {
        template.gameObject.SetActive(false);
    }

    public void SetRecipe(RecipeSO recipeSO) {
        recipeName.text = recipeSO.RecipeName;

        foreach (Transform child in container) {
            if (child == template) {
                continue;
            }
            Destroy(child.gameObject);
        }
        foreach (KitchenObject_SO kitchenObject_SO in recipeSO.Objects) {
            Transform iconTransform = Instantiate(template, container);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<Image>().sprite = kitchenObject_SO.sprite;
        }
    }
}
