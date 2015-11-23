using UnityEngine;
using System.Collections;

public class ZombieStaticMove : MonoBehaviour {
	public Transform[] waypoints;
	int cur = 0;
	public float speed = 0.3f;
	enum State { Wait, Init, Scatter, Chase, Run };
	State state;
	public PlayerController pacman;
	private GameManager _gm;
	private float _timeToWhite;
	private float _timeToToggleWhite;

	void Start()
	{
		_gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
		//_toggleInterval = _gm.scareLength * 0.33f * 0.20f;  
	}
	// Update is called once per frame
	void FixedUpdate () {
		// Waypoint not reached yet? then move closer
		if (transform.position != waypoints[cur].position) {
			Vector2 p = Vector2.MoveTowards(transform.position,
			                                waypoints[cur].position,
			                                speed);
			GetComponent<Rigidbody2D>().MovePosition(p);
		}
		// Waypoint reached, select next one
		else cur = (cur + 1) % waypoints.Length;
		// Animation
		Vector2 dir = waypoints[cur].position - transform.position;
		GetComponent<Animator>().SetFloat("DirX", dir.x);
		GetComponent<Animator>().SetFloat("DirY", dir.y);
	}
	//TODO
	public void Calm()
	{
		// if the ghost is not running, do nothing
		if (state != State.Run) return;
		
		//waypoints.Clear ();
		state = State.Chase;
		_timeToToggleWhite = 0;
		_timeToWhite = 0;
		GetComponent<Animator>().SetBool("Run_White", false);
		GetComponent<Animator>().SetBool("Run", false);
	}
	void OnTriggerEnter2D(Collider2D co) {
		if (co.name == "pacman")
			//Destroy(co.gameObject);
			if (state == State.Run)
		{
			//Calm();
			//InitializeGhost(_startPos);
			pacman.UpdateScore();
		}
		
		else
		{
			_gm.LoseLife();
		}

	}
}
