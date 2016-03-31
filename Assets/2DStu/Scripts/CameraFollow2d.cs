using UnityEngine;
using System.Collections;

public class CameraFollow2d : MonoBehaviour {
	public Transform target;

	public float smoothing = 1f;
	public float lowY = 0;

	Vector3 offset;
	// Use this for initialization
	void Start () {
		lowY = transform.position.y;
		offset = transform.position - target.position;
	}
	

	void FixedUpdate () {
		if(target != null)
		{
			Vector3 targetCamPos = target.position + offset;
			
			transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing);
			
			if(transform.position.y < lowY)
			{
				transform.position = new Vector3(transform.position.x, lowY, transform.position.z);
			}
		}
	}
}
