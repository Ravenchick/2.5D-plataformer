using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _controller;
    public Vector3 playerVelocity;
    public bool groundedPlayer;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float gravityValue = -9.81f;
    public bool _hangingFromLedge;
    public Vector3 _climbUpPosition;
    public GameObject _climbUpHitBox;

    public float rolingForce;
    public bool isRolling;

    //Snap the model position 
    private Transform _model;
    private Vector3 _modelPosition;

    private Animator _animator;



    //Ladder system
    private ClimbLadder _ladder;
    public bool _onLadderRange;
    

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
        _climbUpHitBox = GameObject.Find("Ledge_Grab_Checker");
        _model = GameObject.Find("Model").GetComponent<Transform>();
        _ladder = GetComponent<ClimbLadder>();
        
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

        //Block rotation of the model

        _model.transform.rotation = transform.rotation;

        //Climb ladder

        if(Input.GetKeyDown(KeyCode.E) && _onLadderRange == true && _animator.GetCurrentAnimatorStateInfo(0).IsName("Climbing") == false)
        {            
            _ladder.enabled = true;
            
        }

        _animator.speed = 1;


        

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
            _hangingFromLedge = false;
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

    //jump into the ladder

    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            _onLadderRange = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            _onLadderRange = true;
        }
    }
}
