using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    private float horizontalMove;
    private float verticalMove;
    private bool jump;
    private bool crawl;
    
    // Start is called before the first frame update
    void Start()
    {
        Vector2 vector = new Vector2(10,11);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");
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
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundChecker.position, groundCheckerRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(headChecker.position, headCheckerRadius);
    }
}
