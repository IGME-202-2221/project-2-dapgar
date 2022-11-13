using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    public Vector3 direction = Vector3.zero;
    public Vector3 velocity = Vector3.zero;
    public Vector3 acceleration = Vector3.zero;
    public Vector3 position = Vector3.zero;
    
    [SerializeField] float mass = 1f;
    [SerializeField] public float radius;

    [SerializeField] bool useGravity = true;
    Vector3 gravity = Vector3.down;

    [SerializeField] bool useFriction = true;
    [SerializeField] float coeff = 1f;

    [SerializeField] bool bounceOffWalls;

    private float camHeight;
    private float camWidth;

    public float CamHeight
    {
        get { return camHeight; }
    }

    public float CamWidth
    {
        get { return camWidth; }
    }

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;

        camHeight = Camera.main.orthographicSize * 2;
        camWidth = camHeight * Camera.main.aspect;

        direction = Random.insideUnitCircle.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        if (bounceOffWalls)
        {
            Bounce();
        }

        if (useGravity)
        {
            ApplyGravity();
        }

        if (useFriction)
        {
            ApplyFriction();
        }

        // Calc the velocity
        velocity += acceleration * Time.deltaTime;
        position += velocity * Time.deltaTime;

        if (velocity.sqrMagnitude > Mathf.Epsilon)
        {
            // Grab current direction from velocity
            direction = velocity.normalized;
        }

        transform.position = position;

        // Zero out acceleration
        acceleration = Vector3.zero;

        // Handle rotation
        transform.rotation = Quaternion.LookRotation(Vector3.back, direction);
    }

    public void ApplyForce(Vector3 force)
    {
        // F = M * A
        // A = F /M
        acceleration += force / mass;
    }

    void ApplyGravity()
    {
        acceleration += gravity;
    }

    void ApplyFriction()
    {
        Vector3 friction = velocity * -1;
        friction.Normalize();

        friction = friction * coeff;

        ApplyForce(friction);
    }

    void Bounce()
    {
        if (transform.position.y <= -camHeight / 2 ||
            transform.position.y >= camHeight / 2)
        {
            velocity.y *= -1;
        }
        if (transform.position.x <= -camWidth / 2 ||
            transform.position.x >= camWidth / 2)
        {
            velocity.x *= -1;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
