using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Vector3[] _floors;
    [SerializeField] private Vector3 _tarjet;
    private bool _moving = true;

    private void Start()
    {
        _tarjet = _floors[0];        
    }

    
    private void FixedUpdate()
    {
        if (transform.position != _tarjet && _moving == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, _tarjet, _speed * Time.deltaTime);

        }

        if (transform.position == _tarjet && _moving == true)
        {
            _moving = false;
            StartCoroutine(movement());
        }
    }
    private IEnumerator movement()
    {
        yield return new WaitForSeconds(5f);

        if (_tarjet == _floors[0])
        {
            _tarjet = _floors[1];
        }
        else
        {
            _tarjet = _floors[0];
        }
        _moving = true;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.parent = this.transform;
            
        }
    }   

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.parent = null;
            
        }
    }
}
