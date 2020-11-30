using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Movement : MonoBehaviour

{

    internal double distance;
    [Range(0, 10.0f)] [SerializeField] public float speedWalk, speedFly;
    [Range(200, 10000.0f)] [SerializeField] public float jumpVelocity;
    public float BoosterCount;
    public Vector2 BoosterVelocity;
    Vector2 velocity;

    public QuickEffect jumpEffect;
    public ParticleSystem boostParticle;

    public Transform groundCheckPoint;
    public LayerMask groundLayerMask;
    public bool isground = true;
    Collider2D[] colliders;
    float groundCheckRadius = 1.0f;
    public float speedFall;
    float smooth = 0;

    Animator animator;
    Rigidbody2D rigidbody2d;
    Vector2 velocityFall;
    ShootSystem ssystem;


    void Update()
    {
        JumpHolding();
        CheckJumping();
        if (Input.GetKeyDown(KeyCode.F1)) ssystem.enabled = ssystem.enabled ? false : true;

    }
    void Start()
    {
        ssystem = GetComponent<ShootSystem>();
        velocity = new Vector2(speedWalk, 0.0f);
        velocityFall = new Vector2(0, speedFall);
        animator = transform.gameObject.GetComponent<Animator>();
        rigidbody2d = transform.gameObject.GetComponent<Rigidbody2D>();

    }
    void Jump()
    {

        jumpEffect.Show(
            new Vector3(transform.position.x + 1.31f, transform.position.y + 1.13705f, 0.0f));



        rigidbody2d.AddForceAtPosition(new Vector2(0.0f, 3.0f) * jumpVelocity, rigidbody2d.position, ForceMode2D.Force);

    }
    void JumpHolding()
    {

        if (Input.GetKey(KeyCode.E))
        {
            Debug.Log("WOOOOOOOOOW....");
            BoosterCount -= Time.deltaTime;
            if (BoosterCount > 0)
            {
                if (!boostParticle.isPlaying)
                {
                    boostParticle.Play();
                }
                speedFall = 1;
                FreezePosition(false);
                rigidbody2d.AddForceAtPosition(new Vector2(0.0f, 2.0f) * BoosterVelocity, rigidbody2d.position, ForceMode2D.Force);
                //rigidbody2d.AddForceAtPosition(new Vector2(ssystem.isFlipped ? -2.0f : 2.0f, 2.0f) * BoosterVelocity, rigidbody2d.position, ForceMode2D.Force);

            }

            else { BoosterCount = 0; speedFall = 6; }

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
            boostParticle.Stop();
            animator.SetBool("isGround", true);
            rigidbody2d.gravityScale = 1;

            if (animator.GetBool("isjump")) { animator.SetBool("isjump", false); }

        }
        else
        {


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

            rigidbody2d.AddForce(velocity * smooth * speedFly);
        }
    }
    void FixedUpdate()
    {
        isGround();

        Moving();





    }
}

