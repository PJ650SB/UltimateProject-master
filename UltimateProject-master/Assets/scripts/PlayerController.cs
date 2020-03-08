using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D selfRigidbody;


    public float speed;
    public float JumpForce;
    public Transform FeetRectangleTopLeft;
    public Transform FeetRectangleBottomRight;
    public LayerMask GroundLayer;
    private bool isGrounded = false;
    private bool canJump = false;
    private Animator selfAnimator;
    private float horizontalMovement;
    public bool IsAttacking = false;

    // Start is called before the first frame update
    void Start()

    {
        selfRigidbody = GetComponent<Rigidbody2D>();
        selfAnimator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = Input.GetAxis("Horizontal");


        if (Input.GetButton("Jump") && isGrounded)
        {
            canJump = true;
            selfAnimator.SetTrigger("Jump");
        }
        if (Mathf.Abs(horizontalMovement) > 0.1f)
        {
            selfAnimator.SetBool("Runs", true);

        }
        else
        {
            selfAnimator.SetBool("Runs", false);
        }
        if ((horizontalMovement > 0.1f && transform.localScale.x < 0) ||
        (horizontalMovement < -0.1f && transform.localScale.x > 0))
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
        if (Input.GetButton("Fire1"))
        {
            selfAnimator.SetTrigger("Attack");
            IsAttacking = true;
        }




    }
    public void ResetAttacking()
    {
        IsAttacking = false;
    }
    // Fixed update is called every x to x ms
    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapArea(FeetRectangleTopLeft.position, FeetRectangleBottomRight.position,
            GroundLayer);

        if (canJump)
        {
            canJump = false;
            isGrounded = false;
            selfRigidbody.AddForce(new Vector2(0, 1) * JumpForce, ForceMode2D.Impulse);
        }


        selfRigidbody.velocity = new Vector2(speed * horizontalMovement, selfRigidbody.velocity.y);

    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<EnemyController>() != null && !col.gameObject.GetComponent<EnemyController>().IsDead && !IsAttacking)
        {



            SceneManager.LoadScene ("New scene");
        }
        
    }
}
