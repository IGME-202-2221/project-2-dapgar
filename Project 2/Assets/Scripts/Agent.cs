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

    public float personalSpace = 1f;

    public float visionRange = 2f;

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

    protected void Separate<T>(List<T> agents) where T : Agent
    {
        float sqrPersonalSpace = Mathf.Pow(personalSpace, 2);

        // Loop through all agents
        foreach (T other in agents)
        {
            float sqrDist = Vector3.SqrMagnitude(other.physicsObj.position - physicsObj.position);

            if (sqrDist < float.Epsilon)
            {
                continue;
            }

            if (sqrDist < sqrPersonalSpace)
            {
                float weight = sqrPersonalSpace / (sqrDist + 0.1f);
                Flee(other.physicsObj.position, weight);
            }
        }
    }

    protected void StayInBounds(float weight = 1f)
    {
        // Get position
        Vector3 futurePosition = GetFuturePosition();

        // Check position
        if (futurePosition.x > AgentManager.Instance.maxPosition.x ||
            futurePosition.x < AgentManager.Instance.minPosition.x ||
            futurePosition.y > AgentManager.Instance.maxPosition.y ||
            futurePosition.y < AgentManager.Instance.minPosition.y)
        {
            // If OOB
            Seek(Vector3.zero, weight);
        }
    }

    public Vector3 GetFuturePosition(float timeToLookAhead = 1f)
    {
        return physicsObj.position + physicsObj.velocity * timeToLookAhead;
    }

    protected void AvoidObstacle(Obstacle obstacle)
    {
        // Get Vector from the agent to the obstacle
        Vector3 toObstacle = obstacle.Position - physicsObj.position;

        // Check if obstacle is behind agent
        float fwdToObstacleDot = Vector3.Dot(physicsObj.direction, toObstacle);
        if (fwdToObstacleDot < 0)
        {
            return;
        }

        // Check if obstacle is too far to left or right
        float rightToObstacleDot = Vector3.Dot(physicsObj.right, toObstacle);
        if (Mathf.Abs(rightToObstacleDot) > physicsObj.radius + obstacle.radius)
        {
            return;
        }

        // Check if obstacle is within vision range
        if (fwdToObstacleDot > visionRange)
        {
            return;
        }

        // Avoid obstacle
        Vector3 desiredVelocity;

        if (rightToObstacleDot > 0)
        {
            // If obstacle right, steer left
            desiredVelocity = physicsObj.right * -maxSpeed;
        }
        else
        {
            // If obstacle left, steer right 
            desiredVelocity = physicsObj.right * maxSpeed;
        }

        // Create weight based on distance to obstacle
        float weight = visionRange / (fwdToObstacleDot + 0.1f);

        // Calc steering force
        Vector3 steeringForce = desiredVelocity - physicsObj.velocity * weight;

        // Apply steering force to total force
        totalSteeringForce += steeringForce;
    }

    protected void AvoidAllObstacles()
    {
        foreach (Obstacle obs in ObstacleManager.Instance.obstacles)
        {
            AvoidObstacle(obs);
        }
    }
}
