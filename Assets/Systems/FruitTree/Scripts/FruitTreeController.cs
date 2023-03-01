using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.InputSystem;
using System.Linq;

public class FruitTreeController : PlantContainerController
{
    [SerializeField]
    private float regrowCheckWaitTime;
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
                if (spawnSpot.plant == null)
                {
                    spawnSpot.plant = Instantiate(plantPrefab, spawnSpot.branch.transform)
                        .GetComponent<Plant>();
                    spawnSpot.plant.transform.localPosition = spawnSpot.spot;
                    spawnSpot.plant.root = this;
                    spawnSpots.Remove(spawnSpots[i]);
                    spawnSpots.Add(spawnSpot);
                }
            }
            yield return true;
        }
    }
}
