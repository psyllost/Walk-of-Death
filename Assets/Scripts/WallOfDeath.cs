using UnityEngine;
using System.Collections;

public class WalkOfDeath : MonoBehaviour {
	public GameObject pc;
	private GameManager gm;
	public BoxCollider2D wallOfDeath;
	public float movementSpeed = 5f;
	public Vector2 _dir = Vector2.zero;

	
	//private GameObject energizer;
	// Use this for initialization
	void Start ()
	{
		gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
		

		
	}

	void Update()
	{
	


	}
	
	void FixedUpdate() {
		//rb.AddForce(Vector3.forward);
	}
	
	void Move(Collider2D col)
	{
		//wallOfDeath.AddForce (Vector2.up);
	}
	
}