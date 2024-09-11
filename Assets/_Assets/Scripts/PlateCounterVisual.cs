using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlateCounterVisual : MonoBehaviour
{

    [SerializeField] private Transform TopPoint;
    [SerializeField] private Transform PlateVisual;
    [SerializeField] private PlatesCounter PlatesCounter;

    private List<GameObject> PlateVisualsCount;

    public void Awake() {
        PlateVisualsCount = new List<GameObject>();
    }


    public void Start() {
        PlatesCounter.OnPlateAdded += PlatesCounter_OnPlateAdded;
        PlatesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
    }

    private void PlatesCounter_OnPlateRemoved(object sender, EventArgs e) {
        GameObject PlateGameObject = PlateVisualsCount[PlateVisualsCount.Count - 1];
        PlateVisualsCount.Remove(PlateGameObject);
        Destroy(PlateGameObject);
    }

    private void PlatesCounter_OnPlateAdded(object sender, EventArgs e) {

        Transform plateTransform = Instantiate(PlateVisual, TopPoint);

        float offsetY = 0.15f;

        plateTransform.localPosition = new Vector3(0, offsetY * PlateVisualsCount.Count, 0);
        PlateVisualsCount.Add(plateTransform.gameObject);
    }

}
