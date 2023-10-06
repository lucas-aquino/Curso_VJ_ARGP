using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]   
public class PlayerController : MonoBehaviour, IDamageable
{

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

    private Transform camMainTransform;

    

    // Start is called before the first frame update
    void Start() 
    {
        Cursor.visible = false;
        camMainTransform = Camera.main.transform;
    }

    private void FixedUpdate()
    {
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
            

            /*float targetAngle = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + camMainTransform.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0f);

            rb.rotation = Quaternion.Lerp(rb.rotation, rotation, Time.deltaTime * rotationSpeed);*/

            //transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);

            animator.SetBool("run", true);

            return;
        }

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
        Debug.LogWarning("punch");
        animator.SetTrigger("punch");
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

    public void TakeDamage(float damage, HitType tipo)
    {

        damage *= (float)tipo;



    }
}