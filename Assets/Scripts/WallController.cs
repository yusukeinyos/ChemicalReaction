using UnityEngine;
using System.Collections;

public class WallController : MonoBehaviour {
	private BoxCollider2D col;
	// Use this for initialization
	void Start () {
		col = GetComponent<BoxCollider2D> ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col != null)
		if (col.CompareTag ("Molecule")) {
			Destroy (col.gameObject);
		}

		//爆発効果

		GameObject.FindWithTag ("GameController").SendMessage ("HpUpdate", -0.05f);
	}
}
