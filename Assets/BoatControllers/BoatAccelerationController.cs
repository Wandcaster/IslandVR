using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatAccelerationController : MonoBehaviour
{
    private Rigidbody rigid;
    [SerializeField]
    private Transform toggle;
    [SerializeField]
    ForceMode forceMode;
    [SerializeField]
    float acceleration;
    float currentAcceleration;
    void Start()
    {
        rigid= GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void Update()
    {
        currentAcceleration = toggle.localRotation.eulerAngles.z;
        if (currentAcceleration > 45) currentAcceleration -= 360;
        if (Between(currentAcceleration, -0.1F, 0.1F)) currentAcceleration = 0;
        rigid.AddForce(transform.forward * currentAcceleration*Time.deltaTime*acceleration, forceMode);
    }
    public bool Between(float number, float min, float max)
    {
        return number >= min && number <= max;
    }
}
