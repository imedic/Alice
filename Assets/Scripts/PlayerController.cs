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
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]

public class PlayerController : MonoBehaviour
{

    public float speed = 5.0f;
    public float gravity = 10.0f;
    public float turnSmoothing = 5.0f;
    public float maxVelocityChange = 10.0f;
    private bool grounded = false;
    private Rigidbody rigidbody;
    static Animator anim;
    private int cookiesCollected = 0;
    public int cookieNumber = 100;
    private Vector3 movement;
    private Vector3 turning;
    private float turner;

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
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            // Calculate how fast we should be moving
            Vector3 targetVelocity = new Vector3(horizontal, 0, vertical);
            targetVelocity = transform.TransformDirection(targetVelocity);
            targetVelocity *= speed;

            //// Apply a force that attempts to reach our target velocity
            Vector3 velocity = rigidbody.velocity;
            Vector3 velocityChange = (targetVelocity - velocity);
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;
            rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);

            turner = Input.GetAxis("Horizontal") * turnSmoothing;
            //looker = -Input.GetAxis ("Mouse Y")* sensitivity;
            //looker = -Input.GetAxis ("Vertical")* sensitivity;
            if (turner != 0)
            {
                //Code for action on mouse moving right
                transform.eulerAngles += new Vector3(0, turner, 0);
            }

            //if (horizontal != 0 && vertical != 0)
            //{
            //    Rotate(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            //}
			if (Input.GetKeyDown (KeyCode.Space)) {
				anim.SetBool ("Jump", true);
			}
			else {
				anim.SetBool("Jump", false);
			}
			if (Input.GetKey(KeyCode.UpArrow)) {
				anim.SetBool ("IsWalking", true);
			}

			else {
				anim.SetBool("IsWalking", false);
			}
			if (Input.GetKey(KeyCode.DownArrow)) {
				anim.SetBool ("IsWalkingBackward", true);
			}

			else {
				anim.SetBool("IsWalkingBackward", false);
			}
			if (Input.GetKey(KeyCode.RightArrow) )  {
				anim.SetBool ("IsStraf", true);
			}

			else {
				anim.SetBool("IsStraf", false);
			}
			if (Input.GetKey(KeyCode.LeftArrow) )  {
				anim.SetBool ("IsStrafLeft", true);
			}

			else {
				anim.SetBool("IsStrafLeft", false);
			}
        }

        // We apply gravity manually for more tuning control
        rigidbody.AddForce(new Vector3(0, -gravity * rigidbody.mass, 0));
        //anim.SetBool("IsWalking", Input.GetAxis("Vertical") != 0);

        grounded = false;
    }

    void Rotate(float horizontal, float vertical)
    {
        Vector3 targetDirection = new Vector3(horizontal, 0, vertical);
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        Quaternion newRotation = Quaternion.Lerp(rigidbody.rotation, targetRotation, turnSmoothing * Time.deltaTime);
        rigidbody.MoveRotation(newRotation);
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
            transform.localScale += new Vector3(0.07F, 0.07F, 0.07F);
            Debug.Log(cookiesCollected);
            Destroy(other.gameObject);
        }

        if (other.tag == "Door" && cookiesCollected == cookieNumber)
        {
            Debug.Log("NOVI LEVEL!");
            //Application.LoadLevel(2);
        }

        if (other.tag == "Enemy")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
