using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _controller;
    public Vector3 playerVelocity;
    private bool groundedPlayer;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float gravityValue = -9.81f;
    private bool _hangingFromLedge;
    public Vector3 _climbUpPosition;
    public GameObject _climbUpHitBox;

    public float rolingForce;
    public bool isRolling;

    //Snap the model position 
    private Transform _model;
    private Vector3 _modelPosition;

    private Animator _animator;
    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
        _climbUpHitBox = GameObject.Find("Ledge_Grab_Checker");
        _model = GameObject.Find("Model").GetComponent<Transform>();
        
    }

    private void Start()
    {
        _modelPosition = _model.transform.localPosition;
    }

    private void Update()
    {
        if (isRolling)
        {
            _controller.Move(transform.forward * rolingForce * Time.deltaTime);
            
        }
        else
        {
            Movement();
        }
        

        if (_controller.enabled == true)
        {
            _model.transform.localPosition = _modelPosition;
        }

        
    }

    private void LateUpdate()
    {
        float animatorSpeed;
        animatorSpeed = Input.GetAxis("Horizontal");
        _animator.SetFloat("Speed", Mathf.Pow(animatorSpeed, 2));

        _animator.SetBool("Grounded", groundedPlayer);
        _animator.SetFloat("FallingSpeed", playerVelocity.y);
        

    }

    private void Movement()
    {

        groundedPlayer = _controller.isGrounded;

        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = -0.5f;
                        
        }

        Vector3 move = new Vector3(0, 0, Input.GetAxis("Horizontal"));

        _controller.Move(move * Time.deltaTime * playerSpeed);


        if (Input.GetKeyDown(KeyCode.Space) && groundedPlayer)
        {
            playerVelocity.y += jumpHeight;

            //animator
            _animator.SetTrigger("Jump");
            
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        _controller.Move(playerVelocity * Time.deltaTime);

        if (move != Vector3.zero && _controller.enabled == true)
        {
            gameObject.transform.forward = move;
        }

        //climbing up

        if (_hangingFromLedge == true && Input.GetKeyDown(KeyCode.E))
        {
            _animator.SetTrigger("ClimbUp");
        }

        //Roll

        if(groundedPlayer && Input.GetKeyDown(KeyCode.LeftShift))
        {
            _animator.SetTrigger("Roll");
            
        }
    }

    public void LedgeGrab(Vector3 _climbUpSnap)
    {
        _climbUpPosition = _climbUpSnap;
        _animator.SetTrigger("LedgeGrab");
        _controller.enabled = false;
        _hangingFromLedge = true;
        _climbUpHitBox.SetActive(false);
        

    }

    public void ClimbUp()
    {

        transform.position = _climbUpPosition;        
        _climbUpHitBox.SetActive(true);
        _controller.enabled = true;
    }

}
