using System;
using UnityEngine;

public class Movement : Photon.Pun.MonoBehaviourPun
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
        if (photonView.IsMine)
        {
            velocity.x = Input.GetAxisRaw("Horizontal");
            velocity.y = Input.GetAxisRaw("Vertical");
        }

    }

    private void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            rb.MovePosition(rb.position + (Time.fixedDeltaTime * speed * velocity));
        }
    }
}
