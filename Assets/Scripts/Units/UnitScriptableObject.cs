using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Unit", order = 1)]
public class UnitScriptableObject : ScriptableObject
{
    public string UnitName;

    public float MaxHealth;
    public float Damage;
    public float Speed;
    public float Range;
}
