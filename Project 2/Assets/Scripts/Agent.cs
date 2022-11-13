using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PhysicsObject))]
public abstract class Agent : MonoBehaviour
{
    public PhysicsObject physicsObj;

    [SerializeField] float maxSpeed = 5f;
    [SerializeField] float maxForce = 5f;

    protected Vector3 totalSteeringForce = Vector3.zero;

    private float wanderAngle = 0f;
    public float maxWanderAngle = 45f;
    public float maxWanderChangePerSecond = 10f;

    protected abstract void CalcSteeringForces();

    private void Awake()
    {
        if (physicsObj == null)
        {
            physicsObj = GetComponent<PhysicsObject>();
        }
    }

    protected virtual void Update()
    {
        CalcSteeringForces();

        // Limits total forces.
        totalSteeringForce = Vector3.ClampMagnitude(totalSteeringForce, maxForce);
        physicsObj.ApplyForce(totalSteeringForce);

        totalSteeringForce = Vector3.zero;
    }

    protected void Seek(Vector3 targetPos, float weight = 1f)
    {
        Vector3 desiredVel = targetPos - physicsObj.position;
        desiredVel = desiredVel.normalized * maxSpeed;

        Vector3 seekForce = desiredVel - physicsObj.velocity;
        totalSteeringForce += seekForce * weight;
    }

    protected void Flee(Vector3 targetPos, float weight = 1f)
    {
        Vector3 desiredVel = physicsObj.position - targetPos;
        desiredVel = desiredVel.normalized * maxSpeed;

        Vector3 fleeForce = desiredVel - physicsObj.velocity;
        totalSteeringForce += fleeForce * weight;
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
        Seek(wanderTarget);
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
