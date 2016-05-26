using UnityEngine;
using System.Collections;

public class gibbonBehavior : MonoBehaviour 
{
	//VARIABLES


	public float orangSpeed;

	Rigidbody2D orangRB;
	Animator orangAnim;



	public LayerMask heroLayer;
	public Transform heroCheck;
	public float heroCheckRadius = 12f;

	public LayerMask nestALayer;
	public LayerMask nestBLayer;
	public Transform nestCheck;
	public float nestCheckRadius = .2f;


	public bool heroIsNearby;

	public bool inNestA;
	public bool inNestB;



	// START
	void Start () 
	{
		orangRB = GetComponent<Rigidbody2D>();
		orangAnim = GetComponent<Animator>();

	}




	// UPDATE
	void Update () 
	{
		heroIsNearby = Physics2D.OverlapCircle(heroCheck.position, heroCheckRadius, heroLayer);
		inNestA = Physics2D.OverlapCircle(nestCheck.position, nestCheckRadius, nestALayer);
		inNestB = Physics2D.OverlapCircle(nestCheck.position, nestCheckRadius, nestBLayer);


		if (heroIsNearby) 
		{
			moveToOtherNest ();
		} 
		else 
		{
			if (inNestB || inNestA) 
			{
				stopMoving();
			}
		}




	}



	void moveToOtherNest()
	{

		if (inNestA) 
		{
			moveToNestB();
		}

		if (inNestB) 
		{
			moveToNestA();
		}


	}





	void moveToNestB()
	{

		//orangRB.AddForce (new Vector2 (orangSpeed, 0));
		orangRB.velocity = new Vector2(orangSpeed, 0);

	}

	void moveToNestA()
	{
		//orangRB.AddForce(new Vector2(orangSpeed*-1f, 0));
		orangRB.velocity = new Vector2(orangSpeed*-1f, 0);
	}




	void stopMoving()
	{
		orangRB.velocity = new Vector2(0, 0);
	}

}




