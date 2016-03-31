using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
	public float fullHealth;
	PlayerController pc;
	float curHealth;
	// Use this for initialization
	void Start () {
		pc = GetComponent<PlayerController>();
		curHealth = fullHealth;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void TakeDamage(float damage)
	{
		if(curHealth <= 0)
			return;
		curHealth -= damage;
		if(curHealth <= 0)
			MakeDeath();
	}

	void MakeDeath()
	{
		Destroy(gameObject);
	}
}
