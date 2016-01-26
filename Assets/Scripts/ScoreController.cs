using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {
	private Text scoreText;
	private int score;

	// Use this for initialization
	void Start () {
		scoreText = GetComponent<Text> ();
		score = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AddScore(int deltaScore)
	{
		//if (!isDead) {
			score += deltaScore;
			scoreText.text = "Score : " + score.ToString ();
		//}
	}
}
