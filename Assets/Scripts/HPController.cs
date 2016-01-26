using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HPController : MonoBehaviour {
	private Slider sliderGUI;
	private float hp;
	// Use this for initialization
	void Start () {
		sliderGUI = GetComponent<Slider> ();
		hp = 1;
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void AddHP(float deltaHP)
	{
		hp += deltaHP;
		if (hp < 0) {

		}
		sliderGUI.value = hp;

	}
}
