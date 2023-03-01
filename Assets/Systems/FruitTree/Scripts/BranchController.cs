using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchController : MonoBehaviour
{
    [SerializeField]
    PlantContainerController root;
    private void OnParticleCollision(GameObject other)
    {
        root.OnParticleCollision(gameObject);
    }
}
