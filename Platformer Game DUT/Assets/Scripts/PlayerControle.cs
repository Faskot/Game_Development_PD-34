using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControle : MonoBehaviour
{
    
    Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float groundCheckerRadius;
    [SerializeField] private float headCheckerRadius;
    [SerializeField] private SpriteRenderer spriteFlip;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private Transform headChecker;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsCell;
    [SerializeField] private Collider2D headCollider;
    [SerializeField] private int _maxHp;
    
    [Header(("Animation"))] 
    [SerializeField] private Animator _animator;

    [SerializeField] private string _runAnimatorKey;
    [SerializeField] private string _jumpAnimatorKey;
    [SerializeField] private string _crouchAnimatorKey;

    [Header("UI")]
    [SerializeField] private Slider _hpBar;


    private float horizontalMove;
    private float verticalMove;
    private bool jump;
    private bool crawl;
    private int _currentHp;

    private int CurrentHp
    {
        get => _currentHp;
        set
        {
            _currentHp = value;
            _hpBar.value = _currentHp;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Vector2 vector = new Vector2(10,11);
        rb = GetComponent<Rigidbody2D>();
        _hpBar.maxValue = _maxHp;
        CurrentHp = _maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");
        _animator.SetFloat(_runAnimatorKey, Mathf.Abs(horizontalMove));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }
        
        if (horizontalMove > 0 && spriteFlip.flipX) 
        {
            spriteFlip.flipX = false;
        }
        else if(horizontalMove < 0 && !spriteFlip.flipX) 
        {
            spriteFlip.flipX = true;
        }
        
        crawl = Input.GetKey(KeyCode.C);
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalMove * speed, rb.velocity.y);
        bool canJump = Physics2D.OverlapCircle(groundChecker.position, groundCheckerRadius, whatIsGround);
        bool canStand = !Physics2D.OverlapCircle(headChecker.position, headCheckerRadius, whatIsCell);
        headCollider.enabled = !crawl && canStand;
        
        if(jump && canJump)
        {
            rb.AddForce(Vector2.up * jumpForce);
            jump = false;
        }
        
        _animator.SetBool(_jumpAnimatorKey, !canJump);
        _animator.SetBool(_crouchAnimatorKey, !headCollider.enabled);
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundChecker.position, groundCheckerRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(headChecker.position, headCheckerRadius);
    }
    
    public void AddHp(int hpPoints)
    {
        CurrentHp += hpPoints;
        if (CurrentHp > _maxHp)
            CurrentHp = _maxHp;
    }
}
