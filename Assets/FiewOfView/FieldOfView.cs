using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class FieldOfView : MonoBehaviour {
	[Range(0,360)]
	public float viewAngle;

	public float viewRadius;
	public float mashResolution;
	public LayerMask targetMask;
	public LayerMask obstacleMask;
	[HideInInspector]
	public List<Transform> visibleTargets = new List<Transform>();
	// Use this for initialization
	void Start () {
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

		for(int i=0;i<stepCount;i++)
		{
			float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
			Debug.DrawLine(transform.position, transform.position + DirFromAngle(angle,true) * viewRadius,Color.yellow);
		}
	}

	public Vector3 DirFromAngle(float angleInDegrees ,bool angleIsGloble)
	{
		if(! angleIsGloble)
			angleInDegrees += transform.eulerAngles.y;
		return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),0,Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
	}
}
