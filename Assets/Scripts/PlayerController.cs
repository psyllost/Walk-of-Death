using System;
using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public Rigidbody2D bullet;                // Prefab of the arrow.
	public float movementSpeed = 20f;            // The speed of the projectile
	public Vector3 Rotation;    
	public float clockwise = 1000.0f;
	public float counterClockwise = -5.0f;
	private bool IsShooting=false;
	
    public float speed = 0.4f;
    Vector2 _dest = Vector2.zero;
    public Vector2 _dir = Vector2.zero;
    Vector2 _nextDir = Vector2.zero;

	public AudioClip audioclip;
	public AudioClip audioclip2;

	public AudioSource source;

    [Serializable]
    public class PointSprites
    {
        public GameObject[] pointSprites;
    }

    public PointSprites points;

    public static int killstreak = 0;

    // script handles
    private GameGUINavigation GUINav;
    private GameManager GM;
    private ScoreManager SM;

    private bool _deadPlaying = false;


	
	// Use this for initialization
    void Start()
    {
        GM = GameObject.Find("Game Manager").GetComponent<GameManager>();
        SM = GameObject.Find("Game Manager").GetComponent<ScoreManager>();
        GUINav = GameObject.Find("UI Manager").GetComponent<GameGUINavigation>();
        _dest = transform.position;
		source = GetComponent<AudioSource>();

    }

	void Update() {
		if (IsShooting==true) {
			if (Input.GetButtonDown ("Fire1")) {
					AudioSource.PlayClipAtPoint(audioclip2, gameObject.transform.position);

//			 Instantiate an arrow!

				Rigidbody2D bulletInstance = Instantiate (bullet, transform.position, Quaternion.Euler (new Vector3 (0, 0, 0))) as Rigidbody2D;
				if (_dir == Vector2.up) {
					bulletInstance.velocity = transform.up * movementSpeed;
				} else if (_dir == -Vector2.up) {
					bulletInstance.velocity = -transform.up * movementSpeed;
				} else if (_dir == -Vector2.right) {
					bulletInstance.velocity = -transform.right * movementSpeed;
				} else if (_dir == Vector2.right) {
					bulletInstance.velocity = transform.right * movementSpeed;
				}
			}
			StartCoroutine(KeepShooting());
		}

	}

	IEnumerator KeepShooting()
	{
		yield return new WaitForSeconds(8);
		IsShooting = false;
		
		//pc.GetComponent<PlayerController> ().setIsShooting(false);
	}
	// Update is called once per frame
	void FixedUpdate()
    {
        switch (GameManager.gameState)
        {
            case GameManager.GameState.Game:
                ReadInputAndMove();
                Animate();
                break;

            case GameManager.GameState.Dead:
                if (!_deadPlaying){
                    StartCoroutine("PlayDeadAnimation");
				}
                break;
        }


    }

    IEnumerator PlayDeadAnimation()
    {
        _deadPlaying = true;
        GetComponent<Animator>().SetBool("Die", true);
		Debug.Log (GetComponent<Animator>().GetBool("Die"));
		AudioSource.PlayClipAtPoint(audioclip, gameObject.transform.position);
	
        yield return new WaitForSeconds(2);
        GetComponent<Animator>().SetBool("Die", false);
        _deadPlaying = false;

        //if (GameManager.lives <= 0)
        //{
          //  Debug.Log("Treshold for High Score: " + SM.LowestHigh());
           // if (GameManager.score >= SM.LowestHigh())
            //    GUINav.getScoresMenu();
           // else
        //        GUINav.H_ShowGameOverScreen();
        //}

       // else
        GM.ResetScene();
		GM.ResetVariables ();
		Application.LoadLevel("game");

    }

    void Animate()
    {
        Vector2 dir = _dest - (Vector2)transform.position;
        GetComponent<Animator>().SetFloat("DirX", dir.x);
        GetComponent<Animator>().SetFloat("DirY", dir.y);
    }

    public bool Valid(Vector2 direction)
    {
        // cast line from 'next to pacman' to pacman
        // not from directly the center of next tile but just a little further from center of next tile
        Vector2 pos = transform.position;
        direction += new Vector2(direction.x * 0.45f, direction.y * 0.45f);
        RaycastHit2D hit = Physics2D.Linecast(pos + direction, pos);
        return hit.collider.name == "pacdot" || (hit.collider == GetComponent<Collider2D>());
    }

    public void ResetDestination()
    {
        _dest = new Vector2(10.2f, 5f);
        GetComponent<Animator>().SetFloat("DirX", 1);
        GetComponent<Animator>().SetFloat("DirY", 0);
    }

    void ReadInputAndMove()
    {
        // move closer to destination
        Vector2 p = Vector2.MoveTowards(transform.position, _dest, speed);
        GetComponent<Rigidbody2D>().MovePosition(p);

        // get the next direction from keyboard
        if (Input.GetAxis("Horizontal") > 0) _nextDir = Vector2.right;
        if (Input.GetAxis("Horizontal") < 0) _nextDir = -Vector2.right;
        if (Input.GetAxis("Vertical") > 0) _nextDir = Vector2.up;
        if (Input.GetAxis("Vertical") < 0) _nextDir = -Vector2.up;

        // if pacman is in the center of a tile
        if (Vector2.Distance(_dest, transform.position) < 0.00001f)
        {
            if (Valid(_nextDir))
            {
                _dest = (Vector2)transform.position + _nextDir;
                _dir = _nextDir;
            }
            else   // if next direction is not valid
            {
                if (Valid(_dir))  // and the prev. direction is valid
                    _dest = (Vector2)transform.position + _dir;   // continue on that direction

                // otherwise, do nothing
            }
        }
    }

    public Vector2 getDir()
    {
        return _dir;
    }

    public void UpdateScore()
    {
        killstreak++;

        // limit killstreak at 4
        if (killstreak > 4) killstreak = 4;

        Instantiate(points.pointSprites[killstreak - 1], transform.position, Quaternion.identity);
        GameManager.score += (int)Mathf.Pow(2, killstreak) * 100;

    }

	public void setIsShooting(bool isShooting) {
		this.IsShooting = true;
	}
}
