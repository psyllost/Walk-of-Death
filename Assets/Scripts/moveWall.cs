using UnityEngine;
using System.Collections;

public class moveWall : MonoBehaviour {

	private float start1=0.0F;
	private float start2=0.0F;
	private GameManager _gm;


	// Use this for initialization
	void Start () {
		_gm = GameObject.Find("Game Manager").GetComponent<GameManager>();

	}
	
	// Update is called once per frame
	void Update () {

		transform.localScale += new Vector3(0, start2,0);
		transform.position += new  Vector3 (0, start1,0);

		start2 = start2+0.00003F;
		start1 = start2 / 2.0F;
	}

	void OnTriggerEnter2D(Collider2D co) {
		if (co.name == "pacman"){
			_gm.LoseLife();
		}
	}
}
