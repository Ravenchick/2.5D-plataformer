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

    private void Awake()
    {
        manager = this;
        scoreText = GameObject.Find("Score").GetComponent<TMP_Text>();
    }

    private void Update()
    {
        scoreText.text = "Score :" + GameManager.Manager._score;
    }
}
