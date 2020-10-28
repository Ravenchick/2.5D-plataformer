using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbLadder : MonoBehaviour
{

    private Player _playerScript;
    private CharacterController _controller;
    [SerializeField] private float _speed;
    private Animator _anim;
    private Ladder _ladder;
    public Animation _climbingAnimation;
    private float _animSpeed;

    // Start is called before the first frame update
    void Awake()
    {
        _playerScript = GetComponent<Player>();        
        _controller = GetComponent<CharacterController>();
        _anim = GetComponentInChildren<Animator>();
        
    }

    private void OnEnable()
    {
        _animSpeed = _anim.speed;

        _playerScript.enabled = false;

        //snaping position

        transform.position = new Vector3(transform.position.x, transform.position.y, _ladder._zPosition);

        if (transform.position.y >= _ladder._maxYPosition)
        {
            transform.position = new Vector3(transform.position.x, _ladder._maxYPosition, transform.position.z);
        }

        _anim.SetTrigger("StartClimbing");
    }

    // Update is called once per frame
    void Update()
    {
        _playerScript.enabled = false;
        Vector3 move = new Vector3(0, Input.GetAxis("Vertical"), 0);
        _controller.Move(move * _speed * Time.deltaTime);

        
        if (Input.GetKeyDown(KeyCode.E))
        {
            _anim.SetTrigger("StopClimbing");
            _anim.speed = _animSpeed;
            _playerScript.enabled = true;
            this.enabled = false;
        }

        //climbing ladder control

        _anim.speed = Mathf.Pow(Input.GetAxis("Vertical"), 2);
        _anim.SetFloat("ClimbDirection", Input.GetAxis("Vertical"));

        //Secure snap into ladder

        transform.position = new Vector3(transform.position.x, transform.position.y, _ladder._zPosition);

        if(transform.position.y > _ladder._maxYPosition + 2)
        {
            transform.position = new Vector3(transform.position.x, _ladder._maxYPosition, transform.position.z);
        }
    }

    private void OnDisable()
    {
        _anim.speed = _animSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GetOutLadder") && this.enabled == true)
        {
            _controller.enabled = false;
            _playerScript.enabled = true;            
            this.enabled = false;
            _anim.SetTrigger("ClimbUp");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            _ladder = other.gameObject.GetComponent<Ladder>();
            int Zforward;
            if (other.transform.position.z < transform.position.z)
            {
                Zforward = -1;
            }
            else
            {
                Zforward = 1;
            }

            transform.forward = new Vector3(0, 0, Zforward);
        }
    }
}
