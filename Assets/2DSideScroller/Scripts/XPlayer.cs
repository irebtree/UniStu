using UnityEngine;
using System.Collections;

[RequireComponent(typeof(XController2d))]
public class XPlayer : MonoBehaviour {

	public float jumpHeight = 4;
	public float timeToJumpApex = .4f;
	public float gravity = -20;
	public float jumpVelocity;
	public float moveSpeed = 6;
	public Vector3 velocity;

	float accelerationTimeAirborne = .2f;
	float accelerationTimeGrounded = .1f;
	float velocitySmoothing;
	XController2d controller;
	// Use this for initialization
	void Start () {
		controller = GetComponent<XController2d>();
		gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2); 
		jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
	}
	
	// Update is called once per frame
	void Update () {

		if(controller.collisions.above || controller.collisions.below)
			velocity.y = 0;

		Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

		if(Input.GetKeyDown(KeyCode.Space) && controller.collisions.below)
		{
			velocity.y = jumpVelocity;
		}

		//velocity.x = moveSpeed * input.x;
		float targetVelocityX = moveSpeed * input.x;
		velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocitySmoothing, controller.collisions.below?accelerationTimeGrounded:accelerationTimeAirborne);
		velocity.y += gravity * Time.deltaTime;
		controller.Move(velocity * Time.deltaTime);
		//Debug.Log(velocity);
	}
}
