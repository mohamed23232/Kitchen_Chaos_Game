using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconsUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private Transform iconTemplate;

    public void Awake() {
        iconTemplate.gameObject.SetActive(false);
    }

    public void Start() {
        plateKitchenObject.OnPlateComplete += PlateKitchenObject_OnPlateComplete; ;
    }

    private void PlateKitchenObject_OnPlateComplete(object sender, PlateKitchenObject.OnPlateCompleteEventArgs e) {
        IconUI();
    }

    private void IconUI() {
        foreach (Transform child in transform) {
            if (child == iconTemplate) {
                continue;
            }
            Destroy(child.gameObject);
        }


        foreach (KitchenObject_SO kitchenObject_SO in plateKitchenObject.GetKitchenObject_SOs()) {
            Transform iconTransform = Instantiate(iconTemplate, transform);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<IconTemplateUI>().SetKitchenObjectSOImage(kitchenObject_SO);
        }

    }
}
