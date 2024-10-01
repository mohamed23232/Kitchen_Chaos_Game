using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlatesCounter : BaseCounter {

    public event EventHandler OnPlateAdded;
    public event EventHandler OnPlateRemoved;


    [SerializeField] private KitchenObject_SO kitchenPlatesSO;

    private float spwanPlatesTime;
    private float spwanPlatesTimeMax=3f;
    private int platesCount;
    private int platesCountMax = 4;

    public void RemovePlate() {
        OnPlateRemoved?.Invoke(this, EventArgs.Empty);
    }

    public void AddPlate() {
        OnPlateAdded?.Invoke(this, EventArgs.Empty);
    }

    public override void Interact(PlayerScript player) {
        if (!player.HasKitchenObject()) {
            //player has no kitchen object
            if (platesCount > 0) {
                platesCount--;
                KitchenObject.CreateKitchenObject(kitchenPlatesSO, player);
                RemovePlate();
            }
        }
    }

    void Update() { 
        spwanPlatesTime += Time.deltaTime;
        if(spwanPlatesTime > spwanPlatesTimeMax) {
            spwanPlatesTime = 0f;
            if(platesCount < platesCountMax && GameHandler.Instance.IsPlaying()) {
                platesCount++;
                AddPlate();
            }
        }


    }
}
