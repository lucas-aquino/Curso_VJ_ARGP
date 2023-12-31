using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]   
public class PlayerController : MonoBehaviour, IDamageable
{

    [SerializeField]
    private float _maxLife = 100f;

    private float _currentLife = 100f;

    [SerializeField] 
    private Rigidbody rb;

    [SerializeField]
    private PlayerInput playerInput;

    [SerializeField] 
    private Animator animator;

    [SerializeField] 
    private float rotationSpeed = 4f;
    
    [SerializeField]
    private float speed = 5.0f;

    [SerializeField]
    private float jumpForce = 10f;

    [SerializeField]
    private float _attackDamage = 10f;

    private bool _isTakingDamage = false;
    private bool _isAttack = false;

    private Transform camMainTransform;

    public float MaxLife
    {
        get
        {
            return _maxLife;
        }
    }

    public float CurrentLife {
        get
        {
            return _currentLife;
        }
        set
        {
            if(value < 0)
            {
                value = 0;
            }

            if(value > MaxLife)
            {
                value = MaxLife;
            }

            _currentLife = value;
        }
    }

    public bool IsDead
    {
        get { return CurrentLife <= 0; }
    }

    public float Damage { 
        get { return _attackDamage; } 
        set { _attackDamage = value; }
    }

    public bool IsAttack
    {
        get { return _isAttack; }
        set { _isAttack = value; }
    }

    public bool IsTackingDamage { 
        get { return _isTakingDamage; }
        set { _isTakingDamage = value; }
    }

    public string Tag => gameObject.tag;



    // Start is called before the first frame update
    void Start() 
    {
        Cursor.visible = false;
        CurrentLife = MaxLife;
        camMainTransform = Camera.main.transform;
    }

    private void FixedUpdate()
    {
        if (IsDead)
        {
            Destruir();
            return;
        }

        if (IsTackingDamage)
            return;

        if (IsAttack)
            return;

        Move();
        animator.SetBool("falling", !IsGrounded());
    }

    private void Move()
    {
        Vector2 input = playerInput.actions["Movement"].ReadValue<Vector2>();

        if (input != Vector2.zero)
        {
            Vector3 direction = rb.transform.forward;

            rb.velocity = new Vector3(direction.x * speed, rb.velocity.y, direction.z * speed);

            Vector3 inputV3 = new Vector3(input.x, 0f, input.y);

            float targetAngle = Mathf.Atan2(inputV3.x, inputV3.z) * Mathf.Rad2Deg + camMainTransform.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0f);

            rb.rotation = Quaternion.Lerp(rb.rotation, rotation, Time.deltaTime * rotationSpeed);
            
            animator.SetBool("run", true);

            return;
        }

        rb.velocity = new Vector3(0f, rb.velocity.y, 0f);

        animator.SetBool("run", false);
    }

    public void OnJump(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce * 10f);
            animator.SetTrigger("jump");
        }
    }

    public void OnPunch(InputAction.CallbackContext callbackContext) {
        if (IsAttack)
            return;

        Debug.LogWarning("punch");
        animator.SetTrigger("punch");
        Attack();
    }

    public void OnSamba(InputAction.CallbackContext callbackContext)
    {
        animator.SetTrigger("samba");
    }

    private bool IsGrounded()
    {
        RaycastHit hit;

        bool grounded = Physics.Raycast(transform.position, Vector3.down, out hit, 0.1f, Physics.DefaultRaycastLayers);

        return grounded;
    }

    public void OnHitAnimationStart()
    {
        _isTakingDamage = true;
    }

    public void OnHitAnimationEnd()
    {
        _isTakingDamage = false;
    }

    public void OnAttackAnimationStart()
    {
        _isAttack = true;
    }

    public void OnAttackAnimationEnd()
    {
        _isAttack = false;
    }

    public void TakeDamage(float damage, HitType tipo)
    {
        if(!IsDead)
        {
            damage *= ((float)tipo / 100f);

            Debug.Log($"Damage: {damage}, HitType: {tipo}");
        
            CurrentLife -= damage;
            animator.SetTrigger("hit");
        }

    }

    public void Attack()
    {
        Debug.Log("Attack");
    }

    public void Destruir()
    {
        animator.SetTrigger("death");
        gameObject.GetComponent<PlayerController>().enabled = false;
    }
}
