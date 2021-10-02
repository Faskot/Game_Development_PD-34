using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _damageDelay;

    private float _lastDamageTime;
    private PlayerControle _player;

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerControle player = other.GetComponent<PlayerControle>();
        if (player != null)
        {
            _player = player;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        PlayerControle player = other.GetComponent<PlayerControle>();
        if (player == _player)
        {
            _player = null;
        }
    }

    private void Update()
    {
        if (_player != null && Time.time - _lastDamageTime > _damageDelay)
        {
            _lastDamageTime = Time.time;
            _player.TakeDamage(_damage);
        }
    }
}
