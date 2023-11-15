using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour {

    /////////////////////
    //Updated Values
    /////////////////////
    /// <summary>
    /// This is applied to the current position every frame
    /// </summary>
    public Vector3 Velocity;

    //Position, Heading and Side can be accessed from the transform component with transform.position, transform.forward and transform.right respectively

    //"Constant" values, they are public so we can adjust them through the editor

    //Represents the weight of an object, will effect its acceleration
    public float Mass = 1;

    //The maximum speed this agent can move per second
    public float MaxSpeed = 1;

    //The thrust this agent can produce
    public float MaxForce = 1;

    //We use this to determine how fast the agent can turn, but just ignore it for, we won't be using it
    public float MaxTurnRate = 0.005f;

    private SteeringBehaviourBase[] SteeringBehaviors;

    private void Start()
    {
        //.Get components returns a reliably ordered top-to-bottom list of all the SteeringBehaviors in the editor
        SteeringBehaviors = GetComponents<SteeringBehaviourBase>();
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 SteeringForce = Vector3.zero;

        //Get all steering behaviours attached to this object and add their calculated steering force onto this SteeringForce

        foreach (SteeringBehaviourBase i in SteeringBehaviors)
        {
            Vector3 inputForce = i.Calculate() * i.getWeight();

            if ((SteeringForce + inputForce).magnitude > MaxForce)
            {
                Vector3.ClampMagnitude(inputForce, (MaxForce - inputForce.magnitude));
                SteeringForce += inputForce;
                break;
            }
            else
            {
                SteeringForce += inputForce;
            }
        }

        Vector3 Acceleration = SteeringForce / Mass;

        Velocity += Acceleration * Time.deltaTime;

        Velocity = Vector3.ClampMagnitude(Velocity, MaxSpeed);

        if (Velocity != Vector3.zero)
        {
            transform.position += Velocity * Time.deltaTime;

            Vector3 desiredHeading = Velocity.normalized;
            desiredHeading = ApplyAngleLimit(desiredHeading);
            transform.forward = desiredHeading;
        }

        //transform.right should update on its own once we update the transform.forward
    }
    Vector3 ApplyAngleLimit(Vector3 inputHeadingDesire)
    {
        if (Vector3.Angle(transform.forward, inputHeadingDesire) > Mathf.Rad2Deg * MaxTurnRate)
        {
            return Vector3.RotateTowards(transform.forward, inputHeadingDesire, MaxTurnRate, 0.0f);
        }
        return inputHeadingDesire;
    }
}


