using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpPotion : MonoBehaviour
{
    [SerializeField] private int _hpPoints;

    private void OnCollisionEnter2D(Collision2D other)
    {
        PlayerControle player = other.gameObject.GetComponent<PlayerControle>();
        
        if (player != null)
        {
            player.AddHp(_hpPoints);
            Destroy(gameObject);
        }
    }
}