using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class BoatRotationController : MonoBehaviour
{
    [SerializeField]
    Transform circle;
    [SerializeField]
    float rotationStrengh;
    [SerializeField]
    private Quaternion lastRotation;

    void Update()
    {
        if (lastRotation == circle.transform.localRotation) return;
        float angle =Quaternion.Angle(circle.transform.localRotation, lastRotation);
        if(circle.transform.localRotation.eulerAngles.z < lastRotation.eulerAngles.z) angle= -angle;
        transform.RotateAround(circle.position,Vector3.up, angle*rotationStrengh);
        lastRotation = circle.transform.localRotation;
    }
}
