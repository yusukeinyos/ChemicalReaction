using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public GameObject scoreText;
	public GUIText gameoverText;
	public GameObject hpGUI;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		hpGUI.SendMessage ("AddHP",-0.001f);

	}

	public void HpUpdate(float deltaHP)
	{
		hpGUI.SendMessage ("AddHP",deltaHP);
	}

	public void ScoreUpdate(float deltaScore)
	{
		scoreText.SendMessage ("AddScore", deltaScore);
	}

	public void GameOver()
	{
		gameoverText.text="Game Over";
		//結果画面に遷移
	}
}
