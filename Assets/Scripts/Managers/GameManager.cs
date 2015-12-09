using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    //--------------------------------------------------------
    // Game variables

    public static int Level = 0;
    public static int lives = 3;

	public enum GameState { Init, Game, Dead, Scores }
	public static GameState gameState;

    private GameObject pacman;
    private GameObject blinky;
    private GameObject pinky;
    private GameObject inky;
    private GameObject clyde;
	private GameObject zombieStatic;
	private GameObject zombieStatic2;
	private GameObject zombieStatic3;
	private GameObject zombieStatic4;
	private GameObject bullet;
	private GameObject pacDot;
    private GameGUINavigation gui;
	private Time startTime;

	public static bool scared;
    static public int score;

	public float scareLength;
	private float _timeToCalm;

    public float SpeedPerLevel;
    
    //-------------------------------------------------------------------
    // singleton implementation
    private static GameManager _instance;

    public static GameManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();
                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

    //-------------------------------------------------------------------
    // function definitions

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if(this != _instance)   
                Destroy(this.gameObject);
        }

        AssignGhosts();
    }

	void Start () 
	{	
		score = 0;
		gameState = GameState.Init;
	}

    void OnLevelWasLoaded()
    {
        Debug.Log("Level " + Level + " Loaded!");
        AssignGhosts();
        ResetVariables();


        // Adjust Ghost variables!
        clyde.GetComponent<GhostMove>().speed += Level * SpeedPerLevel;
        blinky.GetComponent<GhostMove>().speed += Level * SpeedPerLevel;
        pinky.GetComponent<GhostMove>().speed += Level * SpeedPerLevel;
        inky.GetComponent<GhostMove>().speed += Level * SpeedPerLevel;
        pacman.GetComponent<PlayerController>().speed += Level*SpeedPerLevel/2;
    }

    public void ResetVariables()
    {
        _timeToCalm = 0.0f;
        scared = false;
        PlayerController.killstreak = 0;
		GameObject[] pacdots = GameObject.FindGameObjectsWithTag("pacdot");

    }

    // Update is called once per frame
	void Update () 
	{
		score ++;
		if(scared && _timeToCalm <= Time.time)
			CalmGhosts();

	}

	public void ResetScene()
	{
        CalmGhosts();

		pacman.transform.position = new Vector3(10.2f, 5f, 0f);
		blinky.transform.position = new Vector3(15f, 20f, 0f);
		pinky.transform.position = new Vector3(14.5f, 17f, 0f);
		inky.transform.position = new Vector3(16.5f, 17f, 0f);
		clyde.transform.position = new Vector3(12.5f, 17f, 0f);

		pacman.GetComponent<PlayerController>().ResetDestination();
		blinky.GetComponent<GhostMove>().InitializeGhost();
		pinky.GetComponent<GhostMove>().InitializeGhost();
		inky.GetComponent<GhostMove>().InitializeGhost();
		clyde.GetComponent<GhostMove>().InitializeGhost();
        gameState = GameState.Init;  
        gui.H_ShowReadyScreen();

		score = 0;

	}

	public void ToggleScare()
	{
		if(!scared)	ScareGhosts();
		else 		CalmGhosts();
	}

	public void ScareGhosts()
	{
		scared = true;
		blinky.GetComponent<GhostMove>().Frighten();
		pinky.GetComponent<GhostMove>().Frighten();
		inky.GetComponent<GhostMove>().Frighten();
		clyde.GetComponent<GhostMove>().Frighten();

		//zombieStatic.GetComponent<ZombieStaticMove> ().Frighten ();
		//zombieStatic2.GetComponent<ZombieStaticMove> ().Frighten ();
		//zombieStatic3.GetComponent<ZombieStaticMove> ().Frighten ();
		//zombieStatic4.GetComponent<ZombieStaticMove> ().Frighten ();
		_timeToCalm = Time.time + scareLength;

        Debug.Log("Ghosts Scared");
	}

	public void CalmGhosts()
	{
		scared = false;
		blinky.GetComponent<GhostMove>().Calm();
		pinky.GetComponent<GhostMove>().Calm();
		inky.GetComponent<GhostMove>().Calm();
		clyde.GetComponent<GhostMove>().Calm();

		//zombieStatic.GetComponent<ZombieStaticMove> ().Calm ();
		//zombieStatic2.GetComponent<ZombieStaticMove> ().Calm ();
		//zombieStatic3.GetComponent<ZombieStaticMove> ().Calm ();
		//zombieStatic4.GetComponent<ZombieStaticMove> ().Calm ();
	    PlayerController.killstreak = 0;
    }

    void AssignGhosts()
    {
        // find and assign ghoststr
        clyde = GameObject.Find("clyde");
        pinky = GameObject.Find("pinky");
        inky = GameObject.Find("inky");
        blinky = GameObject.Find("blinky");
        pacman = GameObject.Find("pacman");
		zombieStatic = GameObject.Find ("zombie_static1");
		zombieStatic2 = GameObject.Find ("zombie_static2");
		zombieStatic3 = GameObject.Find ("zombie_static3");
		zombieStatic4 = GameObject.Find ("zombie_static4");

        if (clyde == null || pinky == null || inky == null || blinky == null || zombieStatic == null) Debug.Log("One of ghosts are NULL");
        if (pacman == null) Debug.Log("Pacman is NULL");

        gui = GameObject.FindObjectOfType<GameGUINavigation>();

        if(gui == null) Debug.Log("GUI Handle Null!");

    }

	public void LoseLife()
	{
		//lives--;
		gameState = GameState.Dead;
		//ResetScene ();
		// update UI too
		//UIScript ui = GameObject.FindObjectOfType<UIScript>();
		//Destroy(ui.lives[ui.lives.Count - 1]);
		//ui.lives.RemoveAt(ui.lives.Count - 1);
	}

    public static void DestroySelf()
    {

        score = 0;
        Level = 0;
        lives = 3;
        Destroy(GameObject.Find("Game Manager"));
    }
}
