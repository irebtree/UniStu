using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {
	public float health;

	float curHealth;
	// Use this for initialization
	void Start () {
		curHealth = health;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void TakeDamage(float damage)
	{
		curHealth -= damage;
		if(curHealth <= 0)
			MakeDead();
	}

	void MakeDead()
	{
		Destroy(gameObject);
	}
}
