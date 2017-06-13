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

            Vector3 targetVelocity = new Vector3(horizontal, 0, vertical);
            targetVelocity = transform.TransformDirection(targetVelocity);
            targetVelocity *= speed;

            Vector3 velocity = rigidbody.velocity;
            Vector3 velocityChange = (targetVelocity - velocity);
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;
            rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);

            turner = Input.GetAxis("Horizontal") * turnSmoothing;
            if (turner != 0)
            {
                transform.eulerAngles += new Vector3(0, turner, 0);
            }

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

        rigidbody.AddForce(new Vector3(0, -gravity * rigidbody.mass, 0));

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
        if (other.tag == "Cookie")
        {
            cookiesCollected++;
            transform.localScale += new Vector3(0.05F, 0.05F, 0.05F);
            Destroy(other.gameObject);
        }

        if (other.tag == "Door" && cookiesCollected == cookieNumber)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if (other.tag == "Enemy")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
