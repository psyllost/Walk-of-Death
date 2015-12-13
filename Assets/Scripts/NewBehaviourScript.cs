using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {


	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") 
		{
			Debug.Break ();
			return;
		}

		if (false)//other.gameObject.transform.parent) 
		{
			Destroy (other.gameObject.transform.parent.gameObject);
		} 
		else 
		{
			Destroy (other.gameObject);
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
