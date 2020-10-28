using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class UIManager : MonoBehaviour
{
    private static UIManager manager;
    public static UIManager Manager
    {
        get
        {
            if(manager == null)
            {
                Debug.Log("UIManager is null");
            }

            return manager;
        }
    }

    private TMP_Text scoreText;
    private TMP_Text climbUp;
    private Player _player;
    private Animator _anim;
    private ClimbLadder _climbScript;
    private CharacterController _controller;

    private void Awake()
    {
        manager = this;
        scoreText = GameObject.Find("Score").GetComponent<TMP_Text>();
        climbUp = GameObject.Find("Climbing Text").GetComponent<TMP_Text>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        _anim = GameObject.Find("Model").GetComponent<Animator>();
        _climbScript = GameObject.Find("Player").GetComponent<ClimbLadder>();
        _controller = GameObject.Find("Player").GetComponent<CharacterController>();
    }
        
    private void Update()
    {
        scoreText.text = "Score :" + GameManager.Manager._score;



        //Climb up text        

        if (_player._hangingFromLedge)
        {
            climbUp.enabled = true;
        }

        else if (_player._onLadderRange && _player.enabled == true && _controller.enabled == true)
        {
            climbUp.enabled = true;
        }

        else
        {
            climbUp.enabled = false;
        }
    }
}
