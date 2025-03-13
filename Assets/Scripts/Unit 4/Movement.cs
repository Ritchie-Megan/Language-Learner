using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
        
        if (EventSystem.current.currentSelectedGameObject != null &&
            EventSystem.current.currentSelectedGameObject.GetComponent<InputField>() != null)
        {
            return; // Skip movement if InputField is active
        }
        
        velocity.x = Input.GetAxisRaw("Horizontal");
        velocity.y = Input.GetAxisRaw("Vertical");

    }

    private void FixedUpdate()
    {

        rb.MovePosition(rb.position + (Time.fixedDeltaTime * speed * velocity));
    }
}
