using UnityEngine;
using System.Collections;

public class rocketHit : MonoBehaviour {
	public float rocketDamge;

	Projectile pro;
	public GameObject explosionEffect;
	// Use this for initialization
	void Awake () {
		pro = GetComponentInParent<Projectile>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.layer == LayerMask.NameToLayer("Shootable"))
		{
			pro.RemoveForce();
			Instantiate(explosionEffect, transform.position, transform.rotation);
			Destroy(gameObject);
			if(other.CompareTag("Enemy"))
			{
				EnemyHealth _health = other.GetComponent<EnemyHealth>();
				_health.TakeDamage(rocketDamge);
			}
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if(other.gameObject.layer == LayerMask.NameToLayer("Shootable"))
		{
			pro.RemoveForce();
			Instantiate(explosionEffect, transform.position, transform.rotation);
			Destroy(gameObject);

			if(other.CompareTag("Enemy"))
			{
				EnemyHealth _health = other.GetComponent<EnemyHealth>();
				_health.TakeDamage(rocketDamge);
			}
		}
	}
}
