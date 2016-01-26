using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col != null)
		if (col.CompareTag ("Molecule")) {
			GameObject.FindWithTag ("GameController").SendMessage ("ScoreUpdate", 10.0f);
			Destroy(col.gameObject);
		}
		
		//爆発効果
		

	}
}
