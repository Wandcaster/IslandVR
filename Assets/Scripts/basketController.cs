using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basketController : MonoBehaviour
{
    [SerializeField]
    string collectingObjectTag;
    [SerializeField]
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(collectingObjectTag)&&other.GetComponent<FruitController>()!=null&& other.GetComponent<FruitController>().attachedToRoot==false) other.transform.SetParent(transform);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(collectingObjectTag) && other.GetComponent<FruitController>() != null && other.GetComponent<FruitController>().attachedToRoot==false) other.transform.SetParent(null);
    }
}
