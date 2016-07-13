using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class XAIBehaviours : MonoBehaviour {
	
	public bool isActive { get; private set; }
	public bool isDefending = false;

	public XBaseState initialState;
	public XBaseState currentState { get; private set; }
	public XBaseState previousState { get; private set; }
	public XBaseState[] states = new XBaseState[0];

	public LayerMask raycastLayers = -1;

	public float health = 100.0f;
	public float maxHealth = 100.0f;

	public static System.Action<XBaseState , XBaseState > onStateChanged;// new state , previous state

	protected Transform trans; 
	public Vector3 currentDestination { get; private set; }

	#region FieldOfView 
	[Range(0,360)]
	public float viewAngle = 172;

	public float viewRadius = 5;
	public float mashResolution = 10;

	public MeshFilter viewMeshFilter;
	Mesh viewMesh;

	//public LayerMask targetMask = -1;
	public LayerMask obstacleMask = -1;
	[HideInInspector]
	public List<Transform> visibleTargets = new List<Transform>();
	#endregion FieldOfView

	void Awake () {
		Init();
	}

	void Start () {
		ChangeActiveState(initialState);
		viewMesh = new Mesh();
		viewMesh.name = "view mesh";
		if(viewMeshFilter != null)
			viewMeshFilter.mesh = viewMesh;

		Debug.Log( DirFromAngle(30, true));
	}

	void Init()
	{
		trans = this.transform;
		currentDestination = transform.position;

		SetActive(true);
	}
	

	void Update () {
		//DrawFieldOfView();
	}
		

	public XBaseState[] GetAllStates()
	{
		return states;
	}

	public void ReplaceAllStates(XBaseState[] states)
	{
		this.states = states;
	}

	public void SetActive(bool isActive)
	{
		this.isActive = isActive;
	}

	public void ChangeActiveState(XBaseState newState)
	{
		if ( newState == null )
		{
			return;
		}



		previousState = currentState;

		if ( previousState != null )
		{
			previousState.ExitState(this);
		}

		currentState = newState;
		//newState.InitState(this);

		if ( onStateChanged != null )
		{
			onStateChanged(newState, previousState);
		}

	}

	#region FieldOfView function
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
		Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, raycastLayers);

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

	#endregion FieldOfView function

	void OnDrawGizmos()
	{
		//Vector3 dir = DirFromAngle(45, true);
		//Gizmos.color = Color.red;
		//Gizmos.DrawLine(Vector3.zero,  dir);
	}
}
