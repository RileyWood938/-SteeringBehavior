using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract class. Steering Behaviours will inherit from this (see Wander/Seek)
/// </summary>
[RequireComponent(typeof(Vehicle))]
public abstract class SteeringBehaviourBase : MonoBehaviour
{
    //Needs to be overidden in child classes
    public abstract Vector3 Calculate();

    [SerializeField]
    private float Weight;

    public float getWeight()
    {
        return Weight;
    }

}
