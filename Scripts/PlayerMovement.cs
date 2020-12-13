using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D Rb;
    Vector2 movement;
    private Animator anim;
    private float horizontal = 0.0f;
    private float speed = 0.0f;
    float xMin;
    float xMax;
    float yMin;
    float yMax;
    float padding = 1f;

    private void Start()
    {
        anim = GetComponent<Animator>();
        SetUpMoveBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        //for input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        horizontal = movement.x > 0.01f ? movement.x : movement.x < -0.01f ? 1 : 0;
        speed = movement.y > 0.01f ? movement.y : movement.y < -0.01f ? 1 : 0;

        if(movement.x < -0.01f)
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        } else
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }

        anim.SetFloat("Horizontal", horizontal);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Speed", speed);
    }

    private void FixedUpdate()
    {
        //for movement
        var position = Rb.position + movement * moveSpeed * Time.fixedDeltaTime;
        position.x = Mathf.Clamp(position.x, xMin, xMax);
        position.y = Mathf.Clamp(position.y, yMin, yMax);
        Rb.MovePosition(position);
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }
}
