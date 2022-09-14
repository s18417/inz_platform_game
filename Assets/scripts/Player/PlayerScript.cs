using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    //children scripts
   
    [SerializeField]
    internal PlayerCollisionScript playerCollisionScript;

    //components
    public Rigidbody2D rb;
    public Animator anim;

 
    //player properties
    public float movespeed;
    public float jumpforce;
    //jump properties
    public bool canjump;
    public float timeSinceGrounded;
    public bool jumpGravTicket;
    //wallslide props
    public float wallslideGravity=0.6f;
    public float standardGravity=3.0f;
    //bjump
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    private bool wallJump;
    

    //input 
    public float movementAxis;

   
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        
    }

    
    void FixedUpdate()
    {
        Bjump();
        MyInput();
        CheckAir();
    }

    void Bjump()
    {
        if(rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }else if(rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    void MyInput() {
        
        movementAxis = Input.GetAxisRaw("Horizontal");
        Vector2 movDir = new Vector2(movementAxis,0);
        movDir.Normalize();
        if (movDir != Vector2.zero)
        {
            
            transform.right = movDir;
            if (playerCollisionScript.isGrounded)
            {
                anim.Play("HeroKnight_Run");
            }
        }
        else if (playerCollisionScript.isGrounded && anim.GetInteger("attackcounter")==0)
        {
            anim.Play("HeroKnight_Idle");
            rb.velocity = Vector2.zero;
            wallJump = false;
        }
        
        if (!wallJump && playerCollisionScript.isGrounded)
        {
            rb.velocity = new Vector2(movementAxis * movespeed, rb.velocity.y);
        } else if (!wallJump && !playerCollisionScript.isGrounded)
        {
            rb.AddForce(new Vector2(movementAxis * movespeed/10, 0), ForceMode2D.Impulse);
        }
    }
    private void Update()
    {
        //Debug.DrawRay(new Vector2(transform.position.x-0.3f,transform.position.y-0.3f), new Vector2(0,-0.01f));
        CheckJump();
        CheckWallSlide();
    }

    private void CheckAir()
    {
        if (playerCollisionScript.isGrounded) jumpGravTicket = true;
        
        if (!playerCollisionScript.isGrounded && !playerCollisionScript.wallSensedFront)
        {
            
            switch (rb.velocity.y)
            {
                case > 0.5f :
                    anim.Play("HeroKnight_Jump");
                    break;
                case < -0.5f :
                    anim.Play("HeroKnight_Fall");
                    break;
                case var n when jumpGravTicket && n is < 0.5f and > -0.5f:
                    if (jumpGravTicket)
                    {
                        StartCoroutine(Gravity(1f));
                        
                        jumpGravTicket = false;
                    }

                    break;
            }
        }
    }

    private void CheckJump() {
        if (Input.GetButtonDown("Jump") && canjump)
        {
            playerCollisionScript.disableColliders(0.5f, "ground");
            playerCollisionScript.isGrounded = false;
            rb.velocity=new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
            canjump = false;
            
        }
        if (playerCollisionScript.isGrounded )
        {
            timeSinceGrounded = 0f;
            canjump = true;            
        }
        else  {
            timeSinceGrounded += Time.deltaTime;          
        }
        if (timeSinceGrounded > 0.25f) canjump = false;
    }

    private float CheckGroundDist()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x,transform.position.y-0.3f), new Vector2(0,-0.01f));
        float distanceToGround = hit.distance;
        return distanceToGround;
    }

    private void CheckWallSlide() {

     
        if (playerCollisionScript.wallSensedFront&& !playerCollisionScript.isGrounded&& CheckGroundDist()>0.5)
        {
            
                anim.Play("HeroKnight_WallSlide");
                rb.gravityScale = wallslideGravity;
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y*0.93f );
                if (Input.GetButtonDown("Jump"))
                {
                    wallJump = true;
                    StartCoroutine(wallJumped(0.5f));
                    if (CheckGroundDist() > 0.5)
                    {
                        if (transform.rotation == new Quaternion(0, 1, 0, 0))
                            rb.AddForce(new Vector2(jumpforce / 2, jumpforce*1.5f), ForceMode2D.Impulse);
                        else
                            rb.AddForce(new Vector2(-jumpforce / 2, jumpforce*1.5f), ForceMode2D.Impulse);
                    }
                   
                }
        }
        else
        {
            rb.gravityScale = standardGravity;
        }
    }

    
    IEnumerator wallJumped(float time)
    {
        yield return new WaitForSeconds(time);
        wallJump = false;
    }

    IEnumerator Gravity(float apexGravScale)
    {
        var mem = standardGravity;
        standardGravity = apexGravScale;
        yield return new WaitForSeconds(0.1f);
        
        standardGravity = mem;
    }

    

}
