//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerController : MonoBehaviour
//{
//    public float speed = 10.0F;
//    public float jumpSpeed = 0.0F;
//    public float gravity = 20.0F;
//    private Vector3 moveDirection = Vector3.zero;
//    private float turner;
//    public float sensitivity = 5.0F;
//    static Animator anim;
//    private CharacterController controller;


//    void Start()
//    {
//        anim = GetComponent<Animator>();
//    }

//    void Update()
//    {
//        controller = GetComponent<CharacterController>();
//        if (controller.isGrounded)
//        {
//            moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
//            moveDirection = transform.TransformDirection(moveDirection);
//            moveDirection *= speed;
//            if (Input.GetButton("Jump"))
//                moveDirection.y = jumpSpeed;

//        }
//        turner = Input.GetAxis("Horizontal") * sensitivity;
//        if (turner != 0)
//        {
//            transform.eulerAngles += new Vector3(0, turner, 0);
//        }

//        anim.SetBool("IsWalking", Input.GetAxis("Vertical") != 0);

//        moveDirection.y -= gravity * Time.deltaTime;
//        controller.Move(moveDirection * Time.deltaTime);
//    }


//    void OnTriggerEnter(Collider other)
//    {
//        Debug.Log(other);
//        if (other.tag == "Cookie")
//        {
//            Debug.Log("OnTriggerEnter() was called");
//            Destroy(other.gameObject);
//        }
//    }

//}

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]

public class PlayerController : MonoBehaviour
{

    public float speed = 10.0f;
    public float gravity = 10.0f;
    public float maxVelocityChange = 10.0f;
    public bool canJump = true;
    public float jumpHeight = 2.0f;
    private bool grounded = false;
    private Rigidbody rigidbody;
    static Animator anim;
    public int cookiesCollected = 0;
    public int cookieNumber = 100;

    void Awake()
    {
        anim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.freezeRotation = true;
        rigidbody.useGravity = false;
    }

    void FixedUpdate()
    {
        if (grounded)
        {
            // Calculate how fast we should be moving
            Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            targetVelocity = transform.TransformDirection(targetVelocity);
            targetVelocity *= speed;

            // Apply a force that attempts to reach our target velocity
            Vector3 velocity = rigidbody.velocity;
            Vector3 velocityChange = (targetVelocity - velocity);
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;
            rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);

            // Jump
            if (canJump && Input.GetButton("Jump"))
            {
                rigidbody.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
            }
        }

        // We apply gravity manually for more tuning control
        rigidbody.AddForce(new Vector3(0, -gravity * rigidbody.mass, 0));
        //anim.SetBool("IsWalking", Input.GetAxis("Vertical") != 0);

        grounded = false;
    }

    void OnCollisionStay()
    {
        grounded = true;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if (other.tag == "Cookie")
        {
            cookiesCollected++;
            transform.localScale += new Vector3(0.05F, 0.05F, 0.05F);
            Debug.Log(cookiesCollected);
            Destroy(other.gameObject);
        }

        if (other.tag == "Door" && cookiesCollected == cookieNumber)
        {
            Debug.Log("NOVI LEVEL!");
            //Application.LoadLevel(2);
        }
    }

    float CalculateJumpVerticalSpeed()
    {
        // From the jump height and gravity we deduce the upwards speed 
        // for the character to reach at the apex.
        return Mathf.Sqrt(2 * jumpHeight * gravity);
    }
}
