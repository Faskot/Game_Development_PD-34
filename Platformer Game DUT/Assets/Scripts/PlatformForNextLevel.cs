using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformForNextLevel : MonoBehaviour
{
    [SerializeField] private int _cristalToNextLevel;

     void Update()
    {
        
       
        PlayerControle   player = gameObject.GetComponent<PlayerControle>();
        if (player != null)
        {
            if (player.CristalAmount >= _cristalToNextLevel)
            {
                gameObject.SetActive(true);
            }
        }
       
    }
}


