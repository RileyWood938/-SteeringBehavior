using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AvoidObstacles : SteeringBehaviourBase
{
    [SerializeField]
    private Vector3 boxScale;
    [SerializeField]
    private float LookAheadDistance;
    private RaycastHit m_Hit;
    private float castDistance;
    private bool m_HitDetect;



    private void Start()
    {
    }
    public override Vector3 Calculate()
    {
        Vehicle vehicle = GetComponent<Vehicle>();
        castDistance= LookAheadDistance * vehicle.Velocity.magnitude;
        Debug.DrawLine(transform.position, (transform.forward*castDistance)+transform.position, Color.red, .1f);


        m_HitDetect = Physics.BoxCast(transform.position, boxScale, transform.forward, out m_Hit, transform.rotation, castDistance);
        if (m_HitDetect)
        {
            if(m_Hit.collider.gameObject.tag == "Obstacle")
            {
                Vector3 obstacleLocalPosition = transform.InverseTransformPoint(m_Hit.collider.transform.position);
                float distanceToObstacle = obstacleLocalPosition.magnitude; 
                float forceMultiplier = 1.0f + ((castDistance - distanceToObstacle ) / castDistance);
                Vector3 outVector = -forceMultiplier* Vector3.Normalize(new Vector3(obstacleLocalPosition.x, obstacleLocalPosition.y, 0));
                Debug.DrawLine(transform.position, transform.TransformPoint(outVector), Color.blue, .1f);
                Vector3 DesiredVelocity = (outVector - transform.position);
                return (DesiredVelocity - vehicle.Velocity);
            }

        }



        return new Vector3(0f, 0f, 0f);
    }

}
