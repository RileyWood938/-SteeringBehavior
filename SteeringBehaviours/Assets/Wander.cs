
using UnityEngine;

public class Wander : SteeringBehaviourBase
{
    public float WanderRadius = 10f;
    public float WanderDistance = 10f;
    public float WanderJitter = 10f;
    Vector3 WanderTarget = Vector3.zero;
    float WanderAngleEnd = 0.0f;
    float WanderAngleStart = 0.0f;
    float WanderAngleCurrent = 0.0f;
    float timer = 0;

    void Start()
    {
        WanderAngleEnd = Random.Range(0.0f, Mathf.PI * 2);
        WanderTarget = new Vector3(Mathf.Cos(WanderAngleEnd), 0, Mathf.Sin(WanderAngleEnd)) * WanderRadius;
    }

    public override Vector3 Calculate()
    {
        //Debug.DrawLine(new Vector3(0f, 500f, 0f), Vector3.zero, Color.red, 1.0f, false);
        timer += Time.deltaTime;
        if(timer > 1)
        {
            timer = 0;
            WanderAngleStart = WanderAngleCurrent;
            WanderAngleEnd += Random.Range(-WanderJitter, WanderJitter);

            Debug.Log("hit");
        }
        else
        {
            WanderAngleCurrent = Mathf.Lerp(WanderAngleStart, WanderAngleEnd, timer);
            debugDrawAngle(WanderAngleCurrent, Color.yellow);
            debugDrawAngle(WanderAngleEnd, Color.red);
            debugDrawAngle(WanderAngleStart, Color.green);
        }

        WanderTarget = new Vector3(Mathf.Cos(WanderAngleCurrent), 0, Mathf.Sin(WanderAngleCurrent)) * WanderRadius;

        Vector3 targetLocal = WanderTarget;

        Vector3 targetWorld = transform.position + WanderTarget;

        targetWorld += transform.forward * WanderDistance;

        return targetWorld - transform.position;
    }
    private void debugDrawAngle(float angle, Color color)
    {
        Vector3 tempTarget = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * WanderRadius;

        Vector3 tempTargetWorld = transform.position + tempTarget;

        //targetWorld += transform.forward * WanderDistance;

        //return targetWorld - transform.position;

        Debug.DrawLine(transform.position, tempTargetWorld, color, .01f);
    }

}

