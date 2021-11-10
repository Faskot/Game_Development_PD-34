using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformForNextLevel : MonoBehaviour
{
    [SerializeField] private int _cristalToNextLevel;

     void FixedUpdate()
    {
        
       
       PlayerControle  player = gameObject.GetComponent<PlayerControle>();
        if (player != null)
        {
           
            if (player.CristalAmount >= _cristalToNextLevel)
            {
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
       
    }
}


