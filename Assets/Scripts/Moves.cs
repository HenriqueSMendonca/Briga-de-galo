using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Move")]
public class Moves : ScriptableObject
{
    [SerializeField] string moveName;

    [TextArea]
    [SerializeField] string description;
    [SerializeField] int damage;
    [SerializeField] int spCost;
    [SerializeField] MoveEffects effects;
    [SerializeField] MoveTarget target;

    public string Name { get { return moveName; } }
    public string Description { get { return description; } }
    public int Damage { get { return damage; } }
    public int SpCost { get { return spCost; } }
    public MoveEffects Effects { get { return effects; } }
    
    public MoveTarget Target { get { return target; } }

    [System.Serializable]
    public class MoveEffects
    {
        
    }
    public enum MoveTarget
    {
        Foe, Self
    }
}
