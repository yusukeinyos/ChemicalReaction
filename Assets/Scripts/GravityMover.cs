using UnityEngine;
using System.Collections;

public class GravityMover : MonoBehaviour {
	public Vector2 topLeft;
	public Vector2 bottomRight;

	private float changeTime;
	private float beforeTime;

	// Use this for initialization
	void Start () {
		beforeTime = Time.time;
		changeTime = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - beforeTime > changeTime) {
			float x = Random.Range(topLeft.x,bottomRight.x);
			float y = Random.Range(bottomRight.y,topLeft.y);
			Vector2 newPosition=new Vector2(x,y);
			transform.position = newPosition;
			beforeTime = Time.time;
		}
	}
}
