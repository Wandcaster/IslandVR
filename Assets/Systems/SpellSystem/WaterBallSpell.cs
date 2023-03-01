using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class WaterBallSpell : MonoBehaviour,ISpell
{
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    string tagName;
    [SerializeField]
    Rigidbody waterBall;
    [SerializeField]
    float waterLosse=100;

    [Header("Levitation settings")]
    public float breakingMultiply; // Adjust this value to control the average upward force
    public float handMultiplier;
    [SerializeField] private SteamVR_Action_Single spellForce;
    [SerializeField] private SteamVR_Action_Single waterControl;

    Transform attachTransform;
    ParticleSystem.EmissionModule emission;
    ParticleSystem.ShapeModule shape;
    float startRadius;
    Vector3 startScale;
    bool isWater;
    Player player;

    private void Start()
    {
        player = Player.instance;
    }
    public void Cast(Transform attachTransform)
    {
        this.attachTransform = attachTransform;
        RaycastHit hit;
        Ray ray = new Ray(attachTransform.position, attachTransform.forward);
        Physics.Raycast(ray,out hit);
        Debug.DrawRay(ray.origin,ray.direction,Color.red,1000);
        if (waterBall != null) Destroy(waterBall.gameObject);
        if (hit.transform==null||!hit.transform.CompareTag(tagName)) return;
        waterBall=Instantiate(prefab, hit.collider.bounds.center,Quaternion.identity).GetComponent<Rigidbody>();
        emission = waterBall.GetComponent<ParticleSystem>().emission;
        emission.rateOverTime = 0;
        shape= waterBall.GetComponent<ParticleSystem>().shape;
        startRadius = shape.radius;
        startScale = waterBall.transform.localScale;
    }
    void FixedUpdate()
    {
        if (waterBall == null) return;

        if (waterControl.axis>0 && !isWater) 
        {
            Debug.Log("StartWater");
            emission.rateOverTime = 56.84F;
            isWater= true;
        }
        else if(waterControl.axis == 0 &&isWater)
        {
            Debug.Log("StopWater");
            emission.rateOverTime = 0;
            isWater= false;
        }
        if(isWater)
        {
            Vector3 newScale = waterBall.transform.localScale - startScale/ waterLosse;
            if (newScale.y <= 0) Destroy(waterBall.gameObject);
            waterBall.transform.localScale = newScale;
            shape.radius -= startRadius / waterLosse;
        }

        Vector3 forceUp = Physics.gravity;
        Vector3 rateOfChangePosition = RateOfChangePosition();
        Vector3 forceFromMovment = rateOfChangePosition * spellForce.axis * handMultiplier;
        if (spellForce.axis > 0.5F)
        {
            waterBall.AddForce(forceFromMovment- forceUp);
        }
        else
        {
            Vector3 brakingForce = -waterBall.velocity;
            waterBall.AddForce(brakingForce - forceUp);
        }
        
    }

    private Vector3 previousPosition;
    private Vector3 currentPosition;

    private Vector3 previousPlayerPos;
    private Quaternion previousPlayerRotation;
    public Vector3 RateOfChangePosition()
    {
        previousPosition = currentPosition;
        currentPosition = attachTransform.position;
        if (previousPlayerPos != player.transform.position||previousPlayerRotation!=player.transform.rotation) 
        {
            previousPlayerPos = player.transform.position;
            previousPlayerRotation = player.transform.rotation;
            previousPosition = attachTransform.position;
            currentPosition = attachTransform.position;
        }
            return (currentPosition - previousPosition) / Time.deltaTime;
    }
}
