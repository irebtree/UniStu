using UnityEngine;
using System.Collections;

public class EnemyDamage : MonoBehaviour {
	public float damage;
	public float damageRate;
	public float pushBackForce;

	float nextDamage;
	// Use this for initialization
	void Start () {
		nextDamage = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D(Collider2D other)
	{
		//Debug.Log("****");
		if(other.tag == "Player" && nextDamage < Time.time)
		{
			PlayerHealth _health = other.gameObject.GetComponent<PlayerHealth>();
			_health.TakeDamage(damage);
			nextDamage = Time.time + damageRate;
			PushBack(other.transform);
		}

	}

	void PushBack(Transform transToPush)
	{
		Vector2 pushDire = new Vector2(0f, transToPush.position.y - transform.position.y).normalized;
		pushDire *= pushBackForce;
		Rigidbody2D rg2 = transToPush.GetComponent<Rigidbody2D>();
		rg2.velocity = Vector2.zero;
	
		rg2.AddForce(pushDire, ForceMode2D.Impulse);
	}
}
