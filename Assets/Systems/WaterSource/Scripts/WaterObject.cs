using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterObject : MonoBehaviour
{
    [SerializeField]
    Vector3 gravity;
    [SerializeField]
    float currentDive;
    [SerializeField]
    float t;
    [SerializeField]
    Transform surfaceHeightTransform;
    private float surfaceHeight;

    [SerializeField]
    private float maxUpVelocity;
    [SerializeField]
    private float minUpVelocity;
    
    
    private void Start()
    {
        surfaceHeight = surfaceHeightTransform.position.y;
    }
    private void OnTriggerStay(Collider other)
    {
        surfaceHeight = surfaceHeightTransform.position.y;
        currentDive = Mathf.Lerp(surfaceHeight, other.transform.position.y, t);
        Vector3 objectVelocity = other.attachedRigidbody.velocity;
        objectVelocity += gravity *currentDive * Time.deltaTime;
        
        objectVelocity.y =Mathf.Clamp(objectVelocity.y,minUpVelocity,maxUpVelocity);
        other.attachedRigidbody.velocity = objectVelocity;
    }
}
