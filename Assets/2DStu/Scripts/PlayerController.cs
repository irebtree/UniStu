using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float maxSpeed;

	Rigidbody2D mRigibody;
	Animator anim;
	bool facingRight = true;


	public bool grouded = false;
	public float groundCheckRedius = 0.2f;
	public Transform groundCheck;
	public float jumpHeight;
	public LayerMask groundLayer;

	public Transform gunTip;
	public GameObject bullet;
	float fireRate = 0.5f;
	float fireNext;

	// Use this for initialization
	void Start () {
		mRigibody = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {

		if(grouded && Input.GetAxis("Vertical") > 0)
		{
			grouded = false;
			anim.SetBool(AnimInfo.isGround, grouded);
			mRigibody.AddForce(new Vector2(0f, jumpHeight));
		}

		if(Input.GetKey(KeyCode.Space))
			Shoot();
	
	}

	void Shoot()
	{
		if(Time.time > fireNext)
		{
			fireNext = Time.time + fireRate;
			if(facingRight)
				Instantiate(bullet,gunTip.position,Quaternion.Euler(0,0,0));
			else if( ! facingRight)
				Instantiate(bullet, gunTip.position, Quaternion.Euler(0,0,180));
		}
	}

	void FixedUpdate(){

		grouded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRedius,groundLayer);
		anim.SetBool(AnimInfo.isGround, grouded);
		anim.SetFloat(AnimInfo.verticalSpeed, mRigibody.velocity.y);

		float move = Input.GetAxis("Horizontal");
		anim.SetFloat(AnimInfo.speed , Mathf.Abs(move));
		//Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), 0f);
		//Vector2 moveVelocity = moveInput.normalized * maxSpeed;
		mRigibody.velocity = new Vector2(move * maxSpeed, mRigibody.velocity.y);
		//mRigibody.MovePosition(mRigibody.position + moveVelocity * Time.deltaTime);

		if(move > 0 && !facingRight)
			Flip();
		else if(move <0 && facingRight)
			Flip();

	}

	void Flip()
	{
		facingRight = ! facingRight;
		Vector3 thisScale = transform.localScale;
		thisScale.x *= -1;
		transform.localScale = thisScale;
	}
}
