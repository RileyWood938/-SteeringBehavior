using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrive : SteeringBehaviourBase
{
    [SerializeField]
    private AnimationCurve arrivalCurve = new AnimationCurve();
    [SerializeField]
    private float curveScalar = 1;

    public Vector3 SeekTargetPos;
    public override Vector3 Calculate()
    {
        Vehicle vehicle = GetComponent<Vehicle>();
        //Debug.Log(Vector3.Distance(SeekTargetPos, transform.position));
        Vector3 DesiredVelocity = ((SeekTargetPos - transform.position).normalized) * (vehicle.MaxSpeed) * (arrivalCurve.Evaluate(Vector3.Distance(transform.position, SeekTargetPos)/curveScalar));

        return (DesiredVelocity - vehicle.Velocity);
    }
}
