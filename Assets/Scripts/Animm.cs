using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animm : MonoBehaviour {

	static Animator anim;
	public float speed = 2.0f;
	public float RotationSpeed = 75.0f;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		float translation = Input.GetAxis("Vertical")*speed;
		float rotation = Input.GetAxis("Horizontal")*RotationSpeed;
		translation *= Time.deltaTime;
		rotation *= Time.deltaTime;

		transform.Translate (0, 0, translation);
		transform.Rotate (0, rotation, 0);

		if (translation != 0) {
			anim.SetBool ("IsWalking", true);
		}

		else {
			anim.SetBool("IsWalking", false);
		
		}
	}
}
