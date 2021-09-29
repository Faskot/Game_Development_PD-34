using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cristal : MonoBehaviour
{
    [SerializeField] private int _CristalPoints;

    private void OnCollisionEnter2D(Collision2D other)
    {
        PlayerControle player = other.gameObject.GetComponent<PlayerControle>();
        
        if (player != null)
        {
            player.AddCristal(_CristalPoints);
            Destroy(gameObject);
        }
    }
}
