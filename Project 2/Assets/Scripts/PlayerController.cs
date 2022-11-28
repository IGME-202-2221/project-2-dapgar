using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] CameraWalls camWalls;

    [SerializeField] float moveSpeed = 4;

    [SerializeField] SpriteRenderer spriteRenderer;

    float vertical;
    float horizontal;

    // Update is called once per frame
    void Update()
    {
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
}
