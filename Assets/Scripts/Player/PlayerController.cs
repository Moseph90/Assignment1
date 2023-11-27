using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;
    Transform trans;

    Coroutine jumpForceChange;

    public int maxLives = 5;

    private int _lives = 3;
    private int _score = 0;
    private int _magic = 5;
    public int magic
    {
        get => _magic;
        set
        {
            _magic = value;
        }
    }
    public int score
    {
        get => _score;
        set
        {
            _score = value;
            Debug.Log("Score Has Been Set To: " + _score.ToString());
        }
    }
    public int lives
    {
        get => _lives;
        set 
        {
            _lives = value;
            if (_lives > maxLives ) _lives = maxLives;

            //if (_lives < 0)
            Debug.Log("Lives Has Been Set To: " + _lives.ToString());      
        }
    }

    //Movement variables
    public float speed = 5.0f;
    public float jumpForce = 300.0f;

    //Ground check
    public bool isGrounded;
    public bool isAttack;
    public bool isAirAttack;
    public Transform groundCheck;
    public LayerMask isGroundLayer;
    public float groundCheckRadius = 0.02f;

    //Player Transform
    public Transform PlayerTransform;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        if (rb == null) Debug.Log("No RigidBody Reference");
        if (sr == null) Debug.Log("No Sprite Renerer Reference");
        if (anim == null) Debug.Log("No Anim Reference");
        if (groundCheckRadius <= 0)
        {
            groundCheckRadius = 0.02f;
            Debug.Log("Ground Check Radius Was Set To Default");
        }
        if (jumpForce <= 0)
        {
            jumpForce = 300.0f;
            Debug.Log("Jump Force Was Set To Default");
        }
        if (speed <= 0)
        {
            speed = 5.0f;
            Debug.Log("Speed Was Set To Default");
        }
        if (groundCheck == null)
        {
            GameObject obj = new GameObject();
            obj.transform.SetParent(gameObject.transform);
            obj.transform.localPosition = Vector3.zero;
            obj.name = "GroundCheck";
            groundCheck = obj.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float hInput = Input.GetAxisRaw("Horizontal");

        if (hInput < 0f)
        {
            sr.flipX = true;
        }
        else if (hInput > 0f)
        {
            sr.flipX = false;
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector2.up * jumpForce);
        }
        Vector2 moveDirection = new Vector2(hInput * speed, rb.velocity.y);
        rb.velocity = moveDirection;

        anim.SetFloat("hInput", Mathf.Abs(hInput));
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isAttack", isAttack);
        anim.SetBool("isAirAttack", isAirAttack);

        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (Input.GetKey(KeyCode.LeftControl))
        {
            anim.SetTrigger("attack");
            isAttack = true;
        }
        if (stateInfo.normalizedTime >= 1.0f)
        {
            anim.ResetTrigger("attack");
            anim.ResetTrigger("airAttack");
            isAttack = false;
            isAirAttack = false;
        }
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.Space))
        {
            anim.SetTrigger("airAttack");
            isAirAttack = true;
        }
        
    }
    
    public void IncreaseGravity()
    {
        rb.gravityScale = 10;
    }

    /* Did not work properly because needs to collide with object before calculating distance therefore cannot 
     * prevent player from slowing down when colliding with the power ups.
     
    private void OnCollisionEnter2D(Collision2D collision)
    {
        BoxCollider2D collider = collision.gameObject.GetComponent<BoxCollider2D>();
        if (collision.gameObject.layer == LayerMask.NameToLayer("PowerUps"))
        {
            float distance = Vector2.Distance(transform.position, collision.gameObject.transform.position);
            
            if (distance == 10f)
            {
                collider.enabled = false;
                Destroy(collision.gameObject);
            }
        }
    } */
    //How Hisham did it for future reference
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PowerUps"))
        {
            Destroy(collision.gameObject);   
        }
    }
    public void StartJumpForceChange()
    {
        if (jumpForceChange == null) jumpForceChange = StartCoroutine(JumpForceChange());
        else
        {
            StopCoroutine(jumpForceChange);
            jumpForceChange = null;
            jumpForce /= 2;
            jumpForceChange = StartCoroutine(JumpForceChange());
        }   
    }
    IEnumerator JumpForceChange()
    {
        jumpForce *= 2;
        yield return new WaitForSeconds(5.0f);
        jumpForce /= 2;
        jumpForceChange = null;
    }
}