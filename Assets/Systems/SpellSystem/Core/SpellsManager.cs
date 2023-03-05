using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRGesureRecognition;

public class SpellsManager : MonoBehaviour
{
    [SerializeField]
    List<SpellData> spells;
    public void OnRecognitionEnd(List<RecognizeOutput> output)
    {
        foreach (var spell in spells) 
        {
            if (spell.gestureName.Equals(output[0].recognizedGesture.gestureName))
            {
                ISpell spellController = spell.spellController as ISpell;
                spellController.Cast(GestureManager.Instance.trackedPoint.transform);
            }
        }
    }
}
