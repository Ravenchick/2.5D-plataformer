using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLedge : MonoBehaviour
{

    private Player _player;
    [SerializeField] private Vector3 _snapingPosition;
    [SerializeField] private Vector3 _climbUpSnap;

    private void Awake()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LedgeChecker"))
        {
            _player.LedgeGrab(_climbUpSnap);
        }

        _player.transform.position = _snapingPosition;
    }
}
