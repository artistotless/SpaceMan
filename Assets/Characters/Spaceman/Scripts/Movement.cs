using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movement : MonoBehaviour

{
    public float smooth = 0;
    [Range(0, 10.0f)] [SerializeField] public float speed;
    [Range(200, 10000.0f)] [SerializeField] public float jumpPower;
    public float jumpBooster;
    public Vector2 jumpBoosterPower;
    public Vector2 velocity;

    public QuickEffect jumpEffect;
    public ParticleSystem jumpParticle;
    
    public Transform groundCheckPoint;
    public LayerMask groundLayerMask;
    public bool isground = true;
    Collider2D[] colliders;
    float groundCheckRadius = 1.0f;
    float speedFall;

    Animator animator;
    Rigidbody2D rigidbody2d;
    Vector2 velocityFall;


    void Update()
    {
        JumpHolding();
        CheckJumping();

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

        jumpEffect.Show(
            new Vector3(transform.position.x + 1.31f, transform.position.y + 1.13705f, 0.0f));



        rigidbody2d.AddForceAtPosition(new Vector2(0.0f, 3.0f) * jumpPower, rigidbody2d.position, ForceMode2D.Force);

    }
    void JumpHolding()
    {

        if (Input.GetKey(KeyCode.E))
        {
            Debug.Log("WOOOOOOOOOW....");
            jumpBooster -= Time.deltaTime;
            if (jumpBooster > 0)
            {
                speedFall = 1;
                rigidbody2d.AddForceAtPosition(new Vector2(0.0f, 3.0f) * jumpBoosterPower, rigidbody2d.position, ForceMode2D.Force);
            }
           
            else { jumpBooster = 0; speedFall = 6; }

        }
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
        if (colliders.Length > 0)
        {

            animator.enabled = true;
            isground = true;
            jumpParticle.Stop();
            animator.SetBool("isGround", true);
            rigidbody2d.gravityScale = 1;

            if (animator.GetBool("isjump")) { animator.SetBool("isjump", false); }

        }
        else
        {
            if (!jumpParticle.isPlaying)
            {
                jumpParticle.Play();
            }

            // Свободное падение
            animator.enabled = false;
            isground = false;
            animator.SetBool("isGround", false);
            rigidbody2d.gravityScale += Time.deltaTime * speedFall;
        }

    }


    void FreezePosition(bool status)
    {
        rigidbody2d.constraints = !status ? RigidbodyConstraints2D.None : RigidbodyConstraints2D.FreezePositionX;
        rigidbody2d.freezeRotation = true;
    }


    void Moving()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

        animator.SetFloat("move", moveInput);

        if (moveInput != 0 || smooth != 0)
        {
            FreezePosition(false);

        }
        else
        {
            FreezePosition(true);
        }
        if (isground)
        {
            smooth = 0;
            rigidbody2d.MovePosition(rigidbody2d.position + velocity * moveInput * Time.fixedDeltaTime);
        }
        else
        {
            smooth = moveInput != 0 ? moveInput : smooth;
            if (moveInput == 0)
            {
                if (smooth > 0) { smooth -= Time.deltaTime; }

                else { smooth += Time.deltaTime; }

            }

            rigidbody2d.AddForce(velocity * smooth * speed);
        }
    }
    void FixedUpdate()
    {
        isGround();

        Moving();


    }
}

