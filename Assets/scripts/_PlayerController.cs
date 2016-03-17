using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Rigidbody))]
public class _PlayerController : MonoBehaviour {
	Rigidbody myRigibody;
	Vector3 velocity;
	Vector3 iii;
	// Use this for initialization
	void Start () {
		myRigibody = GetComponent<Rigidbody> ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate()
	{
		myRigibody.MovePosition (myRigibody.position + velocity * Time.deltaTime) ;

	}

	public void LookAt(Vector3 v)
	{
		Vector3 _point = new Vector3 (v.x, transform.position.y, v.z);
		transform.LookAt (_point);
	}

	public void Move(Vector3 _velocity)
	{
		velocity = _velocity;
	}
}
