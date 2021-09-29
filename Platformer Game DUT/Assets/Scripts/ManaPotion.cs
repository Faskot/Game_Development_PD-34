using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPotion : MonoBehaviour
{
    
    [SerializeField] private int _ManaPoints;

    private void OnCollisionEnter2D(Collision2D other)
    {
        PlayerControle player = other.gameObject.GetComponent<PlayerControle>();
        
        if (player != null)
        {
            player.AddMana(_ManaPoints);
            Destroy(gameObject);
        }
    }
}
