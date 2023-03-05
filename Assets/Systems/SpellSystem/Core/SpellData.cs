using System;
using UnityEditor;
using UnityEngine;

[Serializable]
public struct SpellData
{
    [RequireInterface(typeof(ISpell))]
    public UnityEngine.Object spellController;
    public string gestureName;
}
