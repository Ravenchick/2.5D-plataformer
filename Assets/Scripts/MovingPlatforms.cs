using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    [SerializeField] private Vector3[] point;
    [SerializeField] private Vector3 tarjetPoint;
    private int pointNumber = 0;
    private bool countingBack = false;
    [SerializeField] private float speed;
    [SerializeField] private bool circleMove;
    

    private Rigidbody _rbd;

    private void Awake()
    {
        _rbd = GetComponent<Rigidbody>();
    }

    

    private void FixedUpdate()
    {

        transform.position = Vector3.MoveTowards(transform.position, tarjetPoint, speed * Time.deltaTime);

        tarjetPoint = point[pointNumber];

        if (transform.position == tarjetPoint && countingBack == false)
        {
            pointNumber++;
            if (pointNumber >= point.Length)
            {
                countingBack = true;
                pointNumber = point.Length;
            }
        }
        if(transform.position == tarjetPoint && countingBack == true)
        {

            if (circleMove == false)
            {
                pointNumber--;
                if (pointNumber <= 0)
                {
                    countingBack = false;
                    pointNumber = 0;
                }
            }
            else
            {
                pointNumber = 0;
                countingBack = false;
            }

            
        }

        
        
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
