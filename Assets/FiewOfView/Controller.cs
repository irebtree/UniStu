using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {
	public float speed = 6f;

	Rigidbody rig;
	Camera viewCam;

	Vector3 velocity;
	Vector3 mousePos;
	// Use this for initialization
	void Start () {
		rig = GetComponent<Rigidbody>();
		viewCam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		//velocity = new Vector3(Input.GetAxis("Horizontal"),0f,Input.GetAxis("Vertical")).normalized * speed;
		velocity = new Vector3(Input.GetAxisRaw("Horizontal"),0f,Input.GetAxisRaw("Vertical")).normalized * speed;

		mousePos = viewCam.ScreenToWorldPoint(new Vector3( Input.mousePosition.x, Input.mousePosition.y, viewCam.transform.position.y));
		transform.LookAt(mousePos + Vector3.up);


	}

	void FixedUpdate()
	{
		rig.MovePosition(rig.position + velocity * Time.deltaTime);
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position, mousePos);

		if(Input.GetMouseButton(0))
			Gizmos.DrawSphere(mousePos,0.5f);
	}
}
