using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] CameraWalls camWalls;

    [SerializeField] float moveSpeed = 4;

    [SerializeField] SpriteRenderer spriteRenderer;

    [SerializeField] ParticleSystem particles;

    float vertical;
    float horizontal;

    public bool hasAntidote = false;

    // Update is called once per frame
    void Update()
    {
        // Antidode Usage
        if (UseAntidote() && hasAntidote)
        {
            particles.Play();
            hasAntidote = false;
            CheckHits();
        }

        // Handles Cam walls
        CamWalls();

        // Assigns Vectors based on input values.
        GetInput();

        // Handles sprite flipping.
        FlipSprite();

        // Creates vector for movement.
        Vector3 move = new Vector3(horizontal, vertical, transform.position.z);

        // Moves car forward/backwards
        transform.Translate(move * moveSpeed * Time.deltaTime);
    }

    void GetInput()
    {
        vertical = Input.GetAxisRaw("Vertical");
        horizontal = Input.GetAxisRaw("Horizontal");
    }

    void CamWalls()
    {
        Vector3 vehiclePosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        // Left & Right
        if (transform.position.x > camWalls.width / 2)
        {
            vehiclePosition.x = camWalls.width / 2;
        }
        if (transform.position.x < -camWalls.width / 2)
        {
            vehiclePosition.x = -camWalls.width / 2;
        }

        // Top & Bottom
        if (transform.position.y > camWalls.height / 2)
        {
            vehiclePosition.y = camWalls.height / 2;
        }
        if (transform.position.y < -camWalls.height / 2)
        {
            vehiclePosition.y = -camWalls.height / 2;
        }

        transform.position = vehiclePosition;
    }

    void FlipSprite()
    {
        if (horizontal > 0)
        {
            spriteRenderer.flipX = false;
        }
        if (horizontal < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    bool UseAntidote()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            return true;
        }

        return false;
    }

    void CheckHits()
    {
        float hitRadius = particles.shape.radius;

        foreach(Agent agent in AgentManager.Instance.agents)
        {
            float sqrDistance = Vector3.SqrMagnitude(transform.position - agent.transform.position);

            float sqrRadii = Mathf.Pow(hitRadius, 2) + Mathf.Pow(agent.physicsObj.radius, 2);

            // Returns boolean
            if (sqrDistance < sqrRadii)
            {
                agent.GetComponent<TagPlayer>().Cure();
            }
        }
    }
}
