using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private Player _player;
    [SerializeField] private Vector3 _position;
    public float _zPosition;
    public float _maxYPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _player = other.gameObject.GetComponent<Player>();
            _player._climbUpPosition = _position;
        }
    }
}
