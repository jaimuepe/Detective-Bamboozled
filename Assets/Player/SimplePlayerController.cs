using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimplePlayerController : MonoBehaviour
{
    public float movementSpeed;

    [Header("Black market only")]
    public LayerMask slopes;
    public float slopeOffset = 1.5f;
    public float yPositionWhenGrounded = 1.35f;
    public float offsetWhenGrounded = 1.35f;

    Rigidbody rb;

    int direction;
    public int Direction { get { return direction; } }

    int previousDirection;
    public int PreviousDirection { get { return previousDirection; } }

    Transform playerTransform;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        playerTransform = transform;
    }

    public void Move(Vector3 movementDirection)
    {
        rb.velocity = new Vector3(
            movementDirection.x * movementSpeed,
            rb.velocity.y,
            movementDirection.z * movementSpeed);

        RaycastHit[] hits = Physics.RaycastAll(transform.position, Vector3.down, 5f, slopes);
        if (hits.Length > 0)
        {
            Vector3 collisionPoint = hits[0].point;

            float offset;

            if (hits[0].normal == Vector3.up)
            {
                offset = offsetWhenGrounded;
            }
            else
            {
                offset = slopeOffset;
            }

            playerTransform.position = new Vector3(playerTransform.position.x,
                offset + collisionPoint.y,
                playerTransform.position.z);
        }
        else
        {
            playerTransform.position = new Vector3(playerTransform.position.x,
                yPositionWhenGrounded,
                playerTransform.position.z);
        }

        if (movementDirection.x != 0)
        {
            previousDirection = direction;

            if (movementDirection.x > 0)
            {
                direction = 1;
            }
            else
            {
                direction = -1;
            }
        }
    }
}
