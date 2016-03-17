using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	public float speed = 10f;
	Rigidbody2D mRG;
	// Use this for initialization
	void Awake () {
		mRG = GetComponent<Rigidbody2D>();
		if(transform.eulerAngles.z > 0)
			mRG.AddForce(new Vector2(-1,0) * speed, ForceMode2D.Impulse);
		else
			mRG.AddForce(new Vector2(1,0) * speed, ForceMode2D.Impulse);
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
