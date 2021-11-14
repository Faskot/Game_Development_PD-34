using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    [SerializeField] private float _walkRange;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _speed;
    [SerializeField] private bool _faceRight;
    [SerializeField] private int _damage;
    
    private Vector2 _startPostion;
    
    [Header(("Animation"))] 
    [SerializeField] private Animator _animator;

    [SerializeField] private string _walkAnimatorKey;
    [SerializeField] private string _atackAnimatorKey;
    private Vector2 _drawPostion
    {
        get
        {
            if (_startPostion == Vector2.zero)
                return transform.position;
            else
                return _startPostion;
        }
    }
    private void Start()
    {
        _startPostion = transform.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(_drawPostion, new Vector3(_walkRange*2,1, 0));
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = transform.right * _speed;
    }

    private void Update()
    {
        float xPos = transform.position.x;
        if (xPos > _startPostion.x + _walkRange && _faceRight)
        {
            Flip();
        }
        else if (xPos < _startPostion.x - _walkRange && !_faceRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        _faceRight = !_faceRight;
        transform.Rotate(0,180,0);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        PlayerControle player = other.collider.GetComponent<PlayerControle>();
        if (player != null)
        {
            player.TakeDamage(_damage, transform.position.x);
            if (_animator)
            {
                _animator.SetTrigger("Attack");
            }
        }
    }

}

