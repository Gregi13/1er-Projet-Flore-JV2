using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private float speed;        //fonction permettant de modifier la vitesse du personnage dans l'inspector
    [SerializeField] private float maxSpeed;     //fonction permettant de modifier la vitesse maximale du personnage dans l'inspector
    [SerializeField] private float jumpForce;    //fonction permettant de modifier la puissance de saut
    [SerializeField] private LayerMask Ground;   //fonction permettant de modifier
    
    private Inputs inputs;                       //fonction permettant de faire fonctionner les inputs que l'on a assigné
    private Vector2 direction;                   //fonction permettant d'utiliser les vector2 comme valeurs de déplacements
    private bool isOnGround = false;             

    private Rigidbody2D myRigidbody;             //permet l'utilisation du Rigidbody2D dans le code
    private Animator myAnimator;                 //permet l'utilisation de l'animator dans le code
    private SpriteRenderer myRenderer;
    
    private void OnEnable()

    {
        inputs = new Inputs();
        inputs.Enable();
        inputs.Player.Move.performed += OnMovePerformed;    //Lorsque l'input attribuer aux déplacements est utilisé, le personnage se déplace
        inputs.Player.Move.canceled += OnMoveCanceled;      //Lorsque l'input attribuer aux déplacements n'est pas utiliser, le personnage arrete de se déplacer
        inputs.Player.Jump.performed += JumpOnPerformed;    //Lorsque l'input attribuer au saut est utilisé, le personnage saute
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myRenderer = GetComponent<SpriteRenderer>();

    }
    private void OnMoveCanceled(InputAction.CallbackContext obj)     //lorsque l'on appuie pas sur le bouton déplacment, le personnage arrete de bouger
    {
        direction = Vector2.zero;
    }
    private void OnMovePerformed(InputAction.CallbackContext obj)    //lorsque l'on appuie sur le bouton déplacment, le personnage se déplace
    {   
        direction = obj.ReadValue<Vector2>();
    }

    private void JumpOnPerformed(InputAction.CallbackContext obj)    //permet de faire le sauter le personnage lorsque l'on appuie sur la barre espace
    {
        if (isOnGround)                                              //autorise le personnage a sauter seulement si il est sur la tilemap
        {
            myRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); //permet de donner une force d'impulsion au saut du personnage
            isOnGround = false;                                      // si le personnage n'est pas sur le sol, il ne peut y avoir de saut
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
        var IsJumping =Mathf.Abs(myRigidbody.velocity.y)>0.1f;
        myAnimator.SetBool("IsJumping", IsJumping);
        myAnimator.SetBool("IsRunning", IsRunning);
        if (direction.x < 0)
        {
            myRenderer.flipX = true;            //permet de faire flip le personnage lorsqu'il se déplace
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
