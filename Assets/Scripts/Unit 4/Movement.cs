using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Vector2 velocity;
    private Rigidbody2D rb;

    public float speed = 15f;
    
    private void Awake()
    {
        velocity = Vector2.zero;
        rb = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        velocity.x = Input.GetAxisRaw("Horizontal");
        velocity.y = Input.GetAxisRaw("Vertical");

    }

    private void FixedUpdate()
    {

        rb.MovePosition(rb.position + (Time.fixedDeltaTime * speed * velocity));
    }
}
