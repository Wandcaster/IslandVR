using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetableBoxController : PlantContainerController
{
    [SerializeField]
    private float regrowCheckWaitTime;
    public List<SeedController> seeds = new List<SeedController>();
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(autoRegrow());
    }

    public IEnumerator autoRegrow()
    {
        SpawnSpot spawnSpot;
        while (true)
        {
            yield return new WaitForSeconds(regrowCheckWaitTime);
            
                for (int i = 0; i < spawnSpots.Count; i++)
                {
                    spawnSpot = spawnSpots[i];
                    if (spawnSpot.plant == null&&seeds.Count!=0)
                    {
                        SeedController randomSeed = seeds[Random.Range(0, seeds.Count)];
                        spawnSpot.plant = Instantiate(randomSeed.plantPrefab, transform)
                            .GetComponent<Plant>();
                        seeds.Remove(randomSeed);
                        Destroy(randomSeed.gameObject);
                        spawnSpot.plant.transform.localPosition = spawnSpot.spot;
                        spawnSpot.plant.transform.localRotation = Quaternion.identity;

                        spawnSpot.plant.root = this;
                        spawnSpots.Remove(spawnSpots[i]);
                        spawnSpots.Add(spawnSpot);

                    }
                
            }
            yield return true;
        }
    }
}
