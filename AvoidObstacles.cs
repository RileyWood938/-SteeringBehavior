using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]

public class AvoidObstacles : SteeringBehaviourBase
{
    [SerializeField]
    BoxCollider ObstacleDetectionCollider;
    private Vector3 boxScale;
    [SerializeField]
    private float LookAheadDistance;
    private void Start()
    {
        boxScale = new Vector3(ObstacleDetectionCollider.size.x, ObstacleDetectionCollider.size.y, 0.0f);
    }
    public override Vector3 Calculate()
    {
        Vehicle vehicle = GetComponent<Vehicle>();
        boxScale.z = LookAheadDistance * vehicle.Velocity.magnitude;
        ObstacleDetectionCollider.size = boxScale;
        ObstacleDetectionCollider.center = new Vector3(0, 0, boxScale.z / 2);



        return new Vector3(0f, 0f, 0f);
    }

}
