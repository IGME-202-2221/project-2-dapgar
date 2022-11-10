using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PhysicsObject))]
public abstract class Agent : MonoBehaviour
{
    [SerializeField] protected PhysicsObject physicsObj;

    [SerializeField] float maxSpeed = 2f;
    [SerializeField] float maxForce = 2f;

    protected Vector3 totalSteeringForce;

    private float wanderAngle = 0f;
    public float maxWanderAngle = 45f;
    public float maxWanderChangePerSecond = 10f;

    protected abstract void CalcSteeringForces();

    protected virtual void Update()
    {
        totalSteeringForce = Vector3.zero;
        CalcSteeringForces();

        // Limits total forces.
        totalSteeringForce = Vector3.ClampMagnitude(totalSteeringForce, maxForce);
        physicsObj.ApplyForce(totalSteeringForce);
    }

    protected Vector3 Seek(Vector3 targetPos)
    {
        Vector3 desiredVel = targetPos - transform.position;
        desiredVel = desiredVel.normalized * maxSpeed;

        Vector3 seekForce = desiredVel - physicsObj.velocity;
        return seekForce;
    }

    protected Vector3 Flee(Vector3 targetPos)
    {
        Vector3 desiredVel = transform.position - targetPos;
        desiredVel = desiredVel.normalized * maxSpeed;

        Vector3 fleeForce = desiredVel - physicsObj.velocity;
        return fleeForce;
    }

    protected void Wander()
    {
        // Update the angle of out current wander
        float maxWanderChange = maxWanderChangePerSecond * Time.deltaTime;
        wanderAngle += Random.Range(-maxWanderChange, maxWanderChange);
        wanderAngle = Mathf.Clamp(wanderAngle, -maxWanderAngle, maxWanderAngle);

        // Get position defined by wander angle
        Vector3 wanderTarget = Quaternion.Euler(0, 0, wanderAngle) * physicsObj.direction.normalized + physicsObj.position;

        // Seek towards our wander position
        totalSteeringForce += Seek(wanderTarget);
    }

    protected void StayInBounds()
    {
        // Get position
        Vector3 futurePosition = GetFuturePosition();

        // Check position
        if (futurePosition.x > physicsObj.CamWidth || futurePosition.x < -physicsObj.CamWidth ||
            futurePosition.y > physicsObj.CamHeight || futurePosition.y < -physicsObj.CamHeight)
        {
            // If OOB
            Seek(Vector3.zero);
        }
    }

    public Vector3 GetFuturePosition(float timeToLookAhead = 1f)
    {
        return physicsObj.position + physicsObj.velocity * timeToLookAhead;
    }
}
