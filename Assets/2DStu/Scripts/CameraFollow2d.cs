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

		focusArea = new FocusArea(target.GetComponent<PlayerController>().characterBoundCollider.bounds, focusAreaSize);
	}
	

	void FixedUpdate () {
//		if(target != null)
//		{
//			Vector3 targetCamPos = target.position + offset;
//			
//			transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing);
//			
//			if(transform.position.y < lowY)
//			{
//				transform.position = new Vector3(transform.position.x, lowY, transform.position.z);
//			}
//		}
	}

	void LateUpdate()
	{
		focusArea.Update(target.GetComponent<PlayerController>().characterBoundCollider.bounds);
		Vector2 focusPosition = focusArea.center + Vector2.up * verticalOffset;
		//transform.position =(Vector3) focusPosition + Vector3.forward * -10;

		Vector3 targetCamPos = (Vector3) focusPosition + Vector3.forward * -10;			
		transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing);
	}

	void OnDrawGizmos()
	{

		Gizmos.color = new Color(1,0,0,0.5f);
		Gizmos.DrawCube(focusArea.center, focusAreaSize);
	}


	public Vector2 focusAreaSize;
	public float verticalOffset;
	FocusArea focusArea;

	struct FocusArea
	{
		public Vector2 center;
		float left, right;
		float top, bottom;

		public FocusArea(Bounds targetBounds, Vector2 size)
		{
			left = targetBounds.center.x - size.x / 2;
			//Debug.Log(left + " " + targetBounds.center);
			right = targetBounds.center.x + size.x / 2;
			bottom = targetBounds.min.y;
			top = targetBounds.min.y + size.y;
			center = new Vector2((left + right)/2, (bottom + top)/2);

		}

		public void Update(Bounds targetBounds)
		{
			float shiftX = 0;
			if(targetBounds.min.x < left)
				shiftX = targetBounds.min.x - left;
			else if(targetBounds.max.x > right)
				shiftX = targetBounds.max.x - right;

			left += shiftX;
			right += shiftX;

			float shiftY = 0;
			if(targetBounds.min.y < bottom)
				shiftY = targetBounds.min.y - bottom;
			else if(targetBounds.max.y > top)
				shiftY = targetBounds.max.y - top;
			
			top += shiftY;
			bottom += shiftY;

			center = new Vector2((left + right)/2, (bottom + top)/2);
			Debug.Log("ppp");
		}
	}
}
