using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedCapture : MonoBehaviour
{
    [SerializeField]
    private VegetableBoxController vegetableBoxController;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<SeedController>() == null) return;
        vegetableBoxController.seeds.Add(other.GetComponent<SeedController>());
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<SeedController>() == null) return;
        vegetableBoxController.seeds.Remove(other.GetComponent<SeedController>());
    }
}
