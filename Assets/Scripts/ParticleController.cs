using UnityEngine;
using System.Collections;

public class ParticleController : MonoBehaviour {
	float bornTime;
	// Use this for initialization
	void Start () {
		bornTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - bornTime > 3.0f)
			Destroy (gameObject);
	}
}
