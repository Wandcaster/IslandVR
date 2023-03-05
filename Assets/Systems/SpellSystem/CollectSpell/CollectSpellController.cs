using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class CollectSpellController : MonoBehaviour,ISpell
{
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    Rigidbody windBall;
    [SerializeField]
    Transform sourceTransform;
    [SerializeField]
    float instantiateDistance;

    [Header("Levitation settings")]
    public float breakingMultiply; // Adjust this value to control the average upward force
    public float handMultiplier;
    [SerializeField] private SteamVR_Action_Single moveForce;

    private void Start()
    {
        player = Player.instance;
    }

    public void Cast(Transform sourceTransform)
    {
        this.sourceTransform = sourceTransform;

        if (windBall != null)
        {
            Destroy(windBall.gameObject);
            return;
        }
        windBall = Instantiate(prefab, sourceTransform.position+sourceTransform.forward*instantiateDistance, Quaternion.identity).GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (windBall == null) return;
        Vector3 forceUp = Physics.gravity;
        Vector3 rateOfChangePosition = RateOfChangePosition();
        Vector3 forceFromMovment = rateOfChangePosition * moveForce.axis * handMultiplier;
        if (moveForce.axis > 0.5F)
        {
            windBall.AddForce(forceFromMovment - forceUp);
        }
        else
        {
            Vector3 brakingForce = -windBall.velocity;
            windBall.AddForce(brakingForce - forceUp);
        }
    }

    private Vector3 previousPosition;
    private Vector3 currentPosition;
    private Vector3 previousPlayerPos;
    private Quaternion previousPlayerRotation;
    private Player player;
    public Vector3 RateOfChangePosition()
    {
        previousPosition = currentPosition;
        currentPosition = sourceTransform.position;
        if (previousPlayerPos != player.transform.localPosition || previousPlayerRotation != player.transform.localRotation)
        {
            previousPlayerPos = player.transform.localPosition;
            previousPlayerRotation = player.transform.localRotation;
            previousPosition = sourceTransform.position;
            currentPosition = sourceTransform.position;
        }
        return (currentPosition - previousPosition) / Time.deltaTime;
    }
}
