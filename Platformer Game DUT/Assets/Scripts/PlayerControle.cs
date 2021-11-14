using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControle : MonoBehaviour
{
    
    Rigidbody2D _rb;
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
    [SerializeField] private int _maxMana;
   
    
    [Header(("Animation"))] 
    [SerializeField] private Animator _animator;

    [SerializeField] private string _runAnimatorKey;
    [SerializeField] private string _jumpAnimatorKey;
    [SerializeField] private string _crouchAnimatorKey;
    [SerializeField] private string _diedAnimatorKey;
    
    [Header("UI")]
    [SerializeField] private TMP_Text _cristalAmountText;
    [SerializeField] private Slider _hpBar;
    [SerializeField] private Slider _manaBar;


    private float horizontalMove;
    private float verticalMove;
    private bool jump;
    private bool crawl;
    private int _cristalAmount;
    private int _currentHp;
    private int _currentMana;
    private float _lastHurtTime;

    public int CristalAmount
    {
        get => _cristalAmount;
        set
        {
            _cristalAmount = value;
            _cristalAmountText.text = value.ToString();
        }
    }
    private int CurrentMana
    {
        get => _currentMana;
        set
        {
            _currentMana = value;
            _manaBar.value = _currentMana;
        }
    }
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
    
        CristalAmount = 0;
        
        _manaBar.maxValue = _maxMana;
        CurrentMana = _maxMana;
        
        _hpBar.maxValue = _maxHp;
        CurrentHp = _maxHp;
        Vector2 vector = new Vector2(10,11);
        _rb = GetComponent<Rigidbody2D>();
        
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
        _rb.velocity = new Vector2(horizontalMove * speed, _rb.velocity.y);
        bool canJump = Physics2D.OverlapCircle(groundChecker.position, groundCheckerRadius, whatIsGround);
        bool canStand = !Physics2D.OverlapCircle(headChecker.position, headCheckerRadius, whatIsCell);
        headCollider.enabled = !crawl && canStand;
        
        if(jump && canJump)
        {
            _rb.AddForce(Vector2.up * jumpForce);
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
        
        int missingHp = _maxHp - CurrentHp;
        int pointToAdd = missingHp > hpPoints ? hpPoints : missingHp;
        StartCoroutine(RestoreHp(pointToAdd));
        Debug.Log("HPrised" + hpPoints);
    }

    public void AddMana(int manaPoints)
    {
        int missingMana = _maxMana - CurrentMana;
        int pointToAdd = missingMana > manaPoints ? manaPoints : missingMana;
        StartCoroutine(RestoreMana(pointToAdd));
        Debug.Log("Mana raised" + manaPoints);
    }
    private IEnumerator RestoreMana(int pointToAdd)
    {
        while (pointToAdd != 0)
        {
            pointToAdd--;
            CurrentMana++;
            yield return new WaitForSeconds(0.2f);
        }
    }
    private IEnumerator RestoreHp(int pointToAdd)
    {
        while (pointToAdd != 0)
        {
            pointToAdd--;
            CurrentHp++;
            yield return new WaitForSeconds(0.2f);
        }
    }
    
    public void TakeDamage(int damage, float pushPower = 0, float posX = 0)
    {
        
        CurrentHp -= damage;
       
        if (CurrentHp <= 0)
        {
            _animator.SetBool(_diedAnimatorKey, CurrentHp <= -0.01f);
            Invoke(nameof(ReloadScene), 2f);
        }

        if (pushPower != 0 && Time.time - _lastHurtTime > 0.5f)
        {
            _lastHurtTime = Time.time;
            int direction = posX > transform.position.x ? -1 : 1;
            _rb.AddForce(new Vector2(direction * pushPower/4, pushPower));
        }
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

