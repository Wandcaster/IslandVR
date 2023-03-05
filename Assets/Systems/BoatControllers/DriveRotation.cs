using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveRotation : MonoBehaviour
{
    [SerializeField]
    BoatAccelerationController accelerationController;
    [SerializeField]
    private float velocityMutipler;

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position, transform.forward, accelerationController.currentAcceleration * Time.deltaTime * velocityMutipler);
    }
}
