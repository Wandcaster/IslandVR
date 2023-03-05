using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class SetParent : MonoBehaviour
{
    [SerializeField]
    Transform parent;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Spell")) return;
        if (other.gameObject.isStatic) return;
        if (other.CompareTag("Player")) Player.instance.transform.parent = parent;
        if (other.transform.parent != null) return;
        other.transform.parent = parent;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Spell")) return;
        if (other.gameObject.isStatic) return;
        if (other.CompareTag("Player")) Player.instance.transform.parent = null;
        if (other.transform.parent != parent) return;
        other.transform.parent = null;
    }
}
