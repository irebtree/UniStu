using UnityEngine;
using System.Collections;

[RequireComponent(typeof(XController2d))]
public class XPlayer : MonoBehaviour {
	public float gravity = -20;

	public Vector3 velocity;
	XController2d controller;
	// Use this for initialization
	void Start () {
		controller = GetComponent<XController2d>();
	}
	
	// Update is called once per frame
	void Update () {
		velocity.y += gravity * Time.deltaTime;
		controller.Move(velocity * Time.deltaTime);
		//Debug.Log(velocity);
	}
}
