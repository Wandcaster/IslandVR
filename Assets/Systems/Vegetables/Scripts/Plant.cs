using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Plant : MonoBehaviour
{
    [SerializeField]
    bool isGrowing=true;
    public bool attachedToRoot=true;
    [SerializeField]
    private float growSpeed= 0.0041F;
    [SerializeField]
    private float StateGrowTime= 0.05F;
    [SerializeField]
    float UpdateStatusWaitTime=2;
    [SerializeField]
    [Range(0, 1)]
    private float growProggres;
    [SerializeField]
    [Range(0, 1)]
    float pickUpTreshold=0.8F;
    [SerializeField]
    [Range(0, 1)]
    float waterToGrowTreshold=0.5F;
    private bool canPickUp = false;
    public PlantContainerController root;

    private Material material;
    private IgnoreHovering ignoreHovering;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().materials[0];
        if(GetComponent<IgnoreHovering>() == null ) ignoreHovering= gameObject.AddComponent<IgnoreHovering>();
        else ignoreHovering= GetComponent<IgnoreHovering>();
        StartCoroutine(CheckStatus());
        StartCoroutine(UpdateStatus());
    }
    private void ChangeGrowProgress(float value)
    {
        if (root.hydrationLevel < waterToGrowTreshold) return;
        growProggres = Mathf.Clamp01(growProggres + value);
        transform.localScale = new Vector3(growProggres, growProggres, growProggres);
        material.SetFloat("Vector1_4df39215be94416c90310741114feced", growProggres);
    }
    private IEnumerator CheckStatus()
    {
        while (true)
        {
            yield return new WaitForSeconds(UpdateStatusWaitTime);
            if (growProggres == 1) isGrowing = false;
            canPickUp = pickUpTreshold < growProggres ? true : false;
            yield return true;
        }
    }
    private IEnumerator UpdateStatus()
    {
        while (true)
        {
            if (isGrowing)
            {
                yield return new WaitForSeconds(StateGrowTime);
                ChangeGrowProgress(growSpeed);
            }
            if (canPickUp && ignoreHovering != null) Destroy(ignoreHovering);
            else if(!canPickUp && ignoreHovering == null) ignoreHovering = gameObject.AddComponent<IgnoreHovering>();


            yield return true;
        }
    }
    public void SetIsGrowing(bool isGrowing)
    {
        this.isGrowing = isGrowing;
    }
    public void ChangeLayer(string layer)
    {
        gameObject.layer = LayerMask.NameToLayer(layer);
    }
    public void SetAttachedToRoot(bool attachedToRoot)
    {
        this.attachedToRoot = attachedToRoot;
    }
    public void RemoveFromRoot()
    {
        SpawnSpot temp = root.spawnSpots.Find(x => x.plant == this);
        Debug.Log(temp);
        root.spawnSpots.Remove(temp);
        temp.plant = null;
        root.spawnSpots.Add(temp);
    }
}
