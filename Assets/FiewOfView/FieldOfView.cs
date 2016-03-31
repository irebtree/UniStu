using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class FieldOfView : MonoBehaviour {
	[Range(0,360)]
	public float viewAngle;

	public float viewRadius;
	public float mashResolution;

	public MeshFilter viewMeshFilter;
	Mesh viewMesh;

	public LayerMask targetMask;
	public LayerMask obstacleMask;
	[HideInInspector]
	public List<Transform> visibleTargets = new List<Transform>();
	// Use this for initialization
	void Start () {
		viewMesh = new Mesh();
		viewMesh.name = "view mesh";
		viewMeshFilter.mesh = viewMesh;
		StartCoroutine(FindTargets(0.5f));
	}
	
	// Update is called once per frame
	void Update () {
		DrawFieldOfView();
	}

	IEnumerator FindTargets(float delay)
	{
		while(true)
		{
			yield return new WaitForSeconds(delay);
			FindVisibleTargets();
		}
	}

	void FindVisibleTargets()
	{
		visibleTargets.Clear();
		Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

		for(int i=0;i<targetsInViewRadius.Length;i++)
		{
			Transform target = targetsInViewRadius[i].transform;
			Vector3 dirToTarget = (target.position - transform.position).normalized;

			if(Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
			{
				float disToTarget = Vector3.Distance(transform.position, target.position);
				if(!Physics.Raycast(transform.position,dirToTarget,disToTarget,obstacleMask))
				{
					visibleTargets.Add(target);
				}
			}
		}
	}

	void DrawFieldOfView()
	{
		int stepCount = Mathf.RoundToInt(viewAngle * mashResolution);
		float stepAngleSize = viewAngle / stepCount;
		List<Vector3> viewPoint = new List<Vector3>(); 
		for(int i=0;i<stepCount;i++)
		{
			float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
			//Debug.DrawLine(transform.position, transform.position + DirFromAngle(angle,true) * viewRadius,Color.yellow);
			ViewCastInfo newViewInfo = ViewCast(angle);
			viewPoint.Add(newViewInfo.point);
		}

		int vertexCount = viewPoint.Count + 1;
		Vector3[] vertices = new Vector3[vertexCount];
		int[] triangles = new int[(vertexCount - 2) * 3];

		vertices[0] = Vector3.zero;

		for(int i=0;i<vertexCount - 1;i++)
		{
			vertices[i+1] = transform.InverseTransformPoint( viewPoint[i]);
			if(i<vertexCount -2)
			{
				triangles[i*3] = 0;
				triangles[i*3 + 1] = i + 1;
				triangles[i*3 + 2] = i + 2;
			}
		}

		viewMesh.Clear();
		viewMesh.vertices = vertices;
		viewMesh.triangles = triangles;
		viewMesh.RecalculateNormals();
	}

	ViewCastInfo ViewCast(float globalAngle)
	{
		Vector3 dir = DirFromAngle(globalAngle, true);
		RaycastHit hit;
		if(Physics.Raycast(transform.position, dir, out hit, viewRadius, obstacleMask))
		{
			return new ViewCastInfo(true, hit.point, viewRadius, globalAngle);
		}
		else
		{
			return new ViewCastInfo(false, transform.position + dir * viewRadius, viewRadius, globalAngle);
		}
	}

	public Vector3 DirFromAngle(float angleInDegrees ,bool angleIsGloble)
	{
		if(! angleIsGloble)
			angleInDegrees += transform.eulerAngles.y;
		return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),0,Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
	}

	public struct ViewCastInfo
	{
		public bool hit;
		public Vector3 point;
		public float dis;
		public float angle;

		public ViewCastInfo(bool _hit, Vector3 _point, float _dis, float _angle)
		{
			hit = _hit;
			point = _point;
			dis = _dis;
			angle = _angle;
		}
	}










}
