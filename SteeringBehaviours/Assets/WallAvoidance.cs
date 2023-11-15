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

    private void Start()
    {
        generateSteeringVectors(100);

    }
    public override Vector3 Calculate()
    {
        Vehicle vehicle = GetComponent<Vehicle>();
        castDistance = LookAheadDistance * (vehicle.Velocity.magnitude);

        RaycastHit closestHit;
        Physics.Raycast(transform.position, transform.forward, out closestHit, castDistance);
        float bestDistance = float.MaxValue;
        foreach(Vector3 i in steeringVectors)
        {
            if (Vector3.Angle(transform.forward, i) < veiwAngle)
            {
                RaycastHit hitData;
                if (Physics.Raycast(transform.position, i, out hitData, castDistance))
                {
                    if (hitData.rigidbody)
                    {
                        if (hitData.rigidbody.gameObject.tag == "Wall")
                        {
                            if (hitData.distance < bestDistance)
                            {
                                bestDistance = hitData.distance;
                                closestHit = hitData;
                            }
                            Debug.DrawLine(transform.position, transform.position + (castDistance * i), Color.red, .01f);
                        }
                    }
                }
                else
                {
                    Debug.DrawLine(transform.position, transform.position + (castDistance * i), Color.green, .01f);
                }
            }
        }
        if (bestDistance<=castDistance) //since bestDistance is initialized super big this effectively checks if we hit a wall or not
        {
            float forceMultiplier = 1.0f + ((castDistance - closestHit.distance) / castDistance);

            Vector3 DesiredVelocity =  closestHit.normal * forceMultiplier;

            Debug.DrawLine(transform.position, transform.position + DesiredVelocity, Color.blue, .01f);
            return (DesiredVelocity - vehicle.Velocity);
        }
        else
        {
            //Debug.Log("NO WALL DETECTED");
            return new Vector3(0f, 0f, 0f);
        }

    }


    void generateSteeringVectors(int vectorCount)
    {
        Vector3[] angles = new Vector3[vectorCount];
        for (int i = 0; i < vectorCount; i++)
        {
            float theta = Mathf.PI * 2.0f * i * 1.61804f;
            float phi = Mathf.Acos(1.0f - (2.0f * ((float)i / (vectorCount - 1))));
            float x = Mathf.Cos(theta) * Mathf.Sin(phi);
            float y = Mathf.Sin(theta) * Mathf.Sin(phi);
            float z = Mathf.Cos(phi);
            Vector3 newVec = new Vector3(x, y, z);
            angles[i] = newVec;
        }
        steeringVectors = angles;
    }
}

