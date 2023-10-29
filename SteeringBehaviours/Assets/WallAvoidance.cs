using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WallAvoidance : SteeringBehaviourBase
{
    [SerializeField]
    private Vector3 boxScale;
    [SerializeField]
    private float LookAheadDistance;
    private RaycastHit m_Hit;
    private float castDistance;
    private bool m_HitDetect;

    public Vector3[] steeringVectors;
    [SerializeField]
    private float veiwAngle;

    void generateSteeringVectors(int vectorCount)
    {
        steeringVectors = new Vector3[vectorCount]; 
        for(int i=0; i<vectorCount; i++)
        {
            float theta = Mathf.PI * 2.0f * i * 1.61804f;
            float phi = Mathf.Acos(1.0f - (2.0f * ((float)i / (vectorCount - 1))));
            float x = Mathf.Cos(theta) * Mathf.Sin(phi);
            float y = Mathf.Sin(theta) * Mathf.Sin(phi);
            float z = Mathf.Cos(phi);
            steeringVectors[i] = new Vector3(x, y, z);
        }
    }

    private void Start()
    {
        generateSteeringVectors(100);

        foreach (Vector3 i in steeringVectors)
        {
            if(Vector3.Angle(transform.forward, i) < veiwAngle)
            {
                Debug.Log(i);
                Debug.DrawLine(transform.position, transform.position + (100 * i), Color.green, 1.2f);
            }

        }
    }
    public override Vector3 Calculate()
    {
        Vehicle vehicle = GetComponent<Vehicle>();
        castDistance = LookAheadDistance * vehicle.Velocity.magnitude;
        Debug.DrawLine(transform.position, (transform.forward * castDistance) + transform.position, Color.red, .1f);


        m_HitDetect = Physics.BoxCast(transform.position, boxScale, transform.forward, out m_Hit, transform.rotation, castDistance);
        if (m_HitDetect)
        {
            if (m_Hit.collider.gameObject.tag == "Obstacle")
            {
                Vector3 obstacleLocalPosition = transform.InverseTransformPoint(m_Hit.collider.transform.position);
                float distanceToObstacle = obstacleLocalPosition.magnitude;
                float forceMultiplier = 1.0f + ((castDistance - distanceToObstacle) / castDistance);
                Vector3 outVector = -forceMultiplier * Vector3.Normalize(new Vector3(obstacleLocalPosition.x, obstacleLocalPosition.y, 0));
                Debug.DrawLine(transform.position, transform.TransformPoint(outVector), Color.blue, .1f);
                Vector3 DesiredVelocity = (outVector - transform.position);
                return (DesiredVelocity - vehicle.Velocity);
            }

        }



        return new Vector3(0f, 0f, 0f);
    }

}

