using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager manager;
    public static GameManager Manager
    {
        get
        {
            if(manager == null)
            {
                Debug.Log("GameManager is null");
            }

            return manager;
        }
    }

    public int _score;

    private void Awake()
    {
        manager = this;
    }

    private void Start()
    {
        _score = 0;
    }



}
