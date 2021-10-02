using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPotion : MonoBehaviour
{
    
    [SerializeField] private int _ManaPoints;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerControle player = other.GetComponent<PlayerControle>();
        
        if (player != null)
        {
            player.AddMana(_ManaPoints);
            Destroy(gameObject);
        }
    }
}
