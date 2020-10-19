using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;
    private Inputs inputs;
    private Vector2 direction;

    private void OnEnable()

    {
        inputs = new Inputs();
        inputs.Enable();
        inputs.Player.Move.performed += OnMovePerformed;
        inputs.Player.Move.canceled += OnMoveCanceled;
    }

    private void OnMoveCanceled(InputAction.CallbackContext obj) 
    {
        direction = Vector2.zero;
    }
    
    
    private void OnMovePerformed(InputAction.CallbackContext obj)
    {   
        direction = obj.ReadValue<Vector2>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        var myRigidBody = GetComponent<Rigidbody2D>();
        direction.y = 0;
        if (myRigidBody.velocity.sqrMagnitude < maxSpeed) 
            myRigidBody.AddForce(direction * speed);
    }
}
