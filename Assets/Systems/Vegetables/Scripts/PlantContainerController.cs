using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public struct  SpawnSpot
{
    public SpawnSpot(Plant plant,Vector3 spot,BranchController branch)
    {
        this.plant = plant;
        this.spot = spot;
        this.branch = branch;
    }
    public Plant plant;
    public Vector3 spot;
    public BranchController branch;
}

public class PlantContainerController : MonoBehaviour,IWaterable
{
    public List<SpawnSpot> spawnSpots = new List<SpawnSpot>();
    List<Plant> plants = new List<Plant>();
    public GameObject plantPrefab;
    private PlantContainerView plantContainerView;

    [Range(0, 1)]
    public float hydrationLevel;
    public float waterAmountOnSingleParticle;
    [SerializeField]
    private float updateWaitTime;
    [SerializeField]
    private float amountOfWaterReductionPerUpdate;
    void Awake()
    {
        plantContainerView= GetComponent<PlantContainerView>();
        plants = new List<Plant>(GetComponentsInChildren<Plant>());
        foreach (var plant in plants)
        {
            spawnSpots.Add(new SpawnSpot(plant, plant.transform.localPosition, plant.transform.parent.GetComponent<BranchController>()));
            plant.transform.localScale = Vector3.zero;
            plant.root = this;
        }
        StartCoroutine(UpdateStatus());
    }
    IEnumerator UpdateStatus()
    {
        while(true)
        {
            yield return new WaitForSeconds(updateWaitTime);
            hydrationLevel = Mathf.Clamp01(hydrationLevel - amountOfWaterReductionPerUpdate);
            plantContainerView.SetColor(hydrationLevel);
            yield return true;
        }
    }
    public void Water(float amount)
    {
        hydrationLevel = Mathf.Clamp01(hydrationLevel + amount);
        plantContainerView.SetColor(hydrationLevel);
    }
    public void OnParticleCollision(GameObject other)
    {
        Water(waterAmountOnSingleParticle);
    }
}
