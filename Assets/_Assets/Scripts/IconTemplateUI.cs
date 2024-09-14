using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class IconTemplateUI : MonoBehaviour
{
    [SerializeField] private Image image;
    public void SetKitchenObjectSOImage(KitchenObject_SO kitchenObject_SO) {
        image.sprite = kitchenObject_SO.sprite;
    }
}
