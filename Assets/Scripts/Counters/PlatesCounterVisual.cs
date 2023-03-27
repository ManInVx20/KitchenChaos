using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField]
    private PlatesCounter platesCounter;
    [SerializeField]
    private Transform counterTopPoint;
    [SerializeField]
    private GameObject plateVisualPrefab;

    private List<GameObject> plateVisualGameObjectList = new List<GameObject>();

    private void Start()
    {
        platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
        platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
    }

    private void PlatesCounter_OnPlateSpawned(object sender, EventArgs args)
    {
        GameObject plateVisualGameObject = Instantiate(plateVisualPrefab, counterTopPoint);

        float plateOffsetY = 0.1f;
        plateVisualGameObject.transform.localPosition = new Vector3(0.0f, plateOffsetY * plateVisualGameObjectList.Count, 0.0f);

        plateVisualGameObjectList.Add(plateVisualGameObject);
    }

    private void PlatesCounter_OnPlateRemoved(object sender, EventArgs args)
    {
        GameObject plateVisualGameObject = plateVisualGameObjectList[plateVisualGameObjectList.Count - 1];
        plateVisualGameObjectList.Remove(plateVisualGameObject);
        Destroy(plateVisualGameObject);
    }
}
