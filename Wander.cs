using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : SteeringBehaviourBase
{
    public float WanderRadius = 10f;
    public float WanderDistance = 10f;
    public float WanderJitter = 1f;
    Vector3 WanderTarget = Vector3.zero;
    float WanderAngle = 0.0f;
    [SerializeField]
    private float maxTurnRadius;

    void Start()
    {
        WanderAngle = Random.Range(0.0f, Mathf.PI * 2);
        WanderTarget = new Vector3(Mathf.Cos(WanderAngle), 0, Mathf.Sin(WanderAngle)) * WanderRadius;
    }

    public override Vector3 Calculate()
    {
        Vector3 TrueAngle = 
        WanderTarget = new Vector3(Mathf.Cos(WanderAngle), 0, Mathf.Sin(WanderAngle)) * WanderRadius;

        Vector3 targetWorld = transform.position + WanderTarget;

        targetWorld += transform.forward * WanderDistance;

        return targetWorld - transform.position;
    }

    IEnumerator updateWanderPosition()
    {
        yield return new WaitForSeconds(1.0f);
        WanderAngle += Random.Range(-WanderJitter, WanderJitter);
    }
}
