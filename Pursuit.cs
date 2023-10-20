using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursuit : SteeringBehaviourBase
{
    public Vehicle PursuitTarget;
    public override Vector3 Calculate()
    {
        Vehicle vehicle = GetComponent<Vehicle>();
        Vector3 vectorToTarget = transform.position - PursuitTarget.transform.position;
        float angleToTarget = Vector3.Dot(transform.forward, PursuitTarget.transform.forward);
        //Debug.DrawLine(transform.position, PursuitTarget.transform.position, Color.red, 0.1f);
        //Debug.Log(angleToTarget);

        Vector3 predictedPosition = PursuitTarget.transform.position;//just use default seek behavior if we are facing nearly opposite the target's heading
        Color lineColor = Color.red;
        if (angleToTarget>-0.8f) 
        {
            //Debug.Log("Predicting Position");
            float lookAheadTime = vectorToTarget.magnitude / (vehicle.MaxSpeed + PursuitTarget.Velocity.magnitude); //Dynamically change how far ahead you are looking based on distance to target
            predictedPosition = PursuitTarget.transform.position + (PursuitTarget.Velocity * lookAheadTime);
            lineColor = Color.green;

        }

        Debug.DrawLine(transform.position, predictedPosition, lineColor, 0.1f);
        Vector3 DesiredVelocity = (predictedPosition - transform.position).normalized * vehicle.MaxSpeed;
        return (DesiredVelocity - vehicle.Velocity);
    }
}
