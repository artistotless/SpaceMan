using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movement : MonoBehaviour

{
    [Range(0, 10.0f)]  [SerializeField]  public float speed;
    [Range(200, 10000.0f)] [SerializeField] public float jumpPower;
    [Range(1, 100.0f)] public float speedFall;
    Vector2 velocity;

    Collider2D[] colliders;
    public Transform groundCheckPoint;
    float groundCheckRadius = 1.0f;
    public LayerMask groundLayerMask;
    public bool isground = true;
    Animator animator;
    public Vector2 velocityRigid;
    Rigidbody2D rigidbody2d;
    Vector2 velocityFall;


    void Update()
    {
        
        CheckJumping();

        velocityRigid = rigidbody2d.velocity;
        


    }
    void Start()
    {

        
       
        velocity = new Vector2(speed, 0.0f);
        velocityFall = new Vector2(0, speedFall);
        animator = transform.gameObject.GetComponent<Animator>();
        rigidbody2d = transform.gameObject.GetComponent<Rigidbody2D>();
        
    }
    void Jump()
    {
        rigidbody2d.AddForceAtPosition(new Vector2(0.0f, 3.0f) * jumpPower, rigidbody2d.position, ForceMode2D.Force);

    }
    void CheckJumping()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isground)
            {
                Debug.Log("Space key was pressed.");
                animator.SetBool("isjump", true);

            }

            
        }
    }

    void isGround()
    {

        colliders = Physics2D.OverlapCircleAll(groundCheckPoint.position, groundCheckRadius, groundLayerMask);
        if ( colliders.Length > 0) {
            animator.enabled = true;
            isground = true;
            animator.SetBool("isGround", true);
            rigidbody2d.gravityScale = 1;

            if (animator.GetBool("isjump")) {animator.SetBool("isjump", false); }
            
        } else {
            animator.enabled = false;
            isground = false;
            animator.SetBool("isGround", false);
            rigidbody2d.gravityScale += Time.deltaTime*speedFall;
        }
           
        }
    
 
    void Moving()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("move", moveInput);
        
        if (moveInput != 0) { 
        rigidbody2d.constraints = RigidbodyConstraints2D.None;
        rigidbody2d.freezeRotation = true;
        }

        else
        {
            rigidbody2d.constraints = RigidbodyConstraints2D.FreezePositionX;
            rigidbody2d.freezeRotation = true;
        }

        rigidbody2d.MovePosition(rigidbody2d.position + velocity * moveInput * Time.fixedDeltaTime);
        
    }

    void FixedUpdate()
    {
        velocityRigid = rigidbody2d.velocity;
        isGround();
        
        Moving();
        

    }
}
