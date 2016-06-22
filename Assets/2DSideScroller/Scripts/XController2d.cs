using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class XController2d : MonoBehaviour {
	public int horizontalRayCount = 4;
	public int verticalRayCout = 4;

	public LayerMask collisionMask;

	float horizontalRaySpacing;
	float verticalRaySpacing;

	BoxCollider2D collider;
	RaycastOrigins raycastOrigins;

	const float skinWidth = .015f;

	// Use this for initialization
	void Start () {
		collider = GetComponent<BoxCollider2D>();
		CalculateRaySpacing();

	}
	
	// Update is called once per frame
	void Update () {
		//UpdateRaycastOrigins();
		//CalculateRaySpacing();


	}

	public void Move(Vector3 velocity)
	{
		UpdateRaycastOrigins();
		VerticalCollisions(ref velocity);
		//Debug.Log(velocity);
		transform.Translate(velocity);


	}

	void VerticalCollisions(ref Vector3 velocity)
	{
		float directionY = Mathf.Sign(velocity.y);
		float rayLength = Mathf.Abs(velocity.y) + skinWidth;

		for(int i=0;i<verticalRayCout;i++)
		{
			Vector2 rayOrigin = (directionY == -1)? raycastOrigins.bottomLeft:raycastOrigins.topLeft;
			rayOrigin += Vector2.right * verticalRaySpacing * i;
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);
			Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.red);

			if(hit)
			{
				velocity.y = (hit.distance - skinWidth) * directionY;
				rayLength = hit.distance;
			}
		}
	}

	void UpdateRaycastOrigins()
	{
		Bounds bounds = collider.bounds;
		//Debug.Log("1 = " + bounds.extents);
		bounds.Expand(skinWidth * -2);
		//Debug.Log(bounds.extents);

		raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
		raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
		raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
		raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);

	}

	void CalculateRaySpacing()
	{
		Bounds bounds = collider.bounds;
		//Debug.Log("1 = " + bounds.extents);
		bounds.Expand(skinWidth * -2);

		horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
		verticalRayCout = Mathf.Clamp(verticalRayCout, 2, int.MaxValue);

		horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
		verticalRaySpacing = bounds.size.x / (verticalRayCout - 1);
	}

	struct RaycastOrigins
	{
		public Vector2 topLeft, topRight;
		public Vector2 bottomLeft, bottomRight;
	}
}
