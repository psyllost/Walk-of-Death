using UnityEngine;
using System.Collections;

public class Energizer : MonoBehaviour {
	public GameObject pc;
    private GameManager gm;
	//private GameObject energizer;
	// Use this for initialization
	void Start ()
	{
	    gm = GameObject.Find("Game Manager").GetComponent<GameManager>();

        if( gm == null )    Debug.Log("Energizer did not find Game Manager!");
	}

    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.name == "pacman" && gameObject.name == "energizer") {
			gm.ScareGhosts ();
			Destroy (gameObject);
		} else if (col.name == "pacman" && gameObject.name == "weapon") {
			pc.GetComponent<PlayerController> ().setIsShooting(true);
			Destroy(gameObject);
		}
    }
	IEnumerator KeepShooting()
	{
		yield return new WaitForSeconds(8);
		pc.GetComponent<PlayerController> ().setIsShooting(false);
	}
}