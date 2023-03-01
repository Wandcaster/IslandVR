using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBallController : MonoBehaviour
{
    [SerializeField]
    ParticleSystem particle;
    [SerializeField]
    ParticleSystem splash;
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.transform.CompareTag("WaterSource")) Destroy(gameObject);
    }   
    private void OnParticleCollision(GameObject other)
    {
        List<ParticleCollisionEvent> events = new List<ParticleCollisionEvent>();
        particle.GetCollisionEvents(other, events);
        if(events.Count > 0)
        {
            ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams
            {
                position = events[0].intersection,
                rotation3D = Quaternion.LookRotation(events[0].normal).eulerAngles

        };
            Debug.Log(events[0].normal);
            splash.Emit(emitParams, 1);
        }
    }
}
