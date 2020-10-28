using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbUp : StateMachineBehaviour
{
    private Player _player;
    
        private void Awake()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player.transform.position = _player._climbUpPosition;
        //_playerController.enabled = true;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player.ClimbUp();
        
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player.playerVelocity.y = 0;
    }

}
