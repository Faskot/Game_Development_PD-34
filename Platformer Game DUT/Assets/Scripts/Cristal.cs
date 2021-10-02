using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cristal : MonoBehaviour
{
    [SerializeField] private int _cristalAmount;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerControle player = other.GetComponent<PlayerControle>();
        
        if (player != null)
        {
            player.CristalAmount += +_cristalAmount;
            Debug.Log("Cristal raised" + _cristalAmount );
            Destroy(gameObject);
        }
    }
}
