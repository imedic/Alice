using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public Transform[] patrolPoints;
	public int speed = 5;
	private int currentPoint;
    private Vector3 nextDirection;
    private Quaternion lookRotation;

	// Use this for initialization
	void Start () {
		currentPoint = 0;
		transform.position = patrolPoints[0].position;
	}

	// Update is called once per frame
	void Update () {
		if (transform.position == patrolPoints [currentPoint].position) {
			currentPoint++;
		}

		if (currentPoint >= patrolPoints.Length) {
			currentPoint = 0;
		}

	    nextDirection = (patrolPoints[currentPoint].position - transform.position).normalized;
	    lookRotation = Quaternion.LookRotation(nextDirection);

	    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10);

        transform.position = Vector3.MoveTowards (transform.position, patrolPoints[currentPoint].position, speed * Time.deltaTime);

	}
}
