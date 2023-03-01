using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantContainerView : MonoBehaviour
{
    [SerializeField]
    List<Material> materials;

    public void SetColor(float hydration)
    {
        foreach (var material in materials)
        {
            material.SetFloat("Vector1_4df39215be94416c90310741114feced", hydration);
        }
    }

}