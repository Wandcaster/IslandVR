using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectBallController : MonoBehaviour
{
    [SerializeField]
    List<Plant> plants= new List<Plant>();
    [SerializeField]
    float forceMultipler;

    private void OnTriggerEnter(Collider other)
    {
        TryCollectPlant(other);
    }
    private void OnTriggerExit(Collider other)
    {
        TryUncollectPlant(other,true);
    }
    private void TryCollectPlant(Collider target)
    {
        Plant plant = target.GetComponent<Plant>();
        if (plant == null || !plant.canPickUp) return;
        plant.DeatachFromRoot();
        plant.rigid.useGravity = false;
        plant.rigid.isKinematic = false;
        plants.Add(plant);
    }
    private void TryUncollectPlant(Collider target,bool removeFromList)
    {
        Plant plant = target.GetComponent<Plant>();
        if (plant == null) return;
        plant.rigid.useGravity = true;
        if(removeFromList) plants.Remove(plant);
    }

    private void FixedUpdate()
    {
        foreach (var plant in plants)
        {
            plant.rigid.velocity=(transform.position - plant.transform.position)* forceMultipler;
        }
    }
    private void OnDestroy()
    {
        foreach (var plant in plants)
        {
            TryUncollectPlant(plant.GetComponent<Collider>(),false);
        }
    }
}
