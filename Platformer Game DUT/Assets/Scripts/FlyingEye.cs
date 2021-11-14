using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Pathfinding;

public class FlyingEye : MonoBehaviour
{
    public AIPath aiPath;
    [SerializeField] private int _damage;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerControle player = other.GetComponent<PlayerControle>();
        if (player != null)
        {
            player.TakeDamage(_damage, transform.position.x);
        }
    }
}
