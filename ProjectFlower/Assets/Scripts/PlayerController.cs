using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public float moveInput;

    public bool Fr = true;


    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private int moreJump;
    public int moreJumpValue;
    private float jumpTimeCounter;
    public float jumpTime;
    public bool isJumping;


    private Rigidbody2D rb;

    void Start(){
        moreJump = moreJumpValue;

        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate(){

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);


        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if (Fr == false && moveInput > 0)
        {
            Flip();
        } else if(Fr == true && moveInput < 0){
            Flip();
        
        }
    }

    void Update(){

        if(isGrounded == true){
            moreJump = moreJumpValue;
        }

        if (Input.GetButton("Jump") && isJumping == true){
            if(jumpTimeCounter >0){
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }else{
                isJumping = false;
            }
            
        }
        if (Input.GetButton("Jump")){
            isJumping = false;
        }

        if (Input.GetButtonDown("Jump") && moreJump > 0){
            rb.velocity = Vector2.up * jumpForce;
            isJumping = true;
            jumpTimeCounter = jumpTime;
            moreJump--;
        } else if(Input.GetButtonDown("Jump") && moreJump == 0 && isGrounded == true){
            rb.velocity = Vector2.up * jumpForce;




        }
    }

    void Flip(){
        Fr = !Fr;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

}
