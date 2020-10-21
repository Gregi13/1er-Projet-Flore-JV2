using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask Ground;
    
    private Inputs inputs;
    private Vector2 direction;
    private bool isOnGround = false;

    private Rigidbody2D myRigidbody;
    private Animator myAnimator;
    private SpriteRenderer myRenderer;
    
    private void OnEnable()

    {
        inputs = new Inputs();
        inputs.Enable();
        inputs.Player.Move.performed += OnMovePerformed;
        inputs.Player.Move.canceled += OnMoveCanceled;
        inputs.Player.Jump.performed += JumpOnPerformed;
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myRenderer = GetComponent<SpriteRenderer>();

    }
    private void OnMoveCanceled(InputAction.CallbackContext obj) 
    {
        direction = Vector2.zero;
    }
    private void OnMovePerformed(InputAction.CallbackContext obj)
    {   
        direction = obj.ReadValue<Vector2>();
    }

    private void JumpOnPerformed(InputAction.CallbackContext obj)
    {
        if (isOnGround)
        {
            myRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isOnGround = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<Animator>().SetBool("IsRunning", true);
    }

    void FixedUpdate()
    {
        direction.y = 0;
        if (myRigidbody.velocity.sqrMagnitude < maxSpeed) 
            myRigidbody.AddForce(direction * speed);
        var IsRunning = direction.x != 0;
        myAnimator.SetBool("IsRunning", IsRunning);
        if (direction.x < 0)
        {
            myRenderer.flipX = true;
        }
        
        else if (direction.x > 0)
        {
            myRenderer.flipX = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }
}
