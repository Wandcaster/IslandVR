using OpenCvSharp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlopeBoatController : MonoBehaviour
{
    [SerializeField]
    private Transform toggle;
    float currentRotation;
    [SerializeField]
    float rotationStrengh;
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentRotation == toggle.localEulerAngles.x) return;
        currentRotation = toggle.localEulerAngles.x;
        if (currentRotation > 45) currentRotation -= 360;
        if (Between(currentRotation, -0.1F, 0.1F)) currentRotation = 0;
        Vector3 newRotation=new Vector3(Mathf.LerpAngle(transform.localRotation.eulerAngles.x,currentRotation,rotationStrengh), transform.localRotation.eulerAngles.y,0);
        transform.localRotation=Quaternion.Euler(newRotation);
    }
    public bool Between(float number, float min, float max)
    {
        return number >= min && number <= max;
    }
}
